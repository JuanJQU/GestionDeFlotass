using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;


namespace GestiondeFlotas
{
    public partial class GestorConductores : Form
    {

        public string Nombre { get; set; } // Propiedad para almacenar el usuario que accede al gestor de conductores
        public string ConductorID { get; set; } // Propiedad para almacenar el ID del usuario que accede al gestor de conductores
        public string Licencia { get; set; } // Propiedad para almacenar la licencia del usuario que accede al gestor de conductores
        public string Telefono { get; set; } // Propiedad para almacenar el teléfono del usuario que accede al gestor de conductores
        public string Email { get; set; } // Propiedad para almacenar el email del usuario que accede al gestor de conductores
        public byte[] PasswordHash { get; set; } // Propiedad para almacenar el hash de la contraseña del usuario que accede al gestor de conductores



        //private int usuarioID;
        private Usuario db;
        public GestorConductores()
        {
            
            InitializeComponent();
            db = new Usuario();
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar campos vacios
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDocumento.Text) || string.IsNullOrWhiteSpace(txtLicencia.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error de campos vacíos",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            if(!int.TryParse(txtDocumento.Text, out int documento))
            {
                MessageBox.Show("El documento debe ser un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                
            }
            byte[] passwordHash = PasswordUtils.GetSHA1Hash(txtContraseña.Text);
            if(db.UsuarioExistente(documento))
            {
                MessageBox.Show("El conductor ya existe.", "Error de duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;";
                using(SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "INSERT INTO Conductores (Nombre, ConductorID, Licencia, Telefono, Email, PasswordHash) VALUES (@Nombre, @ConductorID, @Licencia, @Telefono, @Email, @PasswordHash)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@ConductorID", documento);
                        cmd.Parameters.AddWithValue("@Licencia", txtLicencia.Text);
                        cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error al agregar el conductor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void GestorConductores_Load(object sender, EventArgs e)
        {
            dgvConductores.AutoGenerateColumns = false;

            // Crear columnas de texto
            dgvConductores.Columns.Add("Nombre", "Nombre");
            dgvConductores.Columns["Nombre"].DataPropertyName = "Nombre";

            dgvConductores.Columns.Add("Documento", "Documento");
            dgvConductores.Columns["Documento"].DataPropertyName = "Documento";

            dgvConductores.Columns.Add("Licencia", "Licencia");
            dgvConductores.Columns["Licencia"].DataPropertyName = "Licencia";

            dgvConductores.Columns.Add("Telefono", "Teléfono");
            dgvConductores.Columns["Telefono"].DataPropertyName = "Telefono";

            dgvConductores.Columns.Add("Email", "Email");
            dgvConductores.Columns["Email"].DataPropertyName = "Email";
     
        }

        
        private void dgvConductores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDocumento.Text, out int ConductorID))
            {
                MessageBox.Show("Ingrese un número de documento válido.", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtLicencia.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("Debe llenar todos los campos obligatorios.", "Campos vacíos");
                return;
            }

            if (db.DatosUsuarioDuplicados(
                    ConductorID,
                    txtEmail.Text,
                    txtTelefono.Text,
                    txtLicencia.Text,
                    PasswordUtils.GetSHA1Hash(txtContraseña.Text)
                ))
            {
                MessageBox.Show("El dato ingresado ya pertenece a otro usuario.", "Error");
                return;
            }

            Usuario usuario = new Usuario
            {
                UsuarioID = ConductorID,
                Nombre = txtNombre.Text,
                Licencia = txtLicencia.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                PasswordHash = PasswordUtils.GetSHA1Hash(txtContraseña.Text)
            };

            bool actualizado = db.ActualizarConductor(usuario);

            if (actualizado)
            {
                MessageBox.Show("Usuario actualizado correctamente.", "Éxito");
                CargarUsuarios();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el usuario.", "Error");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDocumento.Text, out int ConductorID))
            {
                MessageBox.Show("Seleccione o ingrese un usuario válido para eliminar.", "Error");
                return;
            }
            else
            {
                var confirm = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este usuario? Esta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    //ELIMINA EL CONDUCTOR
                    bool eliminado = db.EliminarConductor(ConductorID);
                    if (eliminado)
                    {
                        MessageBox.Show("Usuario eliminado correctamente.", "Éxito");
                        CargarUsuarios();
                        btnLimpiar_Click(null, null); // Limpia los campos
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el usuario.", "Error");
                    }
                }
            }
        }
        

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminMenu menu = new AdminMenu();
            menu.Show();
            this.Hide(); // Ocultar el gestor de conductores
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtDocumento.Text, out int usuarioId))
            {
                // Obtener el usuario desde la base de datos
                Usuario usuario = db.ObtenerDatosUsuario(usuarioId);

                if (usuario != null)
                {
                    // Llenar los campos del formulario con los datos del usuario
                    txtNombre.Text = usuario.Nombre;
                    txtEmail.Text = usuario.Email;
                    txtTelefono.Text = usuario.Telefono;
                    txtLicencia.Text = usuario.Licencia;
                    txtDocumento.Text = usuario.UsuarioID.ToString(); // Asumiendo que ConductorID es el documento del usuario
                    txtContraseña.Text = "";
                  
                    DataTable dt = db.BuscarUsuarioPorId(usuarioId);
                    dgvConductores.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado.");
                    dgvConductores.DataSource = null;

                }
            }
            else
            {
                MessageBox.Show("Ingrese el número de la persona que desea buscar.", "Error");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtDocumento.Clear();
            txtLicencia.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtContraseña.Clear();
        }

        private void CargarUsuarios()
        {
            dgvConductores.DataSource = db.GetAllUsers();
        }

        private void txtDocumento_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }
    }
}

