
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


        // LISTADO DE REGISTROS:
        public async Task<(int, List<Empleado>)> ListarEmpleados(string search, int page, int pageSize)
        {
            IQueryable<EmpleadoEntity> query = _dbContext.Empleados;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();
                bool isNumeric = int.TryParse(term, out int edadBuscada);

                query = query.Where(x =>
                    x.Nombre.ToLower().Contains(term) ||
                    x.Direccion.ToLower().Contains(term) ||
                    (isNumeric && x.Edad == edadBuscada)
                );
            }

            return await PaginarQuery(query, page, pageSize);
        }




        // MAPEA DE ENTIDAD A ENTITY-Obj:
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

        // MAPEA DE ENTITY A ENTIDAD-obj:
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

        // MAPEA DE ENTITY A ENTIDAD-list:
        private List<Empleado> MapListToEntidad(List<EmpleadoEntity> empleados)
        {
            List<Empleado> listEmpleados = new();

            foreach (EmpleadoEntity empleado in empleados)
            {
                listEmpleados.Add(new Empleado
                {
                    Id = empleado.Id,
                    Nombre = empleado.Nombre,
                    Edad = empleado.Edad,
                    Direccion = empleado.Direccion
                });
            }

            return listEmpleados;
        }

        // GENERA EL PAGINADO:
        private async Task<(int, List<Empleado>)> PaginarQuery(IQueryable<EmpleadoEntity> query, int page, int pageSize)
        {
            var totalCount = await query.CountAsync();

            List<EmpleadoEntity> registros = await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<Empleado> empleadosList = MapListToEntidad(registros);

            return (totalCount, empleadosList);
        }


    }
}
