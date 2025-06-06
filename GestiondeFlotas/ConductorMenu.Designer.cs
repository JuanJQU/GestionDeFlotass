namespace GestiondeFlotas
{
    partial class ConductorMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnOutSesion = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHistorialViajesC = new System.Windows.Forms.Button();
            this.btnGC = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.LightSteelBlue;
            this.menu.Controls.Add(this.label1);
            this.menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(811, 106);
            this.menu.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(308, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Perfil Conductor";
            // 
            // btnOutSesion
            // 
            this.btnOutSesion.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnOutSesion.Location = new System.Drawing.Point(12, 298);
            this.btnOutSesion.Name = "btnOutSesion";
            this.btnOutSesion.Size = new System.Drawing.Size(95, 40);
            this.btnOutSesion.TabIndex = 96;
            this.btnOutSesion.Text = "Cerrar Sesión";
            this.btnOutSesion.UseVisualStyleBackColor = false;
            this.btnOutSesion.Click += new System.EventHandler(this.btnOutSesion_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnOutSesion);
            this.panel1.Controls.Add(this.btnHistorialViajesC);
            this.panel1.Controls.Add(this.btnGC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 350);
            this.panel1.TabIndex = 2;
            // 
            // btnHistorialViajesC
            // 
            this.btnHistorialViajesC.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnHistorialViajesC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialViajesC.ForeColor = System.Drawing.Color.Black;
            this.btnHistorialViajesC.Location = new System.Drawing.Point(0, 69);
            this.btnHistorialViajesC.Name = "btnHistorialViajesC";
            this.btnHistorialViajesC.Size = new System.Drawing.Size(247, 50);
            this.btnHistorialViajesC.TabIndex = 1;
            this.btnHistorialViajesC.Text = "Historial de Viajes";
            this.btnHistorialViajesC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistorialViajesC.UseVisualStyleBackColor = false;
            this.btnHistorialViajesC.Click += new System.EventHandler(this.btnHistorialViajesC_Click);
            // 
            // btnGC
            // 
            this.btnGC.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnGC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGC.ForeColor = System.Drawing.Color.Black;
            this.btnGC.Location = new System.Drawing.Point(0, 23);
            this.btnGC.Name = "btnGC";
            this.btnGC.Size = new System.Drawing.Size(247, 50);
            this.btnGC.TabIndex = 0;
            this.btnGC.Text = "Viajes Programados";
            this.btnGC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGC.UseVisualStyleBackColor = false;
            this.btnGC.Click += new System.EventHandler(this.btnGC_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button1.Location = new System.Drawing.Point(126, 298);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 40);
            this.button1.TabIndex = 97;
            this.button1.Text = "Cambiar Contraseña";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // ConductorMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(811, 456);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menu);
            this.Name = "ConductorMenu";
            this.Text = "ConductorMenu";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOutSesion;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnHistorialViajesC;
        private System.Windows.Forms.Button btnGC;
    }
}