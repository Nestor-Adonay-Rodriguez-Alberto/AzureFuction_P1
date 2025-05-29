using AzureFuction.Empleado.Aplication.DTOs.Models;
using AzureFuction.Empleado.Aplication.DTOs.Responses;
using DataAccess.Persistence.Models;
using Domain.Entidades;
using Domain.Repositories;
using System.Drawing.Printing;


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
            
            await _EmpleadoRepository.CreateEmpleado(newEmpleado);

            ResponseDTO<EmpleadoDTO> response = new()
            {
                Message = newEmpleado.Id > 0 ? "Se actualizo el Empleado" : "Se Creo el Empleado",
                Status = true,
                Data = empleadoDTO
            };

            return response;
        }


        // OBTENER POR ID:
        public async Task<ResponseDTO<EmpleadoDTO>> Obtener_PoId(int id)
        {
            Domain.Entidades.Empleado empleado = await _EmpleadoRepository.Obtener_PoId(id);
            EmpleadoDTO empleadoDTO = MapToDTO(empleado);

            ResponseDTO<EmpleadoDTO> response = new()
            {
                Message = "Empleado Obtenido",
                Status = true,
                Data = empleadoDTO
            };

            return response;
        }


        // ELIMINAR POR ID:
        public async Task<ResponseDTO<EmpleadoDTO>> EliminarEmpleado(int id)
        {
            await _EmpleadoRepository.EliminarEmpleado(id);

            ResponseDTO<EmpleadoDTO> response = new()
            {
                Message = "Empleado Eliminado",
                Status = true,
                Data = null
            };

            return response;
        }



        // LISTADO DE REGISTROS:
        public async Task<PaginatedResponseDTO<List<EmpleadoDTO>>> ListarEmpleados(string search, int page, int pageSize)
        {
            var (totalCount, listItems) = await _EmpleadoRepository.ListarEmpleados(search, page, pageSize);

            List<EmpleadoDTO> listaEmpleados = MapListToDTO(listItems);

            PaginatedResponseDTO<List<EmpleadoDTO>> response = new()
            {
                Message = "Lista Empleados Obtenida",
                Status = true,
                Data = listaEmpleados,
                Count = totalCount,
                CurrentPage = page,
                PageSize = pageSize
            };

            return response;
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


        // Lista Entidad a DTO:
        private List<EmpleadoDTO> MapListToDTO(List<Domain.Entidades.Empleado> empleados)
        {
            List<EmpleadoDTO> listEmpleadoDTO = new();

            foreach (Domain.Entidades.Empleado empleado in empleados)
            {
                listEmpleadoDTO.Add(new EmpleadoDTO
                {
                    Id = empleado.Id,
                    Nombre = empleado.Nombre,
                    Edad = empleado.Edad,
                    Direccion = empleado.Direccion
                });
            }

            return listEmpleadoDTO;
        }


    }
}
