using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure.Configurations
{
    public static class QuartzConfiguration
    {
        public static void ConfigureQuartz(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory(); 

                var quartzSettings = configuration.GetSection("Quartz:Jobs");

                var job1CronExpression = quartzSettings.GetSection("Job1:CronExpression").Value;
                q.ScheduleJob<WeatherJob>(trigger => trigger
                    .WithIdentity("Job1")
                    .StartNow()
                    .WithCronSchedule(job1CronExpression));
            });
        }
    }
}
