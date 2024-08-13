using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations
{
    public static class HttpClientConfiguration
    {
        public static void ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            var baseAddress = configuration["WeatherApi:BaseAddress"];

            services.AddHttpClient("WeatherClient", client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
        }
    }
}
