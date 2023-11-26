namespace Luceed_API.Models
{
    public class ObracunArtikliResponse
    {
        public List<ResultObracunArtikl>? result { get; set; }
    }

    public class ResultObracunArtikl
    {
        public List<ObracunArtikl>? obracun_artikli { get; set; }
    }

    public class ObracunArtikl
    {
        public string? artikl_uid { get; set; }
        public string? naziv_artikla { get; set; }
        public decimal? kolicina { get; set; }
        public decimal? iznos { get; set; }
        public char? usluga { get; set; }
    }
}
