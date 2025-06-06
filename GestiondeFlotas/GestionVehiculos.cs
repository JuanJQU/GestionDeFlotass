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
using System.Xml.Linq;

namespace GestiondeFlotas
{
    public partial class GestionVehiculos : Form
    {
        
        private Vehiculo db = new Vehiculo();
        private int vehiculoIdOriginal;


        public GestionVehiculos()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminMenu menu = new AdminMenu();
            menu.Show();
            this.Hide(); // Ocultar el gestor de conductores
        }

        private void GestionVehiculos_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar campos vacios
            if (string.IsNullOrWhiteSpace(txtMatricula.Text) || string.IsNullOrWhiteSpace(txtMarca.Text) || string.IsNullOrWhiteSpace(txtModelo.Text) || string.IsNullOrWhiteSpace(txtTipo.Text) || string.IsNullOrWhiteSpace(txtCapacidad.Text) || string.IsNullOrWhiteSpace(cbEstado.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error de campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Convertir VehiculoID a entero y validar
            if (!int.TryParse(txtVehiculoID.Text, out int vehiculoId))
            {
                MessageBox.Show("El ID del vehículo debe ser un número válido.", "Error de ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Vehiculo vehiculoDB = new Vehiculo();
            if (vehiculoDB.VehiculoIdExistente(vehiculoId))
            {
                MessageBox.Show("El vehiculo ya existe.", "Error de duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "INSERT INTO Vehiculos (VehiculoID, Matricula, Marca, Modelo, Tipo, Capacidad, Estado ) VALUES (@VehiculoID, @Matricula, @Marca, @Modelo, @Tipo, @Capacidad, @Estado)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VehiculoID", txtVehiculoID.Text);
                        cmd.Parameters.AddWithValue("@Matricula", txtMatricula.Text);
                        cmd.Parameters.AddWithValue("@Marca", txtMarca.Text);
                        cmd.Parameters.AddWithValue("@Modelo", txtModelo.Text);
                        cmd.Parameters.AddWithValue("@Tipo", txtTipo.Text);
                        cmd.Parameters.AddWithValue("@Capacidad", Convert.ToInt32(txtCapacidad.Text));
                        cmd.Parameters.AddWithValue("@Estado", cbEstado.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Vehículo agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el conductor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Limpia los campos de texto y el dgv
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtVehiculoID.Clear();
            txtMatricula.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtCapacidad.Clear();
            txtTipo.Clear();
            cbEstado.SelectedIndex = -1;
        }

        //Listar Vahiculos
        private void CargarVehiculos()
        {
            dgvVehiculos.DataSource = db.GetAllVehiculos();
        }
        private void btnListar_Click(object sender, EventArgs e)
        {
            CargarVehiculos();
        }

        // Actualizar datos de Vehiculo
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Vehiculo vehiculoActualizado = new Vehiculo
                {
                    VehiculoID = int.Parse(txtVehiculoID.Text),
                    Matricula = txtMatricula.Text,
                    Marca = txtMarca.Text,
                    Modelo = txtModelo.Text,
                    tipo = txtTipo.Text,
                    Capacidad = int.Parse(txtCapacidad.Text),
                    Estado = cbEstado.SelectedItem.ToString()
                };

                string connDB = "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;";
                db.ActualizarVehiculo(vehiculoActualizado, connDB);

                MessageBox.Show("Vehículo actualizado correctamente.");
                CargarVehiculos(); // Refresca el DataGridView u otra lista
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        //buscar vehiculos por VehiculoID
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtVehiculoID.Text, out int vehiculoId))
            {
                // Obtener el vehiculo desde la base de datos
                Vehiculo vehiculo = db.BuscarVehiculoPorId(vehiculoId);

                if (vehiculo != null)
                {
                    // Llenar los campos del formulario con los datos del vehiculo
                    txtVehiculoID.Text = vehiculo.VehiculoID.ToString();
                    txtMatricula.Text = vehiculo.Matricula;
                    txtMarca.Text = vehiculo.Marca;
                    txtModelo.Text = vehiculo.Modelo;
                    txtTipo.Text = vehiculo.tipo;
                    txtCapacidad.Text = vehiculo.Capacidad.ToString();
                    cbEstado.SelectedItem = vehiculo.Estado;
                    // Actualizar el DataGridView con los datos del vehiculo

                    DataTable dt = db.BuscarVehiculoDataTablePorId(vehiculoId);
                    dgvVehiculos.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado.");
                    dgvVehiculos.DataSource = null;

                }
            }
            else
            {
                MessageBox.Show("Ingrese el id del  que desea buscar.", "Error");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtVehiculoID.Text, out int vehiculoId))
            {
                MessageBox.Show("Seleccione o ingrese un ID de vehículo válido para eliminar.", "Error");
                return;
            }
            else
            {
                var confirm = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este vehículo? Esta acción no se puede deshacer.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    Vehiculo db = new Vehiculo(); // si no lo has instanciado antes
                    bool eliminado = db.EliminarVehiculo(vehiculoId);
                    if (eliminado)
                    {
                        MessageBox.Show("Vehículo eliminado correctamente.", "Éxito");
                        CargarVehiculos(); // Método que actualiza la vista
                        btnLimpiar_Click(null, null); // Limpia los campos
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el vehículo.", "Error");
                    }
                }
            }
        }
    }
}
