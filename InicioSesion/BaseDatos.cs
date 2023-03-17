using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Data;

namespace InicioSesion
{
    class BaseDatos
    {
        public string cadenaConexion = "Data Source=LAPTOP-2JF8TLM8\\SQLEXPRESS; Initial Catalog =Inicio_Sesion; Integrated Security=true;";

        public static string nombreCompleto = "";
        public static string tipoUsuario = "";

        public Boolean iniciarSesion(string nombreUsuario, string contrasena)
        {
            nombreCompleto = "";
            tipoUsuario = "";

            string consultaUsuario = "SELECT  U.NOMBRE,U.APELLIDO,U.NOMBRE_USUARIO,U.PASSWORD_USUARIO,C.NOMBRE FROM USUARIO U INNER JOIN " +
                    "CARGO C ON U.ID_CARGO=C.ID WHERE NOMBRE_USUARIO=@nombreUsuario AND PASSWORD_USUARIO COLLATE Latin1_General_CS_AS=@contrasena";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand(consultaUsuario, conexion))
                {

                    comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    comando.Parameters.AddWithValue("@contrasena", contrasena);

                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nombreCompleto = reader.GetString(0) + " " + reader.GetString(1);
                            tipoUsuario = reader.GetString(4);

                            return true;

                        }
                        else
                            return false;
                    }

                }
            }
        }

        public Boolean comprobarExisteUsuario(string nombreUsuario)
        {

            string consultaUsuario = "SELECT NOMBRE_USUARIO FROM USUARIO WHERE NOMBRE_USUARIO=@nombreUsuario";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand(consultaUsuario, conexion))
                {

                    comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                            return true;

                        else
                            return false;
                    }

                }
            }
        }

        public void renovarCuenta(string nombreUsuario, string nuevaContrasena)
        {

            SqlConnection conexion = new SqlConnection(cadenaConexion);
            string consultaUsuario = "UPDATE USUARIO SET PASSWORD_USUARIO = @nuevaContrasena WHERE NOMBRE_USUARIO=@usuario";

            conexion.Open();

            SqlCommand comando = new SqlCommand(consultaUsuario, conexion);

            comando.Parameters.AddWithValue("@usuario", nombreUsuario);
            comando.Parameters.AddWithValue("@nuevaContrasena", nuevaContrasena);

            int rowsAffected = comando.ExecuteNonQuery();

            conexion.Close();

        }

        public void agregarNuevoUsuario(Usuario usuario) 
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            string consultaUsuario = "INSERT INTO USUARIO VALUES (@nombre,@apellido,@nombreUsuario,@contrasena,@cargo)";

            conexion.Open();

            SqlCommand comando = new SqlCommand(consultaUsuario, conexion);

            comando.Parameters.AddWithValue("@nombre",usuario.Nombre);
            comando.Parameters.AddWithValue("@contrasena", usuario.Contrasena);
            comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
            comando.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
            comando.Parameters.AddWithValue("@cargo", usuario.Cargo);

            int rowsAffected = comando.ExecuteNonQuery();

            conexion.Close();
        }

        public int obtenerIdCargo(string cargo)
        {
            string consultaUsuario = "SELECT ID FROM CARGO WHERE NOMBRE=@nombreCargo";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand(consultaUsuario, conexion))
                {

                    comando.Parameters.AddWithValue("@nombreCargo", cargo);

                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);

                            return id;
                        }
                        else
                            return 0;
                    }

                }
            }
        }


     
    }
}
