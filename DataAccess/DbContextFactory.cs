using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace DataAccess
{
    public class DbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            const string Cadena_Conexion = "Colocarla Para Migraciones";
            optionsBuilder.UseSqlServer(Cadena_Conexion);

            return new MyDbContext(optionsBuilder.Options);
        }
    }
}
