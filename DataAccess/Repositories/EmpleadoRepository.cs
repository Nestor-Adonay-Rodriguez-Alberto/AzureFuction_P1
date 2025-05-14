using DataAccess.Persistence.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class EmpleadoRepository : IEmpleado<Empleado>
    {
        private readonly DbContext _dbContext;

        public EmpleadoRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task CreateEmpleado(Empleado empleado)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
               _dbContext.Empleados.Add(empleado);
               await _dbContext.SaveChangesAsync();
               await transaction.CommitAsync();
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error al agregar la reservación: {ex.Message}");
                throw;
            }

        }

        public Task EditarEmpleado(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public Task EliminarEmpleado(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public Task<List<Empleado>> ListarEmpleados()
        {
            throw new NotImplementedException();
        }

        public Task<Empleado> Obtener_PoId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
