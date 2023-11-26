using Luceed_API.Models;
using Luceed_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Luceed_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuceedController : ControllerBase
    {
        private readonly LuceedService _service;

        public LuceedController(LuceedService service)
        {
            _service = service;
        }

        [HttpGet("GetArtikle/{naziv}")]
        public async Task<IActionResult> DohvatiArtikle([FromRoute] string naziv)
        {
            var result = await _service.DohvatiArtikle(naziv);

            return Ok(result);
        }

        [HttpGet("GetObracunPoVrsti/{id}/{odDatuma}/{doDatuma?}")]
        public async Task<IActionResult> GetObracunPoVrsti([FromRoute] string id, DateTime odDatuma, DateTime? doDatuma)
        {
            var request = CreateObracunRequest(id, odDatuma, doDatuma);
            var result = await _service.DohvatObracun(request, true);
            return Ok(result);
        }

        [HttpGet("GetObracunArtikli/{id}/{odDatuma}/{doDatuma?}")]
        public async Task<IActionResult> GetObracunArtikli([FromRoute] string id, DateTime odDatuma, DateTime? doDatuma)
        {
            var request = CreateObracunRequest(id, odDatuma, doDatuma);
            var result = await _service.DohvatObracun(request, false);
            return Ok(result);
        }

        private Obracun CreateObracunRequest(string id, DateTime odDatuma, DateTime? doDatuma)
        {
            return new Obracun
            {
                Id = id,
                DatumOd = odDatuma.Date,
                DatumDo = doDatuma?.Date ?? DateTime.Now.Date
            };
        }
    }
}
