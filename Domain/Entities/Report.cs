using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  Domain.Entities
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public int WeatherId { get; set; }
        public string Main { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public float Temp { get; set; }
        public float FeelsLike { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
        public float SeaLevel { get; set; }
        public float GroundLevel { get; set; }
        public float WindSpeed { get; set; }
        public float WindDegree { get; set; }
        public float WindGust { get; set; }
        public float Clouds { get; set; }

        public int DisctrictId { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("DisctrictId")]
        public District District { get; set; }

        public void SetDate()
        {
            DateTime = DateTime.Now.ToUniversalTime();
        }
    }
}