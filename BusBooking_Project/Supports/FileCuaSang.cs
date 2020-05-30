using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;

namespace Supports
{
    public class FileCuaSang
    {
        public static string SaveFile(IWebHostEnvironment webHostEnvironment, IFormFile fileupload, string location)
        {
            try
            {
                string nameFinal = DateTime.Now.Ticks + fileupload.FileName;
                string fileName = $"{webHostEnvironment.WebRootPath}\\" + location + $"\\{nameFinal}";
                using (FileStream fs = File.Create(fileName))
                {
                    fileupload.CopyTo(fs);
                    fs.Flush();
                }
                return nameFinal;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static bool RemoveFile(IWebHostEnvironment webHostEnvironment, string filename)
        {
            try
            {
                string pathName = $"{webHostEnvironment.WebRootPath}\\assets\\imageUser\\{filename}";
                File.Delete(pathName);
                //using (FileStream fs = new FileStream(pathName, FileMode.Open))
                //{
                //    File.Delete(pathName);
                //    fs.Flush();
                //}
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}
