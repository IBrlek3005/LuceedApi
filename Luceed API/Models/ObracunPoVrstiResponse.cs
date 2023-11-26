namespace Luceed_API.Models
{
    public class ObracunPoVrstiResponse
    {
        public List<ResultObracunVrstaPlacanja>? result { get; set; }
    }

    public class ResultObracunVrstaPlacanja
    {
        public List<ObracunPlacanja>? obracun_placanja { get; set; }
    }

    public class ObracunPlacanja
    {
        public string? vrste_placanja_uid { get; set; }
        public string? naziv { get; set; }
        public decimal? iznos { get; set;}
    }
}
