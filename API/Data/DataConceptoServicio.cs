using API.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.Data
{
    public static class DataConceptoServicio
    {
        public static Respuesta MostrarConceptoServicios()
        {
            Respuesta respuesta = new Respuesta();
            string consulta = "SELECT * FROM ConceptoServicios";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        respuesta.ListaObjetos.Add(new ConceptoServicio()
                        {
                            IDConceptoServicio = (int)reader["Id"],
                            IDVisita = (int)reader["IdVisita"],
                            IDServicio = (int)reader["IdServicio"],
                            Cantidad = (int)reader["Cantidad"],
                            PrecioUnitario = (double)reader["PrecioUnitario"],
                            Importe = (double)reader["Importe"],
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"{respuesta.ListaObjetos.Count} ConceptoServicios listados";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay ConceptoServicios que mostrar";
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

        public static Respuesta MostrarConceptoServicioID(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"SELECT * FROM ConceptoServicio Where Id={id}";

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
                        respuesta.ListaObjetos.Add(new ConceptoServicio()
                        {
                            IDConceptoServicio = (int)fila["Id"],
                            IDVisita = (int)fila["IdVisita"],
                            IDServicio = (int)fila["IdServicio"],
                            Cantidad = (int)fila["Cantidad"],
                            PrecioUnitario = (double)fila["PrecioUnitario"],
                            Importe = (double)fila["Importe"],
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"Se ha encontrado el conceptoServicio";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay conceptoServicios que mostrar";
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

        public static Respuesta EliminarConceptoServicio(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"DELETE FROM ConceptoServicio Where Id={id}";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        respuesta.MensajeRespuesta = $"No se puede eliminar el conceptoServicio con id {id}";
                        respuesta.Estado = false;
                    }

                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    respuesta.MensajeRespuesta = $"El conceptoServicio ha sido eliminado";

                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;
        }

        public static Respuesta AgregarConceptoServicio(ConceptoServicio conceptoServicio)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoConceptoServicio", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idVisita", conceptoServicio.IDVisita);
                    cmd.Parameters.AddWithValue("@idServicio", conceptoServicio.IDServicio);
                    cmd.Parameters.AddWithValue("@cantidad", conceptoServicio.Cantidad);
                    cmd.Parameters.AddWithValue("@precioUnitario", conceptoServicio.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@importe", conceptoServicio.Importe);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido crear el conceptoServicio." : $"El conceptoServicio se ha creado correctamente";
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }


            return respuesta;
        }

        public static Respuesta EditarConceptoServicio(ConceptoServicio conceptoServicio)
        {

            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoConceptoServicio", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", conceptoServicio.IDConceptoServicio);
                    cmd.Parameters.AddWithValue("@idVisita", conceptoServicio.IDVisita);
                    cmd.Parameters.AddWithValue("@idServicio", conceptoServicio.IDServicio);
                    cmd.Parameters.AddWithValue("@cantidad", conceptoServicio.Cantidad);
                    cmd.Parameters.AddWithValue("@precioUnitario", conceptoServicio.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@importe", conceptoServicio.Importe);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"El conceptoServicio no se ha podido editar." : $"El conceptoServicio se ha editado correctamente.";
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
