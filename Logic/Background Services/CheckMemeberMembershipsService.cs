using Data.Entities;
using Logic.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Logic.Background_Services
{
    public class CheckMemeberMembershipsService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private IGenericRepository<MemberMembership> _memberMembershipRepository;
        public CheckMemeberMembershipsService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                _memberMembershipRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<MemberMembership>>();
                while (!stoppingToken.IsCancellationRequested)
                {

                    var IsDeletedMemberships = await _memberMembershipRepository.GetByExperssion(x=>x.EndDate < DateTime.Now);
                    foreach(var membership in IsDeletedMemberships)
                    {
                        membership.IsActive = false;
                        _memberMembershipRepository.Update(membership);
                        await _memberMembershipRepository.Commit();
                    }
                    await Task.Delay(((1000)*3600)*24);
                }
            }
        }
    }
}
