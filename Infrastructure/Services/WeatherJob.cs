using Quartz;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Services
{
    public class WeatherJob(IWeatherService service,IRepositoryUnitOfWork repository) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var models = await repository.DistrictRepository.GetAllAsync();

            foreach(var model in models)
            {
                var weather = await service.GetWeatherAsync(model.Lat, model.Lon);
                weather.DisctrictId = model.Id;
                weather.SetDate();

                await repository.ReportRepository.AddAsync(weather);
            }
            await Task.CompletedTask;
        }
    }
}