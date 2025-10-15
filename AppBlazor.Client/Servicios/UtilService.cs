namespace AppBlazor.Client.Servicios
{
    public class UtilService
    {
        public string obternerImagen(byte[]? buffer)
        {
            if (buffer == null)
            {
                return "img/default.png";
            }
            else
            {
                return "data:image/png;base64," + Convert.ToBase64String(buffer);
            }
        }
    }
}
