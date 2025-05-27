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


    }
}
