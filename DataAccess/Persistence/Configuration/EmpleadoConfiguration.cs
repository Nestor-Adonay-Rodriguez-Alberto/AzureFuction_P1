using System;
using DataAccess.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccess.Persistence.Configuration
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<EmpleadoEntity>
    {
        public void Configure(EntityTypeBuilder<EmpleadoEntity> builder)
        {
            builder.ToTable("Empleados", "dbo"); 
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("id")
                   .IsRequired()
                   .ValueGeneratedOnAdd(); 

            builder.Property(e => e.Nombre)
                   .HasColumnName("nombre")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Edad)
                   .HasColumnName("edad")
                   .IsRequired()
                   .HasMaxLength(100); ;

            builder.Property(e => e.Direccion)
                   .HasColumnName("direccion")
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
