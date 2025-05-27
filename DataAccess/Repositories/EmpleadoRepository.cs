
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



        // CREAR - ACTUALIZAR
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
                throw ex;
            }

        }


        // OBTENER POR ID:
        public async Task<Empleado> Obtener_PoId(int Id)
        {
            try
            {
                EmpleadoEntity empleadoEntity = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.Id == Id);
                Empleado empleado = MapToDomain(empleadoEntity);

                return empleado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // ELIMINA POR ID:
        public async Task EliminarEmpleado(int id)
        {
            try
            {
                EmpleadoEntity? empleadoEntity = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.Id == id);

                if (empleadoEntity == null)
                {
                    throw new InvalidOperationException($"No se encontró el empleado con ID {id}.");
                }

                _dbContext.Empleados.Remove(empleadoEntity);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // OBTENER TODOS:
        public Task<List<Empleado>> ListarEmpleados()
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
