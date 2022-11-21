using API.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.Data
{
    public static class DataProductos
    {
        public static Respuesta MostrarProductos()
        {
            Respuesta respuesta = new Respuesta();
            string consulta = "SELECT * FROM Productos";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        respuesta.ListaObjetos.Add(new Producto()
                        {
                            IDProducto = (int)reader["Id"],
                            Descripcion = (string)reader["Nombre"],
                            PVP = (double)reader["Apellidos"],
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"{respuesta.ListaObjetos.Count} Productos listados";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay Productos que mostrar";
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

        public static Respuesta MostrarProductoID(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"SELECT * FROM Producto Where Id={id}";

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
                        respuesta.ListaObjetos.Add(new Producto()
                        {
                            IDProducto = (int)fila["Id"],
                            Descripcion = (string)fila["Nombre"],
                            PVP = (double)fila["Apellidos"],
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"Se ha encontrado el producto";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay productos que mostrar";
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

        public static Respuesta EliminarProducto(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"DELETE FROM Producto Where Id={id}";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        respuesta.MensajeRespuesta = $"No se puede eliminar el producto con id {id}";
                        respuesta.Estado = false;
                    }

                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    respuesta.MensajeRespuesta = $"El producto ha sido eliminado";

                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;
        }

        public static Respuesta AgregarProducto(Producto producto)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoProducto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.PVP);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido crear el producto." : $"El producto se ha creado correctamente";
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }


            return respuesta;
        }

        public static Respuesta EditarProducto(Producto producto)
        {

            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoProducto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", producto.IDProducto);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.PVP);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"El producto no se ha podido editar." : $"El producto se ha editado correctamente.";
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
