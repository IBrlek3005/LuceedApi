namespace Luceed_API.Models
{
    public class DohvatiArtikleResponse
    {
        public List<ResultArtikli>? result { get; set; }
    }

    public class ResultArtikli
    {
        public List<Artikli>? artikli { get; set; }
    }

    public class Artikli
    {
        public int? id { get; set; }
        public string? naziv { get; set; }
        public int? sid { get; set; }
    }
}
