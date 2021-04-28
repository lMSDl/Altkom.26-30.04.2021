using DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class Context : DbContext
    {
        public Context()
        {

        }

        public Context(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                var connectionString = "Data Source=(Local);Initial Catalog=WymianaDanych;Integrated Security=True;";

                optionsBuilder.UseSqlServer(connectionString);
            }
            else
            {
                optionsBuilder.UseSqlServer();
            }


            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RequestConfiguration());
            modelBuilder.ApplyConfiguration(new RequestRawConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorConfiguration());
        }
    }
}
