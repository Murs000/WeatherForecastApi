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
        public async Task<ActionResult<List<ReportDTO>>> GetReports([FromQuery] ReportFilter filter, [FromQuery] Pagination pagination)
        {
            var reports = await serviceUnitOfWork.ReportService.Filter(filter, pagination);
            return Ok(reports);
        }

        [HttpPost]
        public async Task<IActionResult> WeatherCheck([FromBody] DistrictDTO model)
        {
            if (model == null)
            {
                return BadRequest("District data is null.");
            }

            var weather = await service.GetWeatherAsync(model.Lat, model.Lon);

            if (weather == null)
            {
                return NotFound("Weather data not found.");
            }

            var reportDTO = mapper.Map<ReportDTO>(weather);
            await serviceUnitOfWork.ReportService.Insert(reportDTO, model.Id);

            return Ok(weather);
        }
}
