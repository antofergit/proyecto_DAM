using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Producto
    {
        public int IDProducto { get; set; }
        public string Descripcion { get; set; }
        public double PVP { get; set; }
    }
}
