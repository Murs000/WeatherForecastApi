using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IXmlService
    {
        public Task<List<District>> GetDistrictsAsync(byte[] xmlFile);
    }
}