using Microsoft.AspNetCore.Http;
using PicturesAPI.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PicturesAPI.Services
{
    public class FileSaverLocallyService : IFileSaver
    {
        public async Task<string> SaveFile(IFormFile file, string ownerId)
        {
            if (file != null)
            {
                var filePath = Path.Combine("C:\\APIFiles", Path.GetRandomFileName());

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return filePath;
            }
            else return "No file provided";
        }
    }
}
