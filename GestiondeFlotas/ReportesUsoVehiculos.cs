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
    public partial class ReportesUsoVehiculos : Form
    {
        public ReportesUsoVehiculos()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminMenu menu = new AdminMenu();
            menu.Show();
            this.Hide(); // Ocultar el gestor de conductores
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtViajeID.Text, out int viajeId))
            {
                MessageBox.Show("Ingresa un ID de viaje válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connDB = "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connDB))
            {
                string query = "SELECT * FROM ReportesUsoVehiculo WHERE ViajeID = @ViajeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ViajeID", viajeId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron reportes con ese ViajeID.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dgvViajes.DataSource = dt;
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            string connDB = "Data Source=.\\SQLEXPRESS;Initial Catalog=GestionFlotas;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connDB))
            {
                string query = "SELECT * FROM ReportesUsoVehiculo";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvViajes.DataSource = dt;
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            {
                txtViajeID.Clear();
                txtVehiculoID.Clear();
                txtDocumento.Clear();
                txtMatricula.Clear();
                dtpLlegada.Value = DateTime.Today;
                dtpSalida.Value = DateTime.Today;
                dgvViajes.DataSource = null;
                dgvViajes.Rows.Clear();
            }
        }
    }

}
