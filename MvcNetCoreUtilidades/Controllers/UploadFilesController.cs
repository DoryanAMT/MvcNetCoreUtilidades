﻿using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;
using System.Runtime.InteropServices;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helperPath;
        public UploadFilesController(HelperPathProvider helperPath)
        {
            this.helperPath = helperPath;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SubirFichero()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFichero
            (IFormFile fichero)
        {
            ////  NECESITAMOS LA RUTA A NUESTRO WWROOT DEL SERVER
            //string rootFolder = this.hostEnvironment.WebRootPath;

            //  COMENZAMOS ALMACENANDO EL FICHERO EN LOS
            //  ELEMENTOS TEMPORALES
            //string tempoFolder = Path.GetTempPath();
            string fileName = fichero.FileName;
            //  CUANDO HABLAMOS DE FICHEROS Y DE RUTAS DE SISTEMAS
            //  ESTAMOS PENSANDO EL LO SIGUIENTE
            //  C:\miruta\carpeta\file.TXT
            //  TENEMOS QUE TENER EN CUENTA QUE ESTAMOS DENTRO DE NET CORE
            //  PODEMOS MONTAR EL SERVIDOR DONDE DESEEMOS
            //  C:/miruta/carpeta/file.txt
            //  ..miruta/carpeta/file.txt
            //  LAS RUTAS DE FICHEROS NO DEBO ESCRIBIRLAS, TENGO QUE GENERAR
            //  DICHAS RUTAS CON EL SISTEMA DONDE ESTOY TRABAJANDO
            //string path = Path.Combine(tempoFolder, fileName);
            //string path = Path.Combine(rootFolder, "uploads", fileName);
            string path = this.helperPath.MapPath(fileName, Folders.Images);
            string urlPath = this.helperPath.MapUrlPath(fileName, Folders.Images);
            //  PARA SUBIR EL FICHERO SE UTLIZA Stream CON IFormFile
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["URL"] = urlPath;
            return View();
        }
    }
}
