using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicturesAPI.Models
{
    public class PictureDTO
    {
        //zakomentowane sa wlasciwosci ustalane przes serwisy lub baze danych nie przez klienta
        //public Guid Id { get; set; }

        public string Title { get; set; }
        //public string Url { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        //public DateTime DateAdded { get; set; }
        public IFormFile File { get; set; }

        //public Guid OwnerId { get; set; }
    }
}
