
using DataAccess.Persistence.Models;
using Domain.Entidades;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class EmpleadoRepository : IEmpleado
    {
        private readonly MyDbContext _dbContext;

        public EmpleadoRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task CreateEmpleado(Empleado newEmpleado)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (newEmpleado.Id == 0) 
                {
                    EmpleadoEntity entity = MapToEntity(newEmpleado);
                    _dbContext.Empleados.Add(entity);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {   
                    var existingEmpleado = newEmpleado.Id > 0 ? 
                        await _dbContext.Empleados.FirstOrDefaultAsync(x=> x.Id==newEmpleado.Id) : null;

                    EmpleadoEntity entity = MapToEntity(newEmpleado);
                    _dbContext.Entry(existingEmpleado).CurrentValues.SetValues(entity);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"CREAR DB: {ex.Message}");
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

        public async Task<Empleado> Obtener_PoId(int Id)
        {
            try
            {
                EmpleadoEntity empleadoEntity = await _dbContext.Empleados.FirstOrDefaultAsync(x=> x.Id == Id);
                Empleado empleado = MapToDomain(empleadoEntity);

                return empleado;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error al Obtener el Empleado: {ex.Message}");
                throw;
            }
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

        private Empleado MapToDomain(EmpleadoEntity empleadoEntity)
        {
            Empleado empleado = new()
            {
                Id = empleadoEntity.Id,
                Nombre = empleadoEntity.Nombre,
                Edad = empleadoEntity.Edad,
                Direccion = empleadoEntity.Direccion
            };

            return empleado;
        }


    }
}
