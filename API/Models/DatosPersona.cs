using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class DatosPersona
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public int CodigoPostal { get; set; }
        public string Poblacion { get; set; }
        public string Provincia { get; set; }
        public string Correo { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
