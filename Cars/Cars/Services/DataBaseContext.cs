using Cars.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Services
{
    public class DataBaseContext : DbContext
    {
        private string _filename;
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Engine> Engines { get; set; }

        public DbSet<BodyType> BodyTypes { get; set; }

        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        public DataBaseContext(string filename)
        {
            _filename = filename;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_filename}");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
