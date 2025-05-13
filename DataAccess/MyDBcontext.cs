using System;
using DataAccess.Persistence.Configuration;
using DataAccess.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MyDBcontext: DbContext
    {
        public MyDBcontext(DbContextOptions<MyDBcontext> options) 
            : base(options) 
        {
        }

        // Tablas En DB:
        public DbSet<Empleado> Empleados { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // T-Empleados
            modelBuilder.ApplyConfiguration(new EmpleadoConfiguration());
        }


    }

}
