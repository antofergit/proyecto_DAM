using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace API.Conexion
{
    public static class Conexion
    {
        public static string cadenaConexion = 
            "Data Source=.\\SQLEXPRESS;Initial Catalog=Pelcan;User ID=antofer;Password=123456789;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    }
}
