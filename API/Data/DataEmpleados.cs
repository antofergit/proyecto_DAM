using API.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.Data
{
    public static class DataEmpleados
    {
        public static Respuesta MostrarEmpleados()
        {
            Respuesta respuesta = new Respuesta();
            string consulta = "SELECT * FROM Empleados";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        respuesta.ListaObjetos.Add(new Empleado()
                        {
                            IdEmpleado = (int)reader["Id"],
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
                        respuesta.MensajeRespuesta = $"{respuesta.ListaObjetos.Count} Empleados listados";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay Empleados que mostrar";
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

        public static Respuesta MostrarEmpleadoID(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"SELECT * FROM Empleado Where Id={id}";

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
                        respuesta.ListaObjetos.Add(new Empleado()
                        {
                            IdEmpleado = (int)fila["Id"],
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
                        respuesta.MensajeRespuesta = $"Se ha encontrado el empleado";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay empleados que mostrar";
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

        public static Respuesta EliminarEmpleado(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"DELETE FROM Empleado Where Id={id}";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        respuesta.MensajeRespuesta = $"No se puede eliminar el empleado con id {id}";
                        respuesta.Estado = false;
                    }

                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    respuesta.MensajeRespuesta = $"El empleado ha sido eliminado";

                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;
        }

        public static Respuesta AgregarEmpleado(Empleado empleado)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoEmpleado", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@apellidos", empleado.Apellidos);
                    cmd.Parameters.AddWithValue("@nombreCompleto", empleado.NombreCompleto);
                    cmd.Parameters.AddWithValue("@telefono", empleado.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", empleado.Direccion);
                    cmd.Parameters.AddWithValue("@codigoPostal", empleado.CodigoPostal);
                    cmd.Parameters.AddWithValue("@poblacion", empleado.Poblacion);
                    cmd.Parameters.AddWithValue("@provincia", empleado.Provincia);
                    cmd.Parameters.AddWithValue("@dni", empleado.DNI);
                    cmd.Parameters.AddWithValue("@correo", empleado.Correo);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido crear el empleado." : $"El empleado {empleado.NombreCompleto} se ha creado correctamente";
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }


            return respuesta;
        }

        public static Respuesta EditarEmpleado(Empleado empleado)
        {

            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoEmpleado", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@apellidos", empleado.Apellidos);
                    cmd.Parameters.AddWithValue("@nombreCompleto", empleado.NombreCompleto);
                    cmd.Parameters.AddWithValue("@telefono", empleado.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", empleado.Direccion);
                    cmd.Parameters.AddWithValue("@codigoPostal", empleado.CodigoPostal);
                    cmd.Parameters.AddWithValue("@poblacion", empleado.Poblacion);
                    cmd.Parameters.AddWithValue("@provincia", empleado.Provincia);
                    cmd.Parameters.AddWithValue("@dni", empleado.DNI);
                    cmd.Parameters.AddWithValue("@correo", empleado.Correo);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"El empleado {empleado.NombreCompleto} no se ha podido editar." : $"El empleado {empleado.NombreCompleto} se ha editado correctamente.";
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
