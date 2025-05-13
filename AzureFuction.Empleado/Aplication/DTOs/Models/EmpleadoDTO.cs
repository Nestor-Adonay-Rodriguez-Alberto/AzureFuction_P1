using System;
using System.Collections.Generic;

namespace AzureFuction.Empleado.Aplication.DTOs.Models
{
    public class EmpleadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
    }
}
