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
    public partial class ConductorMenu: Form
    {
        public ConductorMenu()
        {
            InitializeComponent();
        }

        private void btnOutSesion_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide(); // Ocultar el menú de conductor
        }

        private void btnGC_Click(object sender, EventArgs e)
        {
            ViajesProgramados viajesProgramados = new ViajesProgramados();
            viajesProgramados.Show();
            this.Hide(); // Ocultar el menú de conductor
        }

        private void btnHistorialViajesC_Click(object sender, EventArgs e)
        {
            HistorialViajesConductor hvc = new HistorialViajesConductor();
            hvc.Show();
            this.Hide(); // Ocultar el menú de conductor
        }
    }
}
