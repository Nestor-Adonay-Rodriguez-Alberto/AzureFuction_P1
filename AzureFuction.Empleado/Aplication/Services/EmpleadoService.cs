using AzureFuction.Empleado.Aplication.DTOs.Models;
using AzureFuction.Empleado.Aplication.DTOs.Responses;
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
            var empleado = new Domain.Entidades.Empleado
            {
                Id = empleadoDTO.Id,
                Nombre = empleadoDTO.Nombre,
                Edad = empleadoDTO.Edad,
                Direccion = empleadoDTO.Direccion,
            };

            

            try
            {
                await _EmpleadoRepository.CreateEmpleado(empleado);

                ResponseDTO<EmpleadoDTO> response = new()
                {
                    Message = "Exito. Se Creo Corectamente",
                    Status = true,
                    Data = empleadoDTO
                };

                return response;
            }
            catch (Exception ex) 
            {
                ResponseDTO<EmpleadoDTO> response = new()
                {
                    Message = "Error. No se pudo crear",
                    Status = false,
                    Data = empleadoDTO
                };

                return response;
            }
        }

        

    }
}
