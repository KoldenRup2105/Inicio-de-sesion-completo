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
    /// Lógica de interacción para RecuperarCuenta.xaml
    /// </summary>
    public partial class RecuperarCuenta : Window
    {
        public RecuperarCuenta()
        {
            InitializeComponent();
        }

        private void btIngresar_Click(object sender, RoutedEventArgs e)
        {
            string clave = tbclave.Password;
            string contrasena=tbContrasena.Password;
            string usuario=tbUser.Text;

            if (!String.IsNullOrEmpty(clave)) 
            { 
                if (clave == "123456") 
                {   
                    if (!String.IsNullOrEmpty(usuario) && !String.IsNullOrEmpty(contrasena)) 
                    {
                        BaseDatos bd=new BaseDatos();
                        if (bd.comprobarExisteUsuario(usuario) == true) 
                        {
                            bd.renovarCuenta(usuario, contrasena);
                            MessageBox.Show("Contraseña cambiada correctamente");
                            MainWindow mw = new MainWindow();
                            mw.Show();
                            this.Close();
                        }else
                            MessageBox.Show("Usuario no encontrado");

                    }
                    else
                        MessageBox.Show("Datos incompletos");
                }else
                    MessageBox.Show("Clave incorrecta");

            } else
                MessageBox.Show("Ingrese clave");
        }
        
    }
}
