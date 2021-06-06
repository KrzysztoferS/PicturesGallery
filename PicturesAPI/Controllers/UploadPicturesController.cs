﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PicturesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadPicturesController:ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            string path="none path";
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine("C:\\APIFiles",Path.GetRandomFileName());


                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    path = filePath;
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, path }) ;
        }

        [HttpPost("form")]
        public async Task<IActionResult> OnPostForm([FromForm] UploadFile file)
        {
            
                var filePath = Path.Combine("C:\\APIFiles", Path.GetRandomFileName());


                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.Picture.CopyToAsync(stream);
                }
                
            
            return Ok(new { file.Title, file.Picture.FileName  });
        }
    }

    public class UploadFile
    {
        public string Title { get; set; }
        public IFormFile Picture { get; set; }
    }
}