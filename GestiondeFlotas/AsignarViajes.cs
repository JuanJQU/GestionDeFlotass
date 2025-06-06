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
    public partial class AsignarViajes : Form
    {
        private ViajeDAO viajeDAO = new ViajeDAO();
        public AsignarViajes()
        {
            InitializeComponent();
            CargarViajes();
        }
        private void CargarViajes()
        {
            dgvViajes.DataSource = viajeDAO.ObtenerDatosViajes();
        }
        private void LimpiarFormulario()
        {
            txtViajeID.Clear();
            txtVehiculoID.Clear();
            txtDocumento.Clear();
            txtRuta.Clear();
            cbEstado.SelectedIndex = -1; // Deselecciona cualquier opción
            dtpSalida.Value = DateTime.Now;
            dtpLlegada.Value = DateTime.Now;

            // Si usas un DataGridView, puedes limpiar la selección
            dgvViajes.ClearSelection();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminMenu menu = new AdminMenu();
            menu.Show();
            this.Hide(); // Ocultar el gestor de conductores
        }
        private Viajes ObtenerViajeDesdeFormulario()
        {
            {
                if (!int.TryParse(txtViajeID.Text, out int viajeId))
                {
                    MessageBox.Show("Ingrese un ViajeID válido (número entero).");
                    return null;
                }

                if (!int.TryParse(txtVehiculoID.Text, out int vehiculoId))
                {
                    MessageBox.Show("Ingrese un VehiculoID válido (número entero).");
                    return null;
                }

                if (!int.TryParse(txtDocumento.Text, out int conductorId))
                {
                    MessageBox.Show("Ingrese un Documento de Conductor válido (número entero).");
                    return null;
                }

                // Validar que la ruta no esté vacía
                if (string.IsNullOrWhiteSpace(txtRuta.Text))
                {
                    MessageBox.Show("Ingrese una Ruta válida.");
                    return null;
                }

                // Si todo es válido, retornar el objeto Viajes
                return new Viajes
                {
                    ViajeID = viajeId,
                    VehiculoID = vehiculoId,
                    ConductorID = conductorId,
                    Ruta = txtRuta.Text.Trim(),
                    FechaSalida = dtpSalida.Value,
                    FechaLlegada = dtpLlegada.Value,
                    Estado = cbEstado.Text
                };
            }
        }

        private void txtVehiculoID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var viaje = ObtenerViajeDesdeFormulario();

            if (viaje == null)
            {

                return;
            }


            if (viajeDAO.ViajeIdExistente(viaje.ViajeID))
            {
                MessageBox.Show("Ya existe un viaje con ese ID.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                viajeDAO.AgregarViaje(viaje);
                MessageBox.Show("Viaje agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


                CargarViajes();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al agregar el viaje:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            {
                // 1. Obtener los datos del formulario
                var viaje = ObtenerViajeDesdeFormulario();

                if (viaje == null)
                {
                    // Ya se mostró un mensaje de error desde el método
                    return;
                }

                // 2. Verificar que el viaje exista
                if (!viajeDAO.ViajeIdExistente(viaje.ViajeID))
                {
                    MessageBox.Show("No se encontró un viaje con ese ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Confirmar edición
                var confirmar = MessageBox.Show("¿Está seguro que desea actualizar este viaje?", "Confirmar edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmar != DialogResult.Yes)
                    return;

                // 4. Intentar editar el viaje
                try
                {
                    viajeDAO.ActualizarViaje(viaje);
                    MessageBox.Show("Viaje actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 5. Recargar la tabla
                    CargarViajes();

                    // 6. Limpiar campos

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al editar el viaje:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtViajeID.Text, out int viajeId))
            {
                MessageBox.Show("Ingrese un ID de viaje válido (número entero).");
                return;
            }

            // Confirmación opcional
            var confirm = MessageBox.Show("¿Está seguro que desea eliminar el viaje?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (!viajeDAO.ViajeIdExistente(viajeId))
                {
                    MessageBox.Show("No se encontró un viaje con ese ID.");
                    return;
                }

                viajeDAO.EliminarViaje(viajeId);
                MessageBox.Show("Viaje eliminado correctamente.");

                // Limpia o recarga la interfaz si es necesario
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al eliminar el viaje:\n" + ex.Message);
            }

            CargarViajes();
            txtViajeID.Clear();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtViajeID.Text, out int viajeId))
            {
                MessageBox.Show("Ingrese un ID de viaje válido (número entero).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Buscar el viaje en la base de datos
            var viaje = viajeDAO.BuscarViajePorId(viajeId);

            // 3. Verificar si se encontró
            if (viaje == null)
            {
                MessageBox.Show("No se encontró un viaje con ese ID.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 4. Cargar datos en el formulario
            txtVehiculoID.Text = viaje.VehiculoID.ToString();
            txtDocumento.Text = viaje.ConductorID.ToString();
            txtRuta.Text = viaje.Ruta;
            dtpSalida.Value = viaje.FechaSalida;
            dtpLlegada.Value = viaje.FechaLlegada;
            cbEstado.Text = viaje.Estado;
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarViajes(); // Este método actualiza el DataGridView con los viajes
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar los viajes:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvViajes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvViajes.Rows[e.RowIndex];
                txtViajeID.Text = row.Cells["ViajeID"].Value.ToString();
                txtVehiculoID.Text = row.Cells["VehiculoID"].Value.ToString();
                txtDocumento.Text = row.Cells["ConductorID"].Value.ToString();
                txtRuta.Text = row.Cells["Ruta"].Value.ToString();
                dtpSalida.Value = Convert.ToDateTime(row.Cells["FechaSalida"].Value);
                dtpLlegada.Value = Convert.ToDateTime(row.Cells["FechaLlegada"].Value);
                cbEstado.Text = row.Cells["Estado"].Value.ToString();
            }

            if (e.RowIndex >= 0 && dgvViajes.Rows[e.RowIndex].Cells["ViajeID"].Value != null)
            {
                DataGridViewRow row = dgvViajes.Rows[e.RowIndex];
                txtViajeID.Text = row.Cells["ViajeID"].Value.ToString();
                txtVehiculoID.Text = row.Cells["VehiculoID"].Value.ToString();
                txtDocumento.Text = row.Cells["ConductorID"].Value.ToString();
                txtRuta.Text = row.Cells["Ruta"].Value.ToString();
                dtpSalida.Value = Convert.ToDateTime(row.Cells["FechaSalida"].Value);
                dtpLlegada.Value = Convert.ToDateTime(row.Cells["FechaLlegada"].Value);
                cbEstado.Text = row.Cells["Estado"].Value.ToString();

              
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void txtViajeID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
