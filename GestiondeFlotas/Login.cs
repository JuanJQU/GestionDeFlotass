using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiondeFlotas
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string rol = cbRol.SelectedItem?.ToString(); //SelectedItem: Propiedad de control que obtiene el elemento seleccionado en un ComboBox.
            string contraseña = txtContraseña.Text; //.Text: Propiedad de control que obtiene o establece el texto que se muestra en un control de entrada de texto, como un TextBox.
            string documento = txtDocumento.Text; 

            if (string.IsNullOrWhiteSpace(documento) || string.IsNullOrWhiteSpace(contraseña) || string.IsNullOrWhiteSpace(rol)) //Evalua que ninguno de los campos esté vacío o contenga solo espacios en blanco.
            {
                MessageBox.Show("Por favor completa todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(documento, out int usuarioId)) //int.TryParse: Método que intenta convertir una cadena a un número entero. Si la conversión es exitosa, devuelve true y el valor convertido se almacena en usuarioId.
            {
                MessageBox.Show("El documento debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;"; //Data Source: Especifica el servidor de base de datos al que se va a conectar
                using (SqlConnection conn = new SqlConnection(connString)) //SqlConnection: Clase que representa una conexión a una base de datos SQL Server.
                {
                    conn.Open(); //Se abre la conexión a la base de datos.
                    string query = "SELECT PasswordHash, Rol FROM Usuarios WHERE UsuarioID = @usuarioId"; //Se realiza una consulta SQL

                    using (SqlCommand cmd = new SqlCommand(query, conn))  //using: Se utiliza para asegurar que los recursos se liberen adecuadamente después de su uso. //SqlCommand: Clase que representa una instrucción SQL que se va a ejecutar en la base de datos.
                    {
                        cmd.Parameters.AddWithValue("@usuarioId", usuarioId); //AddWithValue: Método que agrega un parámetro a la consulta SQL con un valor específico.

                        using (SqlDataReader reader = cmd.ExecuteReader()) //SqlDataReader: Clase que permite leer filas de resultados de una consulta SQL.
                        {
                            if (reader.Read()) //Read: Método que avanza al siguiente registro en el conjunto de resultados.
                            {
                                byte[] passwordHashBD = (byte[])reader["PasswordHash"]; //reader["PasswordHash"]: Accede al valor de la columna PasswordHash en el registro actual del SqlDataReader.
                                string rolBD = reader["Rol"].ToString(); //reader["Rol"]: Accede al valor de la columna Rol en el registro actual del SqlDataReader.

                                byte[] passwordIngresadaHash = PasswordUtils.GetSHA1Hash(contraseña); //PasswordUtils.GetSHA1Hash: Método que calcula el hash SHA-1 de la contraseña ingresada por el usuario.
                                string hashIngresadaHex = BitConverter.ToString(passwordIngresadaHash).Replace("-", ""); //BitConverter.ToString: Método que convierte un arreglo de bytes en una representación hexadecimal en forma de cadena
                                string hashBDHex = BitConverter.ToString(passwordHashBD).Replace("-", ""); //Reemplaza los guiones en la representación hexadecimal del hash de la base de datos con cadenas vacías.
                                //MessageBox.Show($"Hash ingresada: {hashIngresadaHex}\nHash BD: {hashBDHex}"); 

                                if (passwordHashBD.SequenceEqual(passwordIngresadaHash))
                                {
                                    if (rol.Equals(rolBD, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Usuario db = new Usuario();
                                        Usuario usuario = db.ObtenerDatosUsuario(usuarioId);

                                        if (usuario != null)
                                        {
                                            if (rolBD == "Administrador")
                                            {
                                                AdminMenu adminMenu = new AdminMenu();
                                                adminMenu.Show();
                                            }
                                            else if (rolBD == "Conductor")
                                            {
                                                ConductorMenu conductorMenu = new ConductorMenu();
                                                conductorMenu.Show();
                                            }

                                            this.Hide(); // Ocultar login
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se pudieron obtener los datos del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Rol incorrecto. Seleccione el rol correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Usuario no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            cbRol.Items.Add("Administrador");
            cbRol.Items.Add("Conductor");
            cbRol.SelectedIndex = 0; // por defecto
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cerrar el formulario de login
        }
       
        
    }
}
