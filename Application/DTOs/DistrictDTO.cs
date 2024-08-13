namespace Application.DTOs
{
    public class DistrictDTO
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public double Lat {get; set;}
        public double Lon {get; set;}
    }
}