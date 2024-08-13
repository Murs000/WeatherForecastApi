using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController(IWeatherService service,IServiceUnitOfWork serviceUnitOfWork,IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<ReportDTO>> GetReports([FromQuery] ReportFilter filter, [FromQuery] Pagination pagination)
    {
        return Ok(serviceUnitOfWork.ReportService.Filter(filter,pagination));
    }
    [HttpPost]
    public IActionResult WeatherCheck([FromBody] DistrictDTO model)
    {
        var weather = service.GetWeatherAsync(model.Lat, model.Lon);

        serviceUnitOfWork.ReportService.Insert(mapper.Map<ReportDTO>(weather),model.Id);

        return Ok(weather);
    }
}
