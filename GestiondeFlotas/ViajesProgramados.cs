﻿using System;
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
    public partial class ViajesProgramados: Form
    {
        public ViajesProgramados()
        {
            InitializeComponent();
        }

        private void txtVehiculoID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ConductorMenu menuC = new ConductorMenu();
            menuC.Show();
            this.Hide(); // Ocultar el formulario de viajes programados
        }
    }
}
