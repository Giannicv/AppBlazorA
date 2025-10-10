using AppBlazor.Client.Services;
using AppBlazor.Entities;
using System.ComponentModel;
using System.Net.Http.Json;
namespace AppBlazor.Client.Servicios
{
    public class LibroService
    {
        private List<LibroListCLS> lista;
        private TipoLibroService tipoLibroService;

        private readonly HttpClient http;

        public LibroService(TipoLibroService _tipolibroservice, HttpClient _http)
        {

            http = _http;
            tipoLibroService = _tipolibroservice;
            lista = new List<LibroListCLS>();
            // lista.Add(new LibroListCLS { idlibro=1, titulo="Caperucita Roja", nombretipolibro="Cuento" });
            // lista.Add(new LibroListCLS { idlibro = 2, titulo = "Don quijote de la Mancha ", nombretipolibro="Novela" });


        }


        public async Task<List<LibroListCLS>> listarLibros()
        {
            try
            {
                var response = await http.GetFromJsonAsync<List<LibroListCLS>>("api/Libro");
                if (response == null)
                {
                    return new List<LibroListCLS>();
                }
                else
                {
                    return response;
                }
            }
            catch
            {
                return new List<LibroListCLS>();
            }
        }



        public async Task<List<LibroListCLS>> filtrarLibros(string nombretitulo)
        {
            List<LibroListCLS> l = await listarLibros();
            if (nombretitulo == "")
            {
                return l;
            }
            else
            {
                List<LibroListCLS> listafiltrada = l.Where(p => p.titulo.ToUpper().Contains(nombretitulo.ToUpper())).ToList();
                return listafiltrada;
            }
        }


        public async Task<string> eliminarLibro(int idlibro)
        {
            var response = await http.DeleteAsync("api/Libro/" + idlibro);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "Error: " + await response.Content.ReadAsStringAsync();
            }
        }


        public async Task<LibroFormCLS> recuperarLibroPorId(int idLibro)

        {
            try
            {
                var response = await http.GetFromJsonAsync<LibroFormCLS>("api/Libro/" + idLibro);
                if (response == null)
                {
                    return new LibroFormCLS();
                }
                else
                {
                    return response;
                }
            }
            catch
            {
                return new LibroFormCLS();
            }
        }


        public async Task<string> recuperarArchivoPorId(int idlibro)
        {
            try
            {
                var response = await http.GetFromJsonAsync<byte[]>("api/Libro/recuperarArchivo/" + idlibro);
                if (response == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToBase64String(response);
                }
            }
            catch
            {
                return "";
            }
        }

        public void guardarLibro(LibroFormCLS oLibroFormCLS)
        {



            if (oLibroFormCLS.idLibro == 0)
            {
                int idLibro = lista.Select(p => p.idlibro).Max() + 1;

                lista.Add(new LibroListCLS
                {
                    idlibro = idLibro,
                    titulo = oLibroFormCLS.titulo,
                    nombretipolibro = tipoLibroService.obtenerNombreTipoLibro(oLibroFormCLS.idtipolibro),
                    imagen = oLibroFormCLS.image,

                    archivo = oLibroFormCLS.archivo,
                    nombrearchivo = oLibroFormCLS.nombrearchivo
                });
            }
            else
            {
                var obj = lista.Where(p => p.idlibro == oLibroFormCLS.idLibro).FirstOrDefault();
                if (obj != null)
                {
                    obj.titulo = oLibroFormCLS.titulo;
                    obj.nombretipolibro = tipoLibroService.obtenerNombreTipoLibro(oLibroFormCLS.idtipolibro);
                    obj.imagen = oLibroFormCLS.image;

                    obj.archivo = oLibroFormCLS.archivo;
                    obj.nombrearchivo = oLibroFormCLS.nombrearchivo;
                }
            }


        }





    }
}