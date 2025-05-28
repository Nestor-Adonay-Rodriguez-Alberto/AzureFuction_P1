using AzureFuction.Empleado.Aplication.DTOs.Models;
using AzureFuction.Empleado.Aplication.DTOs.Responses;
using AzureFuction.Empleado.Aplication.Services;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace AzureFuction.Empleado.Controllers
{
    public class EmpleadoFn
    {
        public readonly ILogger<EmpleadoFn> _Logger;
        public readonly IEmpleado _EmpleadoRepository;

        public EmpleadoFn(ILogger<EmpleadoFn> logger, IEmpleado empleado)
        {
            _Logger = logger;
            _EmpleadoRepository = empleado;
        }



        // CREAR - ACTUALIZAR:
        [Function("fn-save-empleado")]
        public async Task<IActionResult> CreateEmpleado([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "azure-fuction/crear-empleado")] HttpRequest req)
        {
            _Logger.LogInformation("Creando El Empleado...");
            EmpleadoService _service = new(_EmpleadoRepository);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var empleado = JsonConvert.DeserializeObject<EmpleadoDTO>(requestBody);

            try
            {
                ResponseDTO<EmpleadoDTO> response = await _service.CreateEmpleado(empleado);

                return new CreatedResult("Creacion Exitosa",response);
            }
            catch (Exception ex) 
            {
                _Logger.LogError($"Error al crear...: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);
            }
        }


        // OBTENER POR ID:
        [Function("fn-get-empleado")]
        public async Task<IActionResult> Obtener_PoId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "azure-fuction/get-empleado/{id}")] HttpRequest req, int id)
        {
            _Logger.LogInformation("Obteniendo por ID...");
            EmpleadoService _service = new(_EmpleadoRepository);

            try
            {
                ResponseDTO<EmpleadoDTO> response = await _service.Obtener_PoId(id);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error al obtener...: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);
            }

        }


        // ELIMINAR POR ID:
        [Function("fn-delete-empleado")]
        public async Task<IActionResult> EliminarEmpleado([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "azure-fuction/delete-empleado/{id}")] HttpRequest req, int id)
        {
            _Logger.LogInformation("Eliminando por ID...");
            EmpleadoService _service = new(_EmpleadoRepository);

            try
            {
                ResponseDTO<EmpleadoDTO> response = await _service.EliminarEmpleado(id);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error al Eliminar...: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);
            }

        }


        // LISTADO DE REGISTROS:
        [Function("fn-list-empleados")]
        public async Task<IActionResult> ListarEmpleados([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "azure-fuction/list-empleado")] HttpRequest req)
        {
            _Logger.LogInformation("Obteniendo Todos Los Empleados...");
            EmpleadoService _service = new(_EmpleadoRepository);

            string search = req.Query.ContainsKey("search") ? req.Query["search"].ToString() : string.Empty;
            int page = 1;
            int pageSize = 20;

            if (req.Query.ContainsKey("page") && int.TryParse(req.Query["page"], out int parsedPage))
            {
                page = parsedPage;
            }

            if (req.Query.ContainsKey("pageSize") && int.TryParse(req.Query["pageSize"], out int parsedPageSize))
            {
                pageSize = parsedPageSize;
            }

            try
            {
                PaginatedResponseDTO<List<EmpleadoDTO>> response = await _service.ListarEmpleados(search, page, pageSize);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error al obtener todos los registros...: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);
            }

        }


    }
}
