using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using API.Models;
using System.IO;
using API.Conexion;


namespace API.Data
{
    public static class DataClientes 
    {

        public static Respuesta MostrarClientes()
        {
            Respuesta respuesta = new Respuesta();
            string consulta = "SELECT * FROM Cliente";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        respuesta.ListaObjetos.Add(new Cliente()
                        {
                            IDCliente = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"],
                            Apellidos = (string)reader["Apellidos"],
                            NombreCompleto = $"{(string)reader["Nombre"]} {(string)reader["Apellidos"]}",
                            Telefono = (int)reader["Telefono"],
                            Direccion = (string)reader["Direccion"],
                            CodigoPostal = (int)reader["CodigoPostal"],
                            Poblacion = (string)reader["Poblacion"],
                            Provincia = (string)reader["Provincia"],
                            DNI = (string)reader["DNI"],
                            Correo = (string)reader["Correo"],
                            FechaAlta = (DateTime)reader["FechaAlta"]
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"{respuesta.ListaObjetos.Count} Clientes listados";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay clientes que mostrar";
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

        public static Respuesta MostrarClienteID(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"SELECT * FROM Cliente Where Id={id}";

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
                        respuesta.ListaObjetos.Add(new Cliente()
                        {
                            IDCliente = (int)fila["Id"],
                            Nombre = (string)fila["Nombre"],
                            Apellidos = (string)fila.ItemArray[2],
                            NombreCompleto = $"{(string)fila["Nombre"]} {(string)fila.ItemArray[2]}",
                            Telefono = (int)fila["Telefono"],
                            Direccion = (string)fila["Direccion"],
                            CodigoPostal = (int)fila["CodigoPostal"],
                            Poblacion = (string)fila["Poblacion"],
                            Provincia = (string)fila["Provincia"],
                            DNI = (string)fila["DNI"],
                            Correo = (string)fila["Correo"],
                            FechaAlta = (DateTime)fila["FechaAlta"]

                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"Se ha encontrado el cliente";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay clientes que mostrar";
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

        public static Respuesta EliminarCliente(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"DELETE FROM Cliente Where Id={id}";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        respuesta.MensajeRespuesta = $"No se puede eliminar el cliente con id {id}";
                        respuesta.Estado = false;
                    }

                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    respuesta.MensajeRespuesta = $"El Cliente ha sido eliminado";

                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;
        }

        public static Respuesta AgregarCliente(Cliente cliente)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoCliente", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                    cmd.Parameters.AddWithValue("@nombreCompleto", cliente.NombreCompleto);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    cmd.Parameters.AddWithValue("@codigoPostal", cliente.CodigoPostal);
                    cmd.Parameters.AddWithValue("@poblacion", cliente.Poblacion);
                    cmd.Parameters.AddWithValue("@provincia", cliente.Provincia);
                    cmd.Parameters.AddWithValue("@dni", cliente.DNI);
                    cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@fecha", cliente.FechaAlta);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido crear el cliente." : $"El cliente {cliente.NombreCompleto} se ha creado correctamente";
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            
            return respuesta;
        }

        public static Respuesta EditarCliente(Cliente cliente)
        {

            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoCliente", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                    cmd.Parameters.AddWithValue("@nombreCompleto", cliente.NombreCompleto);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    cmd.Parameters.AddWithValue("@codigoPostal", cliente.CodigoPostal);
                    cmd.Parameters.AddWithValue("@poblacion", cliente.Poblacion);
                    cmd.Parameters.AddWithValue("@provincia", cliente.Provincia);
                    cmd.Parameters.AddWithValue("@dni", cliente.DNI);
                    cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@fecha", cliente.FechaAlta);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"El cliente {cliente.NombreCompleto} no se ha podido editar." : $"El cliente {cliente.NombreCompleto} se ha editado correctamente.";
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

