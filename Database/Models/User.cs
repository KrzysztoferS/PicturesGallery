using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
        public string email { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
