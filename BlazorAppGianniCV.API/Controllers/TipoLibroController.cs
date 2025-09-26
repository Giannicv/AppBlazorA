using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppGianniCV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLibroController : ControllerBase
    {
        [HttpGet]
        public IActionResult listarTipoLibros() {
            try {
                return Ok();
               
            }catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
