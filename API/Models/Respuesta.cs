using API.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Respuesta
    {
        public List<object> ListaObjetos { get; set; } = new List<object>();
        public string MensajeRespuesta { get; set; } = "";
        public bool Estado { get; set; } = true;
        public Traza Traza { get; set; } = null;

    }
}
