using System;
using System.Collections.Generic;

namespace AzureFuction.Empleado.Aplication.DTOs.Responses
{
    public class ResponseDTO<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T? Data { get; set; }
    }
}
