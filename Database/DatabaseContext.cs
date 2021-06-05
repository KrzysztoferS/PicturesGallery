using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public DatabaseContext(DbContextOptions opt):base(opt)
        {
        }

        protected DatabaseContext()
        {
        }


    }
}
