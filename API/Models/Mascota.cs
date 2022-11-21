using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Mascota
    {
        public int IDMascota { get; set; }
        public string Nombre { get; set; }
        public int IDRaza { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public double Peso { get; set; }
    }
}
