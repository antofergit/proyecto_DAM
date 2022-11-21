using API.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.Data
{
    public static class DataUsuarios
    {
        public static Respuesta MostrarUsuarios()
        {
            Respuesta respuesta = new Respuesta();
            string consulta = "SELECT * FROM Usuarios";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        respuesta.ListaObjetos.Add(new Usuario()
                        {
                            IDUsuario = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"],
                            Apellidos = (string)reader["Apellidos"],
                            NombreCompleto = $"{(string)reader["Nombre"]} {(string)reader["Apellidos"]}",
                            Telefono = (int)reader["Telefono"],
                            Direccion = (string)reader["Direccion"],
                            CodigoPostal = (int)reader["CodigoPostal"],
                            Poblacion = (string)reader["Poblacion"],
                            Provincia = (string)reader["Provincia"],
                            DNI = (string)reader["DNI"],
                            Correo = (string)reader["Correo"]
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"{respuesta.ListaObjetos.Count} Usuarios listados";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay Usuarios que mostrar";
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

        public static Respuesta MostrarUsuarioID(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"SELECT * FROM Usuario Where Id={id}";

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
                        respuesta.ListaObjetos.Add(new Usuario()
                        {
                            IDUsuario = (int)fila["Id"],
                            Nombre = (string)fila["Nombre"],
                            Apellidos = (string)fila.ItemArray[2],
                            NombreCompleto = $"{(string)fila["Nombre"]} {(string)fila.ItemArray[2]}",
                            Telefono = (int)fila["Telefono"],
                            Direccion = (string)fila["Direccion"],
                            CodigoPostal = (int)fila["CodigoPostal"],
                            Poblacion = (string)fila["Poblacion"],
                            Provincia = (string)fila["Provincia"],
                            DNI = (string)fila["DNI"],
                            Correo = (string)fila["Correo"]
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"Se ha encontrado el usuario";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay usuarios que mostrar";
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

        public static Respuesta EliminarUsuario(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"DELETE FROM Usuario Where Id={id}";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        respuesta.MensajeRespuesta = $"No se puede eliminar el usuario con id {id}";
                        respuesta.Estado = false;
                    }

                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    respuesta.MensajeRespuesta = $"El usuario ha sido eliminado";

                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;
        }

        public static Respuesta AgregarUsuario(Usuario usuario)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoUsuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("@nombreCompleto", usuario.NombreCompleto);
                    cmd.Parameters.AddWithValue("@telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("@codigoPostal", usuario.CodigoPostal);
                    cmd.Parameters.AddWithValue("@poblacion", usuario.Poblacion);
                    cmd.Parameters.AddWithValue("@provincia", usuario.Provincia);
                    cmd.Parameters.AddWithValue("@dni", usuario.DNI);
                    cmd.Parameters.AddWithValue("@correo", usuario.Correo);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido crear el usuario." : $"El usuario {usuario.NombreCompleto} se ha creado correctamente";
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }


            return respuesta;
        }

        public static Respuesta EditarUsuario(Usuario usuario)
        {

            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoUsuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("@nombreCompleto", usuario.NombreCompleto);
                    cmd.Parameters.AddWithValue("@telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("@codigoPostal", usuario.CodigoPostal);
                    cmd.Parameters.AddWithValue("@poblacion", usuario.Poblacion);
                    cmd.Parameters.AddWithValue("@provincia", usuario.Provincia);
                    cmd.Parameters.AddWithValue("@dni", usuario.DNI);
                    cmd.Parameters.AddWithValue("@correo", usuario.Correo);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"El usuario {usuario.NombreCompleto} no se ha podido editar." : $"El usuario {usuario.NombreCompleto} se ha editado correctamente.";
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
