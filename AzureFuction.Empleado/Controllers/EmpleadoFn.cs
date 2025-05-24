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


        [Function("fn-save-empleado")]
        public async Task<IActionResult> CreateEmpleado([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "azure-fuction/crear-empleado")] HttpRequest req)
        {
            EmpleadoService _service = new(_EmpleadoRepository);

            _Logger.LogInformation("Creando El Empleado...");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var empleado = JsonConvert.DeserializeObject<EmpleadoDTO>(requestBody);

            try
            {
                ResponseDTO<EmpleadoDTO> response = await _service.CreateEmpleado(empleado);

                return new CreatedResult("Creacion Exitosa",response);
            }
            catch (Exception ex) 
            {
                _Logger.LogError($"CREAR FN.: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);
            }
        }



    }
}
