
using API.Data;
using API.Models;
using PelcanApp.Recursos.UserControls;
using PelcanApp.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PelcanApp.Pages
{
    /// <summary>
    /// Lógica de interacción para PgClientesMascotas.xaml
    /// </summary>
    public partial class PgClientesMascotas : Page
    {



        public PgClientesMascotas()
        {
            InitializeComponent();

            //Obtenemos un listado completo de todos los clientes almacenados en la base datos
            Respuesta respuesta = DataClientes.MostrarClientes();
            List<object> listaClientes = respuesta.ListaObjetos;

            foreach (Cliente cliente in listaClientes)
            {
                ItemCliente item = new ItemCliente();
                item.itemClienteDNI.Content = cliente.DNI;
                item.itemClienteNombreCompleto.Content = cliente.NombreCompleto;
                item.itemClienteFecha.Content = cliente.FechaAlta;
                item.itemClienteTelefono.Content = cliente.Telefono;
                item.Tag = cliente.IDCliente;
                item.Padre = this;

                GridUsuario.Children.Add(item);
            }


        }

        private void btnBorrarBusqueda_Click(object sender, RoutedEventArgs e)
        {
            txtBuscarCliente.Text = "";
        }

        private void btnNuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            Window window = new wNuevoCliente();

            MessageBox.Show(window.ShowDialog().ToString());
            
        }
    }
}
