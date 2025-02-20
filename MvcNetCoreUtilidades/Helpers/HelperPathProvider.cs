using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Routing.Constraints;

namespace MvcNetCoreUtilidades.Helpers
{
    //  ESTA CLASE SE ENCARGARA DE PROPORCIONAR LA RUTA A LAS CARPETAS DE NUESTROS SERVERS

    //  VAMOS A OFRECER EN PROGRACION UNA ENUMERACION
    //  CON LAS CARPETAS DE NUESTRO SERVIDOR
    public enum Folders { Images, Facturas, Uploads, Temporal }
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        private IServer server;
        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IServer server)
        {
            this.hostEnvironment = hostEnvironment;
            this.server = server; 
        }

        public string MapPath(string fileName, Folders folders)
        {
            string carpeta = "";
            if (folders == Folders.Images)
            {
                carpeta = "images";
            }else if (folders == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folders == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folders == Folders.Temporal)
            {
                carpeta = "temp";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }
        public string MapUrlPath(string fileName, Folders folders)
        {
            string carpeta = "";
            if (folders == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folders == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folders == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folders == Folders.Temporal)
            {
                carpeta = "temp";
            }
            var address = this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = address.FirstOrDefault();
            string urlPath = serverUrl + "/"+carpeta + "/"+fileName;
            //string path = Path.Combine(urlPath, carpeta, fileName);
            return urlPath;
        }

    }
}
