using AppBlazor.Entities;
using BlazorAppGianniCV.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// Importar el namespace de logging
using Microsoft.Extensions.Logging;

namespace BlazorAppGianniCV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly BdbibliotecaContext bd;

        // 1. Declarar la variable del Logger
        private readonly ILogger<LibroController> _logger;

        public LibroController(BdbibliotecaContext _bd, ILogger<LibroController> logger)
        {
            bd = _bd;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult listarLibros()
        {
            try
            {
                _logger.LogInformation("Iniciando la recuperación de la lista de libros."); //LOG 

                var lista = (from libro in bd.Libros
                             join tipolibro in bd.TipoLibros
                             on libro.Iidtipolibro equals tipolibro.Iidtipolibro
                             join autor in bd.Autors
                             on libro.Iidautor equals autor.Iidautor
                             where libro.Bhabilitado == 1
                             select new LibroListCLS
                             {
                                 idlibro = libro.Iidlibro,
                                 titulo = libro.Titulo!,
                                 imagen = libro.Fotocaratula,
                                 nombrearchivo = libro.Nombrearchivo!,
                                 nombretipolibro = tipolibro.Nombretipolibro!,
                                 nombreautor=autor.Nombre + " " + autor.Appaterno + " " + autor.Apmaterno,
                                 idtipolibro=(int) libro.Iidtipolibro!,
                                 idautor=(int) libro.Iidautor!
                             }).ToList();
                _logger.LogInformation("Se recuperaron {Count} libros con éxito.", lista.Count); //LOG
                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR CRÍTICO al listar libros.");
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
                    oLibroFormCLS.idautor=(int)obj.Iidautor!;
                    oLibroFormCLS.numeropaginas = (int)obj.Numpaginas!;
                    oLibroFormCLS.stock = (int)obj.Stock!;
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
                    _logger.LogWarning("Intento de eliminación fallido: Libro no encontrado con ID: {Id}", idLibro); //Log Warning
                    return NotFound();
                   
                }
                else
                {
                    obj.Bhabilitado = 0;
                    bd.SaveChanges();

                    _logger.LogInformation("Libro eliminado lógicamente. ID: {Id}", idLibro); //Log Eliminación
                    return Ok("Se elimino correctamente");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR al intentar eliminar el libro ID: {Id}", idLibro); //Log de Error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult guardarLibro([FromBody] LibroFormCLS oLibroFormCLS)
        {
            try
            {
                if (oLibroFormCLS.idLibro == 0)
                {
                    Libro oLibro = new Libro();
                    oLibro.Titulo = oLibroFormCLS.titulo;
                    oLibro.Resumen = oLibroFormCLS.resumen;
                    oLibro.Numpaginas = oLibroFormCLS.numeropaginas;
                    oLibro.Stock = oLibroFormCLS.stock;
                    oLibro.Iidtipolibro = oLibroFormCLS.idtipolibro;
                    oLibro.Iidautor = oLibroFormCLS.idautor;
                    oLibro.Fotocaratula = oLibroFormCLS.image;
                    oLibro.Libropdf = oLibroFormCLS.archivo;
                    oLibro.Nombrearchivo = oLibroFormCLS.nombrearchivo;
                    oLibro.Bhabilitado = 1;
                    bd.Libros.Add(oLibro);
                    bd.SaveChanges();
                    _logger.LogInformation("NUEVO LIBRO creado. Título: {Titulo}", oLibroFormCLS.titulo); //Log Creación
                }
                else
                {
                    var obj = bd.Libros.Where(p => p.Iidlibro == oLibroFormCLS.idLibro).FirstOrDefault();
                    if (obj == null)
                    {
                        _logger.LogWarning("Intento de actualización fallido: Libro no encontrado con ID: {Id}", oLibroFormCLS.idLibro); //Log Warning
                        return NotFound();
                    }
                    else
                    {
                        obj.Titulo = oLibroFormCLS.titulo;
                        obj.Resumen = oLibroFormCLS.resumen;
                        obj.Iidtipolibro = oLibroFormCLS.idtipolibro;
                        obj.Nombrearchivo = oLibroFormCLS.nombrearchivo;
                        obj.Libropdf = oLibroFormCLS.archivo;
                        obj.Fotocaratula = oLibroFormCLS.image;
                        obj.Iidautor = oLibroFormCLS.idautor;
                        obj.Numpaginas = oLibroFormCLS.numeropaginas;
                        obj.Stock = oLibroFormCLS.stock;
                        bd.SaveChanges();

                        _logger.LogInformation("Libro actualizado. ID: {Id}, Título: {Titulo}", oLibroFormCLS.idLibro, oLibroFormCLS.titulo); //Log Actualización
                        return Ok("Se actualizo correctamente");
                    }
                }
                return Ok("Se guardo correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR al guardar/actualizar el libro ID: {Id}", oLibroFormCLS.idLibro); //Log de Error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("recuperarArchivo/{idLibro}")]
        public IActionResult recuperarArchivoPorId(int idLibro)
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
                    return Ok(obj.Libropdf);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
    }
}

