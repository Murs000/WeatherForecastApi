using Application.Mappers;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Simpl;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

Configure(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddHttpClient("WeatherClient", client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["WeatherApi:BaseAddress"]);
    });

    builder.Services.AddQuartz(q =>
    {
        q.UseMicrosoftDependencyInjectionJobFactory(); 

        var cronSchedule = builder.Configuration["Quartz:Jobs:Job1:CronExpression"];

        q.ScheduleJob<WeatherJob>(trigger => trigger
            .WithIdentity("WeatherJobTrigger")
            .WithCronSchedule(cronSchedule));
    });

    services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    var mapperConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new DistrictMappingProfile());
        mc.AddProfile(new ReportMappingProfile());
    });

    IMapper mapper = mapperConfig.CreateMapper();
    services.AddSingleton(mapper);

    services.AddControllers();

    services.AddScoped<IWeatherService, WeatherService>();
    services.AddScoped<IXmlService, XmlService>();
    services.AddScoped<IRepositoryUnitOfWork, RepositoryUnitOfWork>();
    services.AddScoped<IServiceUnitOfWork, ServiceUnitOfWork>();
}

void Configure(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}