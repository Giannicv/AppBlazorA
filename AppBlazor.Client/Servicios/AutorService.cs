using AppBlazor.Entities;
using System.Net.Http.Json;

namespace AppBlazor.Client.Servicios
{
    public class AutorService
    {
        private readonly HttpClient http;
        public AutorService(HttpClient _http)
        {
            this.http = _http;
        }
        public async Task<List<AutorCLS>> listarAutores()
        {
            try
            {
                var response = await http.GetFromJsonAsync<List<AutorCLS>>("api/Autor");
                if (response == null) {
                    return new List<AutorCLS>();
                }
                else
                {
                    return response;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
