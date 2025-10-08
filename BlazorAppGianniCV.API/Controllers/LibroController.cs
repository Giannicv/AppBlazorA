using AppBlazor.Entities;
using BlazorAppGianniCV.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppGianniCV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly BdbibliotecaContext bd;
        public LibroController(BdbibliotecaContext _bd)
        {
            bd = _bd;
        }
        [HttpGet]
        public IActionResult listarLibros()
        {
            try
            {
                var lista = (from libro in bd.Libros
                             join tipolibro in bd.TipoLibros
                             on libro.Iidtipolibro equals tipolibro.Iidtipolibro
                             where libro.Bhabilitado == 1
                             select new LibroListCLS
                             {
                                 idlibro = libro.Iidlibro,
                                 titulo = libro.Titulo!,
                                 imagen = libro.Fotocaratula,
                                 nombrearchivo = libro.Nombrearchivo!,
                                 nombretipolibro = tipolibro.Nombretipolibro!
                             }).ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{idlibro}")]
        public IActionResult recuperaLibroPorId(int idlibro)
        {
            try
            {
                var obj = bd.Libros.Where(p => p.Iidlibro == idlibro).FirstOrDefault();
                if (obj == null)
                {
                    return NotFound();
                }
                else
                {
                    LibroFormCLS oLibroFormCLS = new LibroFormCLS();
                    oLibroFormCLS.idLibro = obj.Iidlibro;
                    oLibroFormCLS.titulo = obj.Titulo!;
                    oLibroFormCLS.resumen = obj.Resumen!;
                    oLibroFormCLS.idtipolibro = (int)obj.Iidtipolibro!;
                    oLibroFormCLS.nombrearchivo = obj.Nombrearchivo!;
                    oLibroFormCLS.archivo = obj.Libropdf;
                    oLibroFormCLS.image = obj.Fotocaratula;
                    return Ok(oLibroFormCLS);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{idLibro}")]
        public IActionResult eliminarLibro(int idLibro)
        {
            try
            {
                var obj = bd.Libros.Where(p => p.Iidlibro == idLibro).FirstOrDefault();
                if (obj == null)
                {
                    return NotFound();
                }
                else
                {
                    obj.Bhabilitado = 0;
                    bd.SaveChanges();
                    return Ok("Se elimino correctamente");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult guardarLibro([FromBody] LibroFormCLS oLibroFormCLS)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("recuperarArchivo/{idLibro}")]
        public IActionResult recuperarArchivoPorId(int idLibro)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    } 
}

