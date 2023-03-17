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
using System.Windows.Shapes;

namespace InicioSesion
{
    /// <summary>
    /// Lógica de interacción para Programa.xaml
    /// </summary>
    public partial class Programa : Window
    {
        public Programa()
        {
            InitializeComponent();

            nombreUsuario.Content = BaseDatos.nombreCompleto;

            if (BaseDatos.tipoUsuario == "Jefe")
            {
                panelJefe.Visibility = Visibility.Visible;
                panelAdministrador.Visibility = Visibility.Hidden;
                panelTrabajador.Visibility = Visibility.Hidden;
            }

            if (BaseDatos.tipoUsuario == "Administrador")
            {
                panelJefe.Visibility = Visibility.Hidden;
                panelAdministrador.Visibility = Visibility.Visible;
                panelTrabajador.Visibility = Visibility.Hidden;
            }

            if (BaseDatos.tipoUsuario == "Empleado")
            {
                panelJefe.Visibility = Visibility.Hidden;
                panelAdministrador.Visibility = Visibility.Hidden;
                panelTrabajador.Visibility = Visibility.Visible;
            }
        }

        private void btCerrar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw=new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
