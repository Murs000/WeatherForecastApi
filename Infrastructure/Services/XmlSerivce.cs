using System.Xml.Linq;
using System.IO;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Services
{
    public class XmlService(IRepositoryUnitOfWork repository) : IXmlService
    {
        public async Task<List<District>> GetDistrictsAsync(byte[] xmlFile)
        {
            var districts = new List<District>();

            using (var stream = new MemoryStream(xmlFile))
            {
                var doc = XDocument.Load(stream);
                XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

                foreach (var row in doc.Descendants(ss + "Row").Skip(1))
                {
                    var cells = row.Elements(ss + "Cell").ToList();
                    if (cells.Count >= 4) // Ensure there are enough cells to read from
                    {
                        var district = new District
                        {
                            Id = int.Parse(cells[0].Element(ss + "Data")?.Value ?? "0"),
                            Name = cells[1].Element(ss + "Data")?.Value,
                            Lat = float.Parse(cells[2].Element(ss + "Data")?.Value ?? "0"),
                            Lon = float.Parse(cells[3].Element(ss + "Data")?.Value ?? "0")
                        };
                        districts.Add(district);
                    }
                }
            }

            foreach (var district in districts)
            {
                await repository.DistrictRepository.AddAsync(district);
            }

            return districts;
        }
    }
}