using System;
using System.Collections.Generic;


namespace AzureFuction.Empleado.Aplication.DTOs.Responses
{
    public class PaginatedResponseDTO<T> : ResponseDTO<T>
    {
        public int Count { get; set; }
    }
}
