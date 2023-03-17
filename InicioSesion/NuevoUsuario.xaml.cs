using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Lógica de interacción para NuevoUsuario.xaml
    /// </summary>
    public partial class NuevoUsuario : Window
    {
        public NuevoUsuario()
        {
            InitializeComponent();
            rellenarCargos();
        }

        public void rellenarCargos()
        {
            BaseDatos bd = new BaseDatos();

            SqlConnection conexion = new SqlConnection(bd.cadenaConexion);
            conexion.Open();

            string consulta = "SELECT NOMBRE FROM CARGO";

            SqlCommand cmd = new SqlCommand(consulta, conexion);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cbCargo.Items.Add(reader["NOMBRE"].ToString());
            }

            // Cerrar la conexión y el objeto SqlDataReader
            reader.Close();
            conexion.Close();
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (tbClave.Password == "123456")
            {
                BaseDatos bd = new BaseDatos();
                Usuario u = new Usuario();

                u.Nombre = tbNombre.Text;
                u.Apellido = tbApellido.Text;
                u.NombreUsuario = tbUser.Text;
                u.Contrasena = tbContrasena.Password;

                bool nombre = !String.IsNullOrEmpty(u.Nombre);
                bool apellido = !String.IsNullOrEmpty(u.Apellido);
                bool nombreUsuario = !String.IsNullOrEmpty(u.NombreUsuario);
                bool contrasena = !String.IsNullOrEmpty(u.Contrasena);

                u.Cargo = bd.obtenerIdCargo(cbCargo.Text);

                if (nombre && apellido && nombreUsuario && contrasena)
                {
                    try
                    {
                        bd.agregarNuevoUsuario(u);

                        MessageBox.Show("Nuevo usuario agregado exitosamente");

                        MainWindow mw = new MainWindow();
                        mw.Show();
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error: Es posible que los datos estén incompletos o el usuario ya exista (ingrese otro nombre de usuario)");
                    }
                }
                else
                    MessageBox.Show("Datos incompletos");
            }
            else
                MessageBox.Show("La clave es incorrecta");


        }
    

        
    }
}
