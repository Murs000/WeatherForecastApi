using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class District
    {
        [Key]
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public double Lat {get; set;}
        public double Lon {get; set;}
        public DateTime CreationDate {get; set;}
        public bool IsDeleted {get; set;}
        public List<Report>? Reports {get; set;}
    }
}