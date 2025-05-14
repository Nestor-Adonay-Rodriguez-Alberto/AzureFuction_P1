using System;

namespace Domain.Repositories
{
    public interface IEmpleado<T>
    {
        Task CreateEmpleado(T empleado);
        Task EditarEmpleado(T empleado);
        Task EliminarEmpleado(T empleado);
        Task<T> Obtener_PoId(int Id);
        Task<List<T>> ListarEmpleados();
    }
}
