using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicturesAPI.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
        public string email { get; set; }
    }
}
