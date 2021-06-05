using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicturesAPI.Models
{
    public class PictureDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public DateTime DateAdded { get; set; }

        public Guid OwnerId { get; set; }
    }
}
