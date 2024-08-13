using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DistrictController(IServiceUnitOfWork serviceUnitOfWork,IXmlService xmlService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<DistrictDTO>> GetDistricts()
    {
        return Ok(serviceUnitOfWork.DistrictService.Get());
    }
    [HttpPost]
    public IActionResult InsertDistrict([FromBody] DistrictDTO model)
    {
        serviceUnitOfWork.DistrictService.Insert(model);
        return Ok();
    }
    [HttpPost("Upload")]
    public async Task<ActionResult<List<DistrictDTO>>> UploadDistrictAsync(IFormFile file)
    {
        byte[] xmlFile;
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            xmlFile = memoryStream.ToArray();
        }
        var districts = await xmlService.GetDistricts(xmlFile);
        return Ok(districts);
    }
}
