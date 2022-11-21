using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using API.Models;
using API.Data;

namespace PelcanApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frmContenedor.Navigate(new System.Uri("Pages/PgFondo.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnClientesMascotas_Click(object sender, RoutedEventArgs e)
        {
            frmContenedor.Navigate(new System.Uri("Pages/PgClientesMascotas.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Respuesta respuesta = DataClientes.EliminarCliente(1);
            if(respuesta.Traza != null)
            {
                MessageBox.Show("Se ha creado una nueva traza", "Alerta de Traza", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void btnProductosServicios_Click(object sender, RoutedEventArgs e)
        {
            frmContenedor.Navigate(new System.Uri("Pages/PgProductosServicios.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
