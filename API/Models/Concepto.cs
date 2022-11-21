using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Concepto
    {
        public int IDVisita { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Importe { get; set; }
    }
}
