using Luceed_API.Models;
using Luceed_API.Options;
using Microsoft.Extensions.Options;
using System.Text;

namespace Luceed_API.Services
{
    public class LuceedService : HttpService
    {
        private readonly LuceedOptions _options;
        private string endpoint;
        public LuceedService(IHttpClientFactory clientFactory,
            IOptions<LuceedOptions> options)
            : base(clientFactory)
        {
            _options = options.Value;
            endpoint = string.Empty;
        }

        public async Task<List<Artikli>> DohvatiArtikle(string naziv)
        {
            ArgumentNullException.ThrowIfNull(naziv, nameof(naziv));

            endpoint = string.Format("{0}artikli/naziv/{1}",_options.URL, naziv);
            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.Username}:{_options.Password}"));

            var response = await ExecuteGetWithResponse<DohvatiArtikleResponse>(_options.LuceedIntegration, endpoint, credentials);

            if (response?.result != null)
            {
                var allArtikli = response.result.SelectMany(resultArtikli => resultArtikli.artikli ?? Enumerable.Empty<Artikli>());

                return allArtikli.ToList();
            }

            return new List<Artikli>();
        }

        public async Task<object> DohvatObracun(Obracun request, bool isVrstaObracuna)
        {
            ValidateObracunRequest(request);

            string action = isVrstaObracuna ? "mpobracun/placanja" : "mpobracun/artikli";

            BuildEndpoint(action, request);

            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.Username}:{_options.Password}"));

            if (isVrstaObracuna)
            {
                var response = await ExecuteGetWithResponse<ObracunPoVrstiResponse>(_options.LuceedIntegration, endpoint, credentials);

                if(response?.result != null)
                {
                    var allObracunPlacanja = response.result.SelectMany(resultArtikli => resultArtikli.obracun_placanja ?? Enumerable.Empty<ObracunPlacanja>());
                    return allObracunPlacanja.ToList();
                }
                return new object[] { new ObracunPlacanja() };
            }
            else
            {
                var response = await ExecuteGetWithResponse<ObracunArtikliResponse>(_options.LuceedIntegration, endpoint, credentials);

                if (response?.result != null)
                {
                    var allObracunArtikl = response.result.SelectMany(resultArtikli => resultArtikli.obracun_artikli ?? Enumerable.Empty<ObracunArtikl>());
                    return allObracunArtikl.ToList();
                }
                return new object[] { new ObracunArtikl() };
            }
        }

        private void ValidateObracunRequest(Obracun request)
        {
            ArgumentNullException.ThrowIfNull(request.Id, nameof(request.Id));

            if (request.DatumOd == null)
            {
                throw new Exception("Potrebno je unijeti polje od kojeg datuma želite dohvatiti obračun.");
            }

            if (request.DatumOd >= request.DatumDo)
            {
                throw new Exception("Polje datum do mora biti veće od datuma od.");
            }
        }

        private void BuildEndpoint(string action, Obracun request)
        {
            endpoint = string.Format("{0}{1}/{2}/{3}/{4}",
                            _options.URL,
                            action,
                            request.Id,
                            request.DatumOd.Date.ToString("dd.MM.yyyy"),
                            request.DatumDo?.Date.ToString("dd.MM.yyyy"));
        }
    }
}
