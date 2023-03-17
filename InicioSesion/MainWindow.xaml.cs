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

namespace InicioSesion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btIngresar_Click(object sender, RoutedEventArgs e)
        {
            string usuario= tbUser.Text;
            string contrasena = tbContrasena.Password.ToString();
            if (!String.IsNullOrEmpty(usuario) && !String.IsNullOrEmpty(contrasena))
            {
                try
                {
                    BaseDatos bd = new BaseDatos();
                    Boolean res = bd.iniciarSesion(usuario,contrasena);

                    if (res==true)
                    {
                        Programa p = new Programa();
                        p.Show();
                        this.Close();
                    }
                    else
                        MessageBox.Show("Datos incorrectos");

                }
                catch
                {
                    MessageBox.Show("Error");
                }

            } else
                MessageBox.Show("Datos incompletos");
        }

        private void btRenovarContrasena_Click(object sender, RoutedEventArgs e)
        {
            RecuperarCuenta rc = new RecuperarCuenta();
            rc.Show();
            this.Close();
        }

        private void btRegistrar_Click(object sender, RoutedEventArgs e)
        {
            NuevoUsuario nu = new NuevoUsuario();
            nu.Show();
            this.Close();
        }
    }
}
