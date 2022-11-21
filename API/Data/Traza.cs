using API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace API.Data
{
    public class Traza
    {
        public string Metodo { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }

        public static Respuesta CrearTraza(Exception ex)
        {

            Traza traza = new Traza
            {
                Fecha = DateTime.Now,
                Mensaje = ex.Message
            };

            StringReader strReader = new StringReader(Environment.StackTrace);
            string linea = "";
            for (int x = 0; x < 3; x++)
            {
                linea = strReader.ReadLine();
            }

            traza.Metodo = linea;

            //Guardamos la traza
            GuardarTraza(traza);

            //Creamos un nueva respuesta de error
            Respuesta respuesta = new Respuesta()
            {
                Estado = false,
                MensajeRespuesta = ex.Message,
                ListaObjetos = new List<object>(),
                Traza = traza

            };

            return respuesta;
        }

        private static void GuardarTraza(Traza traza)
        {
            string carpeta = $@"{Directory.GetCurrentDirectory()}\Trazas\{DateTime.Now.Year}_{DateTime.Now.Month}\";

            if (!Directory.Exists(carpeta))
            {   
                Directory.CreateDirectory(carpeta);
            }

            using (StreamWriter escritor = File.AppendText($"{carpeta}Trazas.txt"))
            {
                escritor.WriteLine($"Fecha: {traza.Fecha}\nError en método: {traza.Metodo}\nMensaje de error: {traza.Mensaje}\n");
                escritor.Close();
            }

        }
    }
}
