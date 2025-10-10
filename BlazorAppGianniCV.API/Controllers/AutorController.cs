using AppBlazor.Entities;
using BlazorAppGianniCV.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace BlazorAppGianniCV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly BdbibliotecaContext bd;

        public AutorController(BdbibliotecaContext _bd)
        {
            this.bd = _bd;
        }

        [HttpGet]
        public IActionResult listarAutor()
        {
            try
            {
                var lista = (from autor in bd.Autors
                             where autor.Bhabilitado == 1
                             select new AutorCLS
                             {
                                 idautor = autor.Iidautor,
                                 nombreautor = autor.Nombre + " " + autor.Appaterno + " " + autor.Apmaterno
                             }).ToList();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}