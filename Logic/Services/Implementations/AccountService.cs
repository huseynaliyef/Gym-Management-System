using AutoMapper;
using AutoMapper.Execution;
using Azure;
using Data.Entities;
using Logic.Helpers.JWT;
using Logic.Models.DTOs.Admin;
using Logic.Models.DTOs.Member;
using Logic.Models.DTOs.Personel;
using Logic.Models.DTOs.Shared;
using Logic.Models.Response_Model;
using Logic.Models.Token;
using Logic.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Serilog;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Member = Data.Entities.Member;

namespace Logic.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public readonly IGenericRepository<Member> _memberRepository;
        private readonly IEmailService _emailService;
        private readonly IGenericRepository<AccountRecoveryEmailToken> _recoveryEmailTokenRepository;
        private readonly IGenericRepository<MemberMembership> _memberMembershipRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AccountService(UserManager<IdentityUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           SignInManager<IdentityUser> signInManager,
                           IEmailService emailService,
                           IGenericRepository<AccountRecoveryEmailToken> recoveryEmailTokenRepository,
                           IGenericRepository<MemberMembership> memberMembershipRepository,
                           IGenericRepository<Member> memberRepository,
                           IConfiguration configuration,
                           IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _recoveryEmailTokenRepository = recoveryEmailTokenRepository;
            _memberMembershipRepository = memberMembershipRepository;
            _memberRepository = memberRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<GenericResponse<TokenModel>> GetJwtTokenWithRefreshToken(string refreshToken)
        {
            var Response = new GenericResponse<TokenModel>();
            try
            {
                var existRefreshToken = (await _memberRepository.GetByExperssion(x => x.RefreshToken == refreshToken)).FirstOrDefault();
                if (existRefreshToken != null)
                {
                    var memberRole = (await _userManager.GetRolesAsync(existRefreshToken)).FirstOrDefault();
                    var token = new JWTHelper().CreateToken(_configuration, existRefreshToken, memberRole);
                    existRefreshToken.RefreshToken = token.RefreshToken;
                    _memberRepository.Update(existRefreshToken);
                    await _memberRepository.Commit();
                    Response.Success(token);
                }
                else
                {
                    Response.Error(404, "Wrong Refresh Token!");
                }
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
            }
            return Response;
        }
        public async Task<GenericResponse<TokenModel>> Login(LoginDTO model, ModelStateDictionary ModelState)
        {
            var Response = new GenericResponse<TokenModel>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    Response.Error(400, error);
                    return Response;
                }
                
                var member = (Member)await _userManager.FindByNameAsync(model.UserName);
                if (member != null)
                {
                    var role = (await _userManager.GetRolesAsync(member)).FirstOrDefault();
                    SignInResult SignIn = await _signInManager.CheckPasswordSignInAsync(member, model.Password, false);

                    if (SignIn.Succeeded)
                    {
                        var token = new JWTHelper().CreateToken(_configuration, member, role);
                        member.RefreshToken = token.RefreshToken;
                        _memberRepository.Update(member);
                        await _memberRepository.Commit();
                        Response.Success(token);
                    }
                    else
                    {
                        Response.Error(400, "Wrong Password.");
                    }

                }
                else
                    Response.Error(404, "User is not found.");

                Log.Logger.Information($"{nameof(AccountService)}.{nameof(Login)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }

            return Response;


        }
        public async Task<GenericResponse<bool>> CreateAdmin()
        {
            var Response = new GenericResponse<bool>();
            try
            {
                var newUser = new Member { UserName = "LeadAdmin", Email = "admin@gmail.com" };
                var result = await _userManager.CreateAsync(newUser, "admin123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Admin");
                    Response.Success(true);
                }
                else
                {
                    Response.Error(400, "Occured Error");
                }
                Log.Logger.Information($"{nameof(AccountService)}.{nameof(CreateAdmin)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> RegisterMember(MemberRegisterDTO model, IWebHostEnvironment environment, ModelStateDictionary ModelState)
        {
            var Response = new GenericResponse<bool>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    Response.Error(400, error);
                    return Response;
                }

                var existRole = await _roleManager.FindByNameAsync("Member");
                if (existRole == null)
                {
                    var MemberRole = new IdentityRole { Name = "Member" };
                    await _roleManager.CreateAsync(MemberRole);
                }

                var existMember = await _userManager.FindByEmailAsync(model.Email);
                if (existMember != null) 
                    Response.Error(400, "Member is exist.");

                else
                {
                    var newMember = new Member { UserName = model.UserName, Email = model.Email, Age = model.Age, Gender = model.Gender, PhoneNumber = model.PhoneNumber };
                    if (model.Photo != null)
                    {
                        var photoName = string.Concat(Guid.NewGuid().ToString(), model.Photo.FileName);
                        var folderPath = Path.Combine(environment.WebRootPath, model.UserName);
                        var filePath = Path.Combine(folderPath, photoName);
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            model.Photo.CopyTo(stream);
                        }
                        newMember.Photo = photoName;
                    }

                    var result = await _userManager.CreateAsync(newMember, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newMember, "Member");
                        Response.Success(true, 201);
                    }
                    else
                    {
                        Response.Error(400, "Occured Error");
                    }

                }

                Log.Logger.Information($"{nameof(AccountService)}.{nameof(RegisterMember)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }

            return Response;
        }
        
        public async Task<GenericResponse<bool>> RegisterPersonel(PersonelRegisterDTO model, IWebHostEnvironment environment, ModelStateDictionary ModelState)
        {
            var Response = new GenericResponse<bool>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    Response.Error(400, error);
                    return Response;
                }
                var existRole = await _roleManager.FindByNameAsync("Personel");

                if (existRole == null)
                {
                    var PersonelRole = new IdentityRole { Name = "Personel" };
                    await _roleManager.CreateAsync(PersonelRole);
                }

                var existMember = await _userManager.FindByEmailAsync(model.Email);

                if (existMember != null)
                    Response.Error(400, "Personel is exist.");

                else
                {
                    var newMember = new Member { UserName = model.UserName, Email = model.Email, Age = model.Age, Gender = model.Gender, PhoneNumber = model.PhoneNumber };

                    if (model.Photo != null)
                    {
                        var photoName = string.Concat(Guid.NewGuid().ToString(), model.Photo.FileName);
                        var folderPath = Path.Combine(environment.WebRootPath, model.UserName);
                        var filePath = Path.Combine(folderPath, photoName);
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            model.Photo.CopyTo(stream);
                        }
                        newMember.Photo = photoName;
                    }

                    var result = await _userManager.CreateAsync(newMember, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newMember, "Personel");
                        Response.Success(true, 201);
                    }
                    else
                        Response.Error(400, "Occured Error");

                }
                Log.Logger.Information($"{nameof(AccountService)}.{nameof(RegisterPersonel)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
            
        }

        public async Task<GenericResponse<bool>> UpdateInformation(UpdateDTO model, ClaimsPrincipal userModel, IWebHostEnvironment environment, ModelStateDictionary ModelState)
        {
            var Response = new GenericResponse<bool>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    Response.Error(400, error);
                    return Response;
                }

                Member user = (Member)await _userManager.GetUserAsync(userModel);

                if (model.UserName != null)
                {
                    user.UserName = model.UserName;
                }

                if (model.PhoneNumber != null) user.PhoneNumber = model.PhoneNumber;
                if (model.Age != null) user.Age = model.Age;
                if (model.Gender != null) user.Gender = model.Gender;



                if (model.Photo != null)
                {
                    var photoName = string.Concat(Guid.NewGuid().ToString(), model.Photo.FileName);
                    var folderPath = Path.Combine(environment.WebRootPath, user.UserName);
                    var filePath = Path.Combine(folderPath, photoName);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(stream);
                    }
                    user.Photo = photoName;
                }
                await _userManager.UpdateAsync(user);
                Response.Success(true, 200);

                Log.Logger.Information($"{nameof(AccountService)}.{nameof(UpdateInformation)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;

            
        }

        public async Task<GenericResponse<bool>> RecoveryPassword(RecoveryEmailDTO model, ModelStateDictionary ModelState)
        {
            var Response = new GenericResponse<bool>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    Response.Error(400, error);
                    return Response;
                }
                var existEmail = await _userManager.FindByEmailAsync(model.Email);
                if (existEmail != null)
                {
                    Random rnd = new Random();
                    var key = rnd.NextInt64(100000, 999999);
                    string token = key.ToString();
                    _emailService.SendEmail(model.Email, "Recovery Code", $"https://localhost:44316/api/Account/CheckToken?Token={token}");

                    await _recoveryEmailTokenRepository.AddAndCommit(new AccountRecoveryEmailToken
                    {
                        MemberId = existEmail.Id,
                        Token = token,
                        ExpireDate = DateTime.Now.AddMinutes(2)
                    });
                    Response.Success(true, 200);
                }
                else
                {
                    Response.Error(400, "User is not found.");
                }
                Log.Logger.Information($"{nameof(AccountService)}.{nameof(RecoveryPassword)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
            
        }
        public async Task<GenericResponse<string>> CheckToken(string Token)
        {
            var Response = new GenericResponse<string>();
            try
            {
                var TokeisExist = (await _recoveryEmailTokenRepository.GetByExperssion(x => x.Token == Token && x.ExpireDate > DateTime.Now)).FirstOrDefault();
                if (TokeisExist != null)
                {
                    Response.Success(Token, 200);
                }
                else
                    Response.Error(400, "Token Error!");

                Log.Logger.Information($"{nameof(AccountService)}.{nameof(CheckToken)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> ChangePassword(ChangePasswordDTO model, ModelStateDictionary ModelState)
        {
            var Response = new GenericResponse<bool>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    Response.Error(400, error);
                    return Response;
                }
                if(model.newPassword != model.confirmPassword)
                {
                    Response.Error(400, "Passwords are not equals!");
                    return Response;
                }

                var TokenData = (await _recoveryEmailTokenRepository.GetByExperssion(x => x.Token == model.Token && x.ExpireDate > DateTime.Now)).FirstOrDefault();
                if(TokenData != null)
                {
                    var user = await _userManager.FindByIdAsync(TokenData.MemberId);
                    if (user == null)
                    {
                        Response.Error(400, "Invalid Token");
                        return Response;
                    }
                    
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.newPassword);
                    var Result = await _userManager.UpdateAsync(user);

                    if (Result.Succeeded)
                        Response.Success(true, 200);
                    else
                        Response.Error(400, "Operation Failed.");
                }
                else
                    Response.Error(400, "Token Error!");
                
                Log.Logger.Information($"{nameof(AccountService)}.{nameof(ChangePassword)} mehod is executed");

            }
            catch(Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;

        }

        public async Task<GenericResponse<ShowProfileDTO>> ShowMyProfile(ClaimsPrincipal userModel, IWebHostEnvironment environment)
        {
            var Response = new GenericResponse<ShowProfileDTO>();
            try
            {
                Member loginedUsed = (Member)await _userManager.GetUserAsync(userModel);
                var ProfileInfo = _mapper.Map<ShowProfileDTO>(loginedUsed);
                if(loginedUsed.Photo  != null)
                {
                    var folderPath = Path.Combine(environment.WebRootPath, loginedUsed.UserName);
                    var filePath = Path.Combine(folderPath, loginedUsed.Photo);
                    byte[] data = default(byte[]);
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        data = new byte[fs.Length];
                        await fs.ReadAsync(data, 0, data.Length);
                    }
                    ProfileInfo.Photo = Convert.ToBase64String(data);
                }
                
                Response.Success(ProfileInfo, 200);
                Log.Logger.Information($"{nameof(AccountService)}.{nameof(ShowMyProfile)} mehod is executed");
            }
            catch(Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }
    }
}
