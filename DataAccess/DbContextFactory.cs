using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace DataAccess
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DbContext>
    {
        public DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            const string Cadena_Conexion = "Colocarla Para Migraciones";
            optionsBuilder.UseSqlServer(Cadena_Conexion);

            return new DbContext(optionsBuilder.Options);
        }
    }
}
