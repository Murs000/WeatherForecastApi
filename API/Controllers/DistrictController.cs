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
        public async Task<ActionResult<List<DistrictDTO>>> GetDistricts()
        {
            var districts = await serviceUnitOfWork.DistrictService.Get();
            return Ok(districts);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDistrict([FromBody] DistrictDTO model)
        {
            if (model == null)
            {
                return BadRequest("District data is null.");
            }

            await serviceUnitOfWork.DistrictService.Insert(model);
            return Ok("District inserted successfully.");
        }

        [HttpPost("Upload")]
        public async Task<ActionResult<List<DistrictDTO>>> UploadDistrictAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            byte[] xmlFile;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                xmlFile = memoryStream.ToArray();
            }

            var districts = await xmlService.GetDistrictsAsync(xmlFile);
            return Ok(districts);
        }
}
