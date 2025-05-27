using AzureFuction.Empleado.Aplication.DTOs.Models;
using AzureFuction.Empleado.Aplication.DTOs.Responses;
using DataAccess.Persistence.Models;
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


        // CREAR - ACTUALIZAR
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
                    Message = newEmpleado.Id > 0 ? "Se actualizo el Empleado":"Se Creo el Empleado",
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


        // OBTENER POR ID:
        public async Task<ResponseDTO<EmpleadoDTO>> Obtener_PoId(int id)
        {
            try
            {
                Domain.Entidades.Empleado empleado =  await _EmpleadoRepository.Obtener_PoId(id);
                EmpleadoDTO empleadoDTO = MapToDTO(empleado);

                ResponseDTO<EmpleadoDTO> response = new()
                {
                    Message = "Empleado Obtenido",
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


        // Entidad a DTO:
        private EmpleadoDTO MapToDTO(Domain.Entidades.Empleado empleado)
        {
            EmpleadoDTO empleadoDTO = new()
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Edad = empleado.Edad,
                Direccion = empleado.Direccion
            };

            return empleadoDTO;
        }


    }
}
