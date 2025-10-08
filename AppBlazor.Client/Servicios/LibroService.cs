using AppBlazor.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AppBlazor.Client.Services
{
    public class LibroService
    {
        private readonly HttpClient http;
        private List<LibroListCLS> lista;
        private TipoLibroService tipolibroservice;
        public LibroService(TipoLibroService _tipolibroService, HttpClient _http)
        {
            http = _http;
            tipolibroservice = _tipolibroService;
            lista = new List<LibroListCLS>();
            //lista.Add(new LibroListCLS { idLibro = 1, titulo = "Caperucita Roja", nombretipolibro = "Cuento" });
            //lista.Add(new LibroListCLS { idLibro = 2, titulo = "Don Quijote de la Mancha", nombretipolibro = "Novela" });
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
        public void eliminarLibro(int idLibro)
        {
            var listaQueda = lista.Where(p => p.idlibro != idLibro).ToList();
            lista = listaQueda;
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
        public void guardarLibro(LibroFormCLS oLibroFormCLS)
        {

            if (oLibroFormCLS.idLibro == 0)
            {
                int idlibro = lista.Select(p => p.idlibro).Max() + 1;
                lista.Add(new LibroListCLS
                {
                    idlibro = idlibro,
                    titulo = oLibroFormCLS.titulo,
                    nombretipolibro = tipolibroservice.obtenerNombreTipoLibro(oLibroFormCLS.idtipolibro),
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
                    obj.nombretipolibro = tipolibroservice.obtenerNombreTipoLibro(oLibroFormCLS.idtipolibro);
                    obj.imagen = oLibroFormCLS.image;

                    obj.archivo = oLibroFormCLS.archivo;
                    obj.nombrearchivo = oLibroFormCLS.nombrearchivo;
                }
            }
        }
        public async Task<string> recuperarArchivoPorId(int idLibro)
        {
            try
            {
                var response = await http.GetFromJsonAsync<byte[]>("api/Libro/recuperarArchivo/" + idLibro);
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
        public event Func<string, Task> OnSearch = delegate { return Task.CompletedTask; };
        public async Task notificarBusqueda(string titulolibro)
        {
            if (OnSearch != null)
            {
                await OnSearch.Invoke(titulolibro);
            }
        }
    }
}