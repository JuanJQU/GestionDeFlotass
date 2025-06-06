namespace GestiondeFlotas
{
    partial class AdminMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu = new System.Windows.Forms.Panel();
            this.btnGC = new System.Windows.Forms.Button();
            this.btnGV = new System.Windows.Forms.Button();
            this.btnMV = new System.Windows.Forms.Button();
            this.btnAViajes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRHViajes = new System.Windows.Forms.Button();
            this.btnOutSesion = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.LightSteelBlue;
            this.menu.Controls.Add(this.button1);
            this.menu.Controls.Add(this.btnRHViajes);
            this.menu.Controls.Add(this.btnAViajes);
            this.menu.Controls.Add(this.btnMV);
            this.menu.Controls.Add(this.btnGV);
            this.menu.Controls.Add(this.btnGC);
            this.menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(292, 523);
            this.menu.TabIndex = 0;
            // 
            // btnGC
            // 
            this.btnGC.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnGC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGC.ForeColor = System.Drawing.Color.Black;
            this.btnGC.Location = new System.Drawing.Point(0, 123);
            this.btnGC.Name = "btnGC";
            this.btnGC.Size = new System.Drawing.Size(292, 50);
            this.btnGC.TabIndex = 0;
            this.btnGC.Text = "Gestión de Conductores";
            this.btnGC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGC.UseVisualStyleBackColor = false;
            this.btnGC.Click += new System.EventHandler(this.btnGC_Click);
            // 
            // btnGV
            // 
            this.btnGV.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnGV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGV.ForeColor = System.Drawing.Color.Black;
            this.btnGV.Location = new System.Drawing.Point(0, 169);
            this.btnGV.Name = "btnGV";
            this.btnGV.Size = new System.Drawing.Size(292, 50);
            this.btnGV.TabIndex = 1;
            this.btnGV.Text = "Gestión de Vehículos";
            this.btnGV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGV.UseVisualStyleBackColor = false;
            this.btnGV.Click += new System.EventHandler(this.btnGV_Click);
            // 
            // btnMV
            // 
            this.btnMV.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnMV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMV.ForeColor = System.Drawing.Color.Black;
            this.btnMV.Location = new System.Drawing.Point(0, 216);
            this.btnMV.Name = "btnMV";
            this.btnMV.Size = new System.Drawing.Size(292, 50);
            this.btnMV.TabIndex = 2;
            this.btnMV.Text = "Mantenimiento de Vehículos";
            this.btnMV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMV.UseVisualStyleBackColor = false;
            this.btnMV.Click += new System.EventHandler(this.btnMV_Click);
            // 
            // btnAViajes
            // 
            this.btnAViajes.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnAViajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAViajes.ForeColor = System.Drawing.Color.Black;
            this.btnAViajes.Location = new System.Drawing.Point(0, 261);
            this.btnAViajes.Name = "btnAViajes";
            this.btnAViajes.Size = new System.Drawing.Size(292, 50);
            this.btnAViajes.TabIndex = 3;
            this.btnAViajes.Text = "Asignar Viajes";
            this.btnAViajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAViajes.UseVisualStyleBackColor = false;
            this.btnAViajes.Click += new System.EventHandler(this.btnAViajes_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(398, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Perfil del Administrador";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(276, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 31);
            this.label2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SlateGray;
            this.label3.Location = new System.Drawing.Point(483, 308);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Gestiona y Asigana Viajes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SteelBlue;
            this.label4.Location = new System.Drawing.Point(404, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(311, 29);
            this.label4.TabIndex = 9;
            this.label4.Text = "¡Bienvenido Nuevamente!";
            // 
            // btnRHViajes
            // 
            this.btnRHViajes.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnRHViajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRHViajes.ForeColor = System.Drawing.Color.Black;
            this.btnRHViajes.Location = new System.Drawing.Point(0, 308);
            this.btnRHViajes.Name = "btnRHViajes";
            this.btnRHViajes.Size = new System.Drawing.Size(292, 50);
            this.btnRHViajes.TabIndex = 4;
            this.btnRHViajes.Text = "Reportes de uso";
            this.btnRHViajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRHViajes.UseVisualStyleBackColor = false;
            this.btnRHViajes.Click += new System.EventHandler(this.btnRHViajes_Click);
            // 
            // btnOutSesion
            // 
            this.btnOutSesion.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnOutSesion.Location = new System.Drawing.Point(509, 471);
            this.btnOutSesion.Name = "btnOutSesion";
            this.btnOutSesion.Size = new System.Drawing.Size(95, 40);
            this.btnOutSesion.TabIndex = 11;
            this.btnOutSesion.Text = "Cerrar Sesión";
            this.btnOutSesion.UseVisualStyleBackColor = false;
            this.btnOutSesion.Click += new System.EventHandler(this.btnOutSesion_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(0, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(292, 50);
            this.button1.TabIndex = 5;
            this.button1.Text = "Historial de Mantenimiento";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AdminMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(811, 523);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.btnOutSesion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AdminMenu";
            this.Text = "AdminMenu";
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel menu;
        private System.Windows.Forms.Button btnAViajes;
        private System.Windows.Forms.Button btnMV;
        private System.Windows.Forms.Button btnGV;
        private System.Windows.Forms.Button btnGC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRHViajes;
        private System.Windows.Forms.Button btnOutSesion;
        private System.Windows.Forms.Button button1;
    }
}