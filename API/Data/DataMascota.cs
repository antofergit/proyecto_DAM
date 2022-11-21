using API.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.Data
{
    public static class DataMascota
    {
        public static Respuesta MostrarMascotas()
        {
            Respuesta respuesta = new Respuesta();
            string consulta = "SELECT * FROM Mascotas";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        respuesta.ListaObjetos.Add(new Mascota()
                        {
                            IDMascota = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"],
                            IDRaza = (int)reader["IdRaza"],
                            FechaNacimiento = (DateTime)reader["FechaNacimiento"],
                            Peso = (float)reader["Peso"]
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"{respuesta.ListaObjetos.Count} Mascotas listadas";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay Mascotas que mostrar";
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

        public static Respuesta MostrarMascotaID(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"SELECT * FROM Mascota Where Id={id}";

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
                        respuesta.ListaObjetos.Add(new Mascota()
                        {
                            IDMascota = (int)fila["Id"],
                            Nombre = (string)fila["Nombre"],
                            IDRaza = (int)fila["IdRaza"],
                            FechaNacimiento = (DateTime)fila["FechaNacimiento"],
                            Peso = (float)fila["Peso"]
                        });
                    }

                    //Adaptamos la respuesta al número de datos recibidos
                    if (respuesta.ListaObjetos.Count > 0)
                    {
                        respuesta.MensajeRespuesta = $"Se ha encontrado la mascota";

                    }
                    else
                    {
                        respuesta.MensajeRespuesta = "No hay mascotas que mostrar";
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

        public static Respuesta EliminarMascota(int id)
        {
            Respuesta respuesta = new Respuesta();
            string consulta = $"DELETE FROM Mascota Where Id={id}";

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(consulta, con);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        respuesta.MensajeRespuesta = $"No se puede eliminar la mascota con id {id}";
                        respuesta.Estado = false;
                    }

                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    respuesta.MensajeRespuesta = $"La mascota ha sido eliminado";

                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }

            return respuesta;
        }

        public static Respuesta AgregarMascota(Mascota mascota)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoMascota", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", mascota.Nombre);
                    cmd.Parameters.AddWithValue("@idRaza", mascota.IDRaza);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", mascota.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@peso", mascota.Peso);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido crear la mascota." : $"La mascota se ha creado correctamente";
                }
                catch (Exception ex)
                {
                    respuesta = Traza.CrearTraza(ex);
                }
            }


            return respuesta;
        }

        public static Respuesta EditarMascota(Mascota mascota)
        {

            Respuesta respuesta = new Respuesta();

            using (SqlConnection con = new SqlConnection(Conexion.Conexion.cadenaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProcedureNuevoMascota", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", mascota.IDMascota);
                    cmd.Parameters.AddWithValue("@Nombre", mascota.Nombre);
                    cmd.Parameters.AddWithValue("@idRaza", mascota.IDRaza);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", mascota.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@peso", mascota.Peso);

                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();

                    respuesta.MensajeRespuesta = (res == 0) ? $"No se ha podido editar la mascota." : $"La mascota se ha editado correctamente.";
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
