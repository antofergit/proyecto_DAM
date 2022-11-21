using API.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.Data
{
    public static class DataCita
    {
        public static Respuesta MostrarCitas()
        {
            Respuesta respuesta = new Respuesta();
            string consulta = "SELECT * FROM Citas";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        respuesta.ListaObjetos.Add(new Cita()
                        {
                            IDCita = (int)reader["Id"],
                            IDCliente = (int)reader["IdCliente"],
                            IDMascota = (int)reader["IdMascota"],
                            Fecha = (DateTime)reader["Fecha"]

                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"{respuesta.ListaObjetos.Count} Citas listadas";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay Citas que mostrar";
                        respuesta.Estado = false;
                    }


                    con.Close();
                }
                catch (Exception ex)
                {

                    respuesta = Traza.CrearTraza(ex);
                }

                return respuesta;
            }



        }

        public static Respuesta MostrarCitaID(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"SELECT * FROM Cita Where Id={id}";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(consulta, con);
                    da.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        respuesta.ListaObjetos.Add(new Cita()
                        {
                            IDCita = (int)fila["Id"],
                            IDCliente = (int)fila["IdCliente"],
                            IDMascota = (int)fila["IdMascota"],
                            Fecha = (DateTime)fila["Fecha"]
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"Se ha encontrado la cita";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay citas que mostrar";
                        respuesta.Estado = false;
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;
        }

        public static Respuesta EliminarCita(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"DELETE FROM Cita Where Id={id}";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        respuesta.MensajeRespuesta = $"No se puede eliminar la cita con id {id}";
                        respuesta.Estado = false;
                    }

                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    respuesta.MensajeRespuesta = $"La cita ha sido eliminado";

                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;
        }

        public static Respuesta AgregarCita(Cita cita)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoCita", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                    cmd.Parameters.AddWithValue("@idCliente", cita.IDCliente);
                    cmd.Parameters.AddWithValue("@idMascota", cita.IDMascota);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido crear la cita." : $"La cita se ha creado correctamente";
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }


            return respuesta;
        }

        public static Respuesta EditarCita(Cita cita)
        {

            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoCita", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", cita.IDCita);
                    cmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                    cmd.Parameters.AddWithValue("@idCliente", cita.IDCliente);
                    cmd.Parameters.AddWithValue("@idMascota", cita.IDMascota);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido editar la cita." : $"La cita se ha editado correctamente.";
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;

        }
    }
}
