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
    public partial class AdminMenu: Form
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void btnGC_Click(object sender, EventArgs e)
        {
            GestorConductores gc = new GestorConductores();
            gc.Show();
            this.Hide(); // Ocultar el menú de administrador
        }

        private void btnGV_Click(object sender, EventArgs e)
        {
            GestionVehiculos gv = new GestionVehiculos();
            gv.Show();
            this.Hide(); // Ocultar el menú de administrador
        }

        private void btnMV_Click(object sender, EventArgs e)
        {
            MantenimientoVehiculos mv = new MantenimientoVehiculos();
            mv.Show();
            this.Hide(); // Ocultar el menú de administrador
        }

        private void btnAViajes_Click(object sender, EventArgs e)
        {
            AsignarViajes viajes = new AsignarViajes();
            viajes.Show();
            this.Hide(); // Ocultar el menú de administrador
        }

        private void btnRHViajes_Click(object sender, EventArgs e)
        {
            ReportesUsoVehiculos rpv = new ReportesUsoVehiculos();
            rpv.Show();
            this.Hide(); // Ocultar el menú de administrador
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HistorialMantenimiento hm = new HistorialMantenimiento();
            hm.Show();
            this.Hide(); // Ocultar el menú de administrador
        }

        private void btnOutSesion_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide(); // Ocultar el menú de administrador
        }
    }
}
