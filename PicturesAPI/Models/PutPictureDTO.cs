using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicturesAPI.Models
{
    public class PutPictureDTO
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
    }
}
