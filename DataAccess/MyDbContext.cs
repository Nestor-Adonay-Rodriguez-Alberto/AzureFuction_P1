using System;
using DataAccess.Persistence.Configuration;
using DataAccess.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MyDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) 
            : base(options) 
        {
        }

        // Tablas En DB:
        public DbSet<EmpleadoEntity> Empleados { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // T-Empleados
            modelBuilder.ApplyConfiguration(new EmpleadoConfiguration());
        }


    }

}
