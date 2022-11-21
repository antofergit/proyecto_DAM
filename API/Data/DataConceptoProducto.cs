using API.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.Data
{
    public static class DataConceptoProducto
    {
        public static Respuesta MostrarConceptoProductos()
        {
            Respuesta respuesta = new Respuesta();
            string consulta = "SELECT * FROM ConceptoProductos";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        respuesta.ListaObjetos.Add(new ConceptoProducto()
                        {
                            IDConceptoProducto = (int)reader["Id"],
                            IDVisita = (int)reader["IdVisita"],
                            IDProducto = (int)reader["IdProducto"],
                            Cantidad = (int)reader["Cantidad"],
                            PrecioUnitario = (double)reader["PrecioUnitario"],
                            Importe = (double)reader["Importe"],
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"{respuesta.ListaObjetos.Count} ConceptoProductos listados";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay ConceptoProductos que mostrar";
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

        public static Respuesta MostrarConceptoProductoID(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"SELECT * FROM ConceptoProducto Where Id={id}";

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
                        respuesta.ListaObjetos.Add(new ConceptoProducto()
                        {
                            IDConceptoProducto = (int)fila["Id"],
                            IDVisita = (int)fila["IdVisita"],
                            IDProducto = (int)fila["IdProducto"],
                            Cantidad = (int)fila["Cantidad"],
                            PrecioUnitario = (double)fila["PrecioUnitario"],
                            Importe = (double)fila["Importe"],
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"Se ha encontrado el conceptoProducto";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay conceptoProductos que mostrar";
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

        public static Respuesta EliminarConceptoProducto(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"DELETE FROM ConceptoProducto Where Id={id}";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        respuesta.MensajeRespuesta = $"No se puede eliminar el conceptoProducto con id {id}";
                        respuesta.Estado = false;
                    }

                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    respuesta.MensajeRespuesta = $"El conceptoProducto ha sido eliminado";

                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;
        }

        public static Respuesta AgregarConceptoProducto(ConceptoProducto conceptoProducto)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoConceptoProducto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idVisita", conceptoProducto.IDVisita);
                    cmd.Parameters.AddWithValue("@idProducto", conceptoProducto.IDProducto);
                    cmd.Parameters.AddWithValue("@cantidad", conceptoProducto.Cantidad);
                    cmd.Parameters.AddWithValue("@precioUnitario", conceptoProducto.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@importe", conceptoProducto.Importe);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido crear el conceptoProducto." : $"El conceptoProducto se ha creado correctamente";
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }


            return respuesta;
        }

        public static Respuesta EditarConceptoProducto(ConceptoProducto conceptoProducto)
        {

            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoConceptoProducto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", conceptoProducto.IDConceptoProducto);
                    cmd.Parameters.AddWithValue("@idVisita", conceptoProducto.IDVisita);
                    cmd.Parameters.AddWithValue("@idProducto", conceptoProducto.IDProducto);
                    cmd.Parameters.AddWithValue("@cantidad", conceptoProducto.Cantidad);
                    cmd.Parameters.AddWithValue("@precioUnitario", conceptoProducto.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@importe", conceptoProducto.Importe);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"El conceptoProducto no se ha podido editar." : $"El conceptoProducto se ha editado correctamente.";
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
