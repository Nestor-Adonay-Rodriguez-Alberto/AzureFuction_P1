
using DataAccess.Persistence.Models;
using Domain.Entidades;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class EmpleadoRepository : IEmpleado
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
                EmpleadoEntity entity = MapToEntity(empleado);
                _dbContext.Empleados.Add(entity);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error al crear el Empleado: {ex.Message}");
                throw;
            }

        }


        public async Task EditarEmpleado(Empleado empleado)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                EmpleadoEntity? objetoObtenido = await _dbContext.Empleados.FindAsync(empleado.Id);
                _dbContext.Entry(objetoObtenido).CurrentValues.SetValues(empleado);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();                 
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error al Actualizar el Empleado: {ex.Message}");
                throw;
            }
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


       
        private EmpleadoEntity MapToEntity(Empleado empleado)
        {
            EmpleadoEntity empleadoEntity = new()
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Edad = empleado.Edad,
                Direccion = empleado.Direccion
            };

            return empleadoEntity;
        }

    }
}
