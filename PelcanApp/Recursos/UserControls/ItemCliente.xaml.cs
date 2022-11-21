using System;
using System.Collections.Generic;
using System.IO;
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
using PelcanApp.Pages;
using API.Data;
using API.Models;

namespace PelcanApp.Recursos.UserControls
{
    /// <summary>
    /// Lógica de interacción para ItemCliente.xaml
    /// </summary>
    public partial class ItemCliente : UserControl
    {

        bool ClickEnPanel = true;
        public PgClientesMascotas Padre { get; set; }

        public ItemCliente()
        {
            InitializeComponent();
            imgCortar.Source = Herramientas.DameImagen(Properties.Resources.deleteGris);
            imgEditar.Source = Herramientas.DameImagen(Properties.Resources.EditGris);
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_GotFocus(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hola");

            Image imagen = e.Source as Image;

            BitmapImage image = new BitmapImage(new Uri("Recursos/imagenes/dog.jpg", UriKind.Relative));
            imagen.Source = image;
        }

        private void imgCortar_LostFocus(object sender, RoutedEventArgs e)
        {
            Image imagen = e.Source as Image;

            BitmapImage image = new BitmapImage(new Uri("Recursos/imagenes/dog.png", UriKind.Relative));
            imagen.Source = image;


        }

        private void imgCortar_MouseEnter(Object sender, MouseEventArgs e)
        {
            Image imagen = sender as Image;
            imagen.Source = Herramientas.DameImagen(Properties.Resources.deleteRojo);

        }

        private void imgCortar_MouseLeave(object sender, MouseEventArgs e)
        {
            Image imagen = sender as Image;
            imagen.Source = Herramientas.DameImagen(Properties.Resources.deleteGris);
        }

        private void imgCortar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image imagen = sender as Image;
            imagen.Source = Herramientas.DameImagen(Properties.Resources.deleteClick);
            MessageBox.Show("Click en el boton eliminar");
            ClickEnPanel = false;
        }

        private void imgCortar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image imagen = sender as Image;
            imagen.Source = Herramientas.DameImagen(Properties.Resources.deleteRojo);
        }

        private void imgEditar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image imagen = sender as Image;
            imagen.Source = Herramientas.DameImagen(Properties.Resources.EditClick);
            MessageBox.Show("Click en el boton editar");
            ClickEnPanel = false;
        }

        private void imgEditar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image imagen = sender as Image;
            imagen.Source = Herramientas.DameImagen(Properties.Resources.EditAmarillo);
        }

        private void imgEditar_MouseEnter(object sender, MouseEventArgs e)
        {
            Image imagen = sender as Image;
            imagen.Source = Herramientas.DameImagen(Properties.Resources.EditAmarillo);
        }

        private void imgEditar_MouseLeave(object sender, MouseEventArgs e)
        {
            Image imagen = sender as Image;
            imagen.Source = Herramientas.DameImagen(Properties.Resources.EditGris);
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid miGrid = e.Source as Grid;
            miGrid.Background = Brushes.White;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid miGrid = e.Source as Grid;
            miGrid.Background = Brushes.Transparent;
        }

        private void Stack_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel stack = e.Source as StackPanel;
            stack.Background = Brushes.White;
        }

        private void Stack_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel stack = e.Source as StackPanel;
            stack.Background = Brushes.WhiteSmoke;
        }

        private void Stack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ClickEnPanel)
            {
                //Obtenemos el cliente clicado
                Respuesta respuesta = DataClientes.MostrarClienteID((int)Tag);
                Cliente cliente = respuesta.ListaObjetos[0] as Cliente;

                //Obtenemos la pagina de Cliente mascotas
                Padre.datosClienteLine1.Content = $"DNI:  {cliente.DNI} Telefono:  {cliente.Telefono}";
                Padre.datosClienteLine2.Content = $"Nombre:  {cliente.NombreCompleto}";
                Padre.datosClienteLine3.Content = $"Direccion:  {cliente.Direccion}";
                Padre.datosClienteLine4.Content = $"Poblacion:  {cliente.Poblacion}";
                Padre.datosClienteLine5.Content = $"Codigo Postal:  {cliente.CodigoPostal} Provincia:  {cliente.Provincia}";
                Padre.datosClienteLine6.Content = $"Correo:  {cliente.Correo}";
                Padre.datosClienteLine7.Content = $"Fecha de Alta:  {cliente.FechaAlta}";
            }
            else
            {
                ClickEnPanel = true;
            }

        }
    }
}
