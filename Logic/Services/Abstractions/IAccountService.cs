using Logic.Models.DTOs.Admin;
using Logic.Models.DTOs.Member;
using Logic.Models.DTOs.Personel;
using Logic.Models.DTOs.Shared;
using Logic.Models.Response_Model;
using Logic.Models.Token;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Abstractions
{
    public interface IAccountService
    {
        Task<GenericResponse<TokenModel>> GetJwtTokenWithRefreshToken(string refreshToken);
        Task<GenericResponse<TokenModel>> Login(LoginDTO model,ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> CreateAdmin();
        Task<GenericResponse<bool>> RegisterMember(MemberRegisterDTO model, IWebHostEnvironment environment, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> RegisterPersonel(PersonelRegisterDTO model, IWebHostEnvironment environment, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> UpdateInformation(UpdateDTO model, ClaimsPrincipal userModel, IWebHostEnvironment environment, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> RecoveryPassword(RecoveryEmailDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<string>> CheckToken(string Token);
        Task<GenericResponse<bool>> ChangePassword(ChangePasswordDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<ShowProfileDTO>> ShowMyProfile(ClaimsPrincipal userModel, IWebHostEnvironment environment);

    }
}
