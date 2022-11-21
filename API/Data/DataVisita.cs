using API.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.Data
{
    public static class DataVisita
    {
        public static Respuesta MostrarVisitas()
        {
            Respuesta respuesta = new Respuesta();
            string consulta = "SELECT * FROM Visitas";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        respuesta.ListaObjetos.Add(new Visita()
                        {
                            IDVisita = (int)reader["Id"],
                            Fecha = (DateTime)reader["Fecha"],
                            IDCita = (int)reader["IdCita"],
                            IDCliente = (int)reader["IdCliente"],
                            IDMascota = (int)reader["IdMascota"],
                            Importe = (double)reader["Importe"]
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"{respuesta.ListaObjetos.Count} Visitas listadas";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay Visitas que mostrar";
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

        public static Respuesta MostrarVisitaID(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"SELECT * FROM Visita Where Id={id}";

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
                        respuesta.ListaObjetos.Add(new Visita()
                        {
                            IDVisita = (int)fila["Id"],
                            Fecha = (DateTime)fila["Fecha"],
                            IDCita = (int)fila["IdCita"],
                            IDCliente = (int)fila["IdCliente"],
                            IDMascota = (int)fila["IdMascota"],
                            Importe = (double)fila["Importe"]
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"Se ha encontrado la visita";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay visitas que mostrar";
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

        public static Respuesta EliminarVisita(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"DELETE FROM Visita Where Id={id}";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        respuesta.MensajeRespuesta = $"No se puede eliminar la visita con id {id}";
                        respuesta.Estado = false;
                    }

                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    respuesta.MensajeRespuesta = $"La visita ha sido eliminado";

                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;
        }

        public static Respuesta AgregarVisita(Visita visita)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoVisita", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fecha", visita.Fecha);
                    cmd.Parameters.AddWithValue("@idCliente", visita.IDCliente);
                    cmd.Parameters.AddWithValue("@idMascota", visita.IDMascota);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido crear la visita." : $"La visita se ha creado correctamente";
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }


            return respuesta;
        }

        public static Respuesta EditarVisita(Visita visita)
        {

            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoVisita", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", visita.IDVisita);
                    cmd.Parameters.AddWithValue("@Fecha", visita.Fecha);
                    cmd.Parameters.AddWithValue("@idCliente", visita.IDCliente);
                    cmd.Parameters.AddWithValue("@idMascota", visita.IDMascota);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido editar la visita." : $"La visita se ha editado correctamente.";
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
