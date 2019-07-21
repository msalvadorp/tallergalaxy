using System;
using System.ComponentModel.DataAnnotations;

namespace Sol.Ventas.DTO
{
    public class Articulo
    {
        [Key]
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

    }
}
