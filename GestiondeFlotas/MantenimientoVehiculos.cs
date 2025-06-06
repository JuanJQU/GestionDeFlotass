using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiondeFlotas
{
    public partial class MantenimientoVehiculos : Form
    {
        private Mantenimiento mantenimiento = new Mantenimiento();

        public MantenimientoVehiculos()
        {
            InitializeComponent();
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
        }

        private void dgvConductores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Mensajes de depuración para saber qué falla
                if (!int.TryParse(txtID.Text, out int mantenimientoId))
                {
                    MessageBox.Show("El ID de mantenimiento debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtVehiculoID.Text, out int vehiculoId))
                {
                    MessageBox.Show("El ID de vehículo debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(cbTipoMantenimiento.Text))
                {
                    MessageBox.Show("Selecciona un tipo de mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("La descripción no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime fecha = dtpMantenimiento.Value;
                string tipo = cbTipoMantenimiento.Text;
                string descripcion = txtDescripcion.Text;

                bool exito = mantenimiento.Agregar(mantenimientoId, vehiculoId, fecha, tipo, descripcion);

                if (exito)
                {
                    MessageBox.Show("Mantenimiento agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int mantenimientoId, vehiculoId;
            bool tieneMantenimientoId = int.TryParse(txtID.Text, out mantenimientoId);
            bool tieneVehiculoId = int.TryParse(txtVehiculoID.Text, out vehiculoId);

            if (!tieneMantenimientoId && !tieneVehiculoId)
            {
                MessageBox.Show("Ingrese al menos el ID de mantenimiento o el ID de vehículo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable resultados;

            if (tieneMantenimientoId && tieneVehiculoId)
            {
                // Buscar por ambos
                resultados = mantenimiento.BuscarPorIds(mantenimientoId, vehiculoId);
            }
            else if (tieneMantenimientoId)
            {
                resultados = mantenimiento.BuscarPorMantenimientoId(mantenimientoId);
            }
            else // tieneVehiculoId
            {
                resultados = mantenimiento.BuscarPorVehiculoId(vehiculoId);
            }

            dgvMantenimientos.DataSource = resultados;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text, out int mantenimientoId))
                {
                    MessageBox.Show("El ID de mantenimiento debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtVehiculoID.Text, out int vehiculoId))
                {
                    MessageBox.Show("El ID de vehículo debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(cbTipoMantenimiento.Text))
                {
                    MessageBox.Show("Selecciona un tipo de mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("La descripción no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime fecha = dtpMantenimiento.Value;
                string tipo = cbTipoMantenimiento.Text;
                string descripcion = txtDescripcion.Text;

                bool exito = mantenimiento.Editar(mantenimientoId, vehiculoId, fecha, tipo, descripcion);

                if (exito)
                {
                    MessageBox.Show("Mantenimiento editado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo editar el mantenimiento. Verifica que el ID exista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text, out int mantenimientoId))
                {
                    MessageBox.Show("El ID de mantenimiento debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Debes agregar el método Eliminar en la clase Mantenimiento (ver abajo)
                bool exito = mantenimiento.Eliminar(mantenimientoId);

                    if (exito)
                    {
                    MessageBox.Show("Mantenimiento eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el mantenimiento. Verifica que el ID exista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminMenu menu = new AdminMenu();
            menu.Show();
            this.Hide(); // Ocultar el gestor de conductores
        }

        private void ActualizarGrid()
        {
            dgvMantenimientos.DataSource = mantenimiento.Buscar();
        }
    }
}
