using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IWeatherService
    {
        public Task<Report> GetWeatherAsync(double latitude, double longitude);
    }
}