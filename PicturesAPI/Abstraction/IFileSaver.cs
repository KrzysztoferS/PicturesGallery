using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicturesAPI.Abstraction
{
    public interface IFileSaver
    {
        //saves files and returns url
        Task<string> SaveFile(IFormFile file, string name);
        Task<bool> DeleteFile(string url, string ownerId);
    }
}
