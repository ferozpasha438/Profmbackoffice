using MediatR;
using Microsoft.EntityFrameworkCore;
using CIN.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CIN.Application.ProfmQuery
{

    public static class FileUploads            
    {
        public static (bool, string) FileUploadWithBase64Image(string fileName, string webRoot, string base64Image) //with base64Image
        {
            var guid = Guid.NewGuid().ToString();
            string FileName = $"{fileName}_{guid}";
            if (base64Image == null || base64Image.Length == 0 || string.IsNullOrEmpty(fileName))
            {

                return (false, "Image_Not_Found");
            }
            fileName = $"{FileName}{".jpeg"}";
            var filePath = Path.Combine(webRoot, fileName);
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            var base64File = regex.Replace(base64Image, string.Empty);
            byte[] imageBytes = Convert.FromBase64String(base64File);
            MemoryStream ipstream = new MemoryStream(imageBytes, 0, imageBytes.Length);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                ipstream.CopyTo(stream);
                return (true, fileName);
            }


        }


            public static (bool, string) FileUploadWithIform(string fileName, string webRoot, IFormFile IformImage)
            {
                var guid = Guid.NewGuid().ToString();
                string FileName = $"{fileName}_{guid}";
                var exten = System.IO.Path.GetExtension(IformImage.FileName);
                if (IformImage == null || IformImage.Length == 0 || string.IsNullOrEmpty(fileName))
                {

                    return (false, "Image_Not_Found");
                }
                fileName = $"{FileName}{exten}";
                var filePath = Path.Combine(webRoot, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                IformImage.CopyTo(stream);
                return (true, fileName);
            }

        }

        

    }
}
