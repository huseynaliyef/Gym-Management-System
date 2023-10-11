using Data.Entities;
using Logic.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Background_Services
{
    public class CheckAppointmentService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private IGenericRepository<Appointment> _appointmentRepository;
        public CheckAppointmentService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                _appointmentRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Appointment>>();

                while(!stoppingToken.IsCancellationRequested)
                {
                    var IsDeletedAppointment = await _appointmentRepository.GetByExperssion(x => x.AppointmentDate.AddMonths(x.Duration) < DateTime.Now);
                    foreach (var appointment in IsDeletedAppointment)
                    {
                        _appointmentRepository.Delete(appointment);
                        await _appointmentRepository.Commit();
                    }

                    await Task.Delay(((1000) * 3600) * 24);
                }
            }
        }
    }
}
