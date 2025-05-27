using Domain.Entidades;
using System;

namespace Domain.Repositories
{
    public interface IEmpleado
    {
        Task CreateEmpleado(Empleado empleado);
        Task EliminarEmpleado(int id);
        Task<Empleado> Obtener_PoId(int Id);
        Task<List<Empleado>> ListarEmpleados();
    }
}
