using AzureFuction.Empleado.Aplication.DTOs.Models;
using AzureFuction.Empleado.Aplication.DTOs.Responses;
using Domain.Entidades;
using Domain.Repositories;


namespace AzureFuction.Empleado.Aplication.Services
{
    public class EmpleadoService
    {
        public IEmpleado _EmpleadoRepository;

        public EmpleadoService(IEmpleado empleado)
        {
            _EmpleadoRepository = empleado;
        }


        public async Task<ResponseDTO<EmpleadoDTO>> CreateEmpleado(EmpleadoDTO empleadoDTO)
        {
            var newEmpleado = new Domain.Entidades.Empleado
            {
                Id = empleadoDTO.Id,
                Nombre = empleadoDTO.Nombre,
                Edad = empleadoDTO.Edad,
                Direccion = empleadoDTO.Direccion,
            };
            
            try
            {
                await _EmpleadoRepository.CreateEmpleado(newEmpleado);

                ResponseDTO<EmpleadoDTO> response = new()
                {
                    Message = newEmpleado.Id>0 ? "Se actualizo el Empleado":"Se Creo el Empleado",
                    Status = true,
                    Data = empleadoDTO
                };

                return response;
            }
            catch (Exception ex)
            { 
                throw;
            }
        }

        

    }
}
