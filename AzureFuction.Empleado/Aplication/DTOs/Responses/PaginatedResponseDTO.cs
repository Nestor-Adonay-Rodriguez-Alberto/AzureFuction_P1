using System;
using System.Collections.Generic;
using System.Drawing.Printing;


namespace AzureFuction.Empleado.Aplication.DTOs.Responses
{
    public class PaginatedResponseDTO<T> : ResponseDTO<T>
    {
        public int Count { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)Count / PageSize);
    }
}
