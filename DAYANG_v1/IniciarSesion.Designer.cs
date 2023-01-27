
namespace DAYANG_v1
{
    partial class IniciarSesion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IniciarSesion));
            this.Accept = new System.Windows.Forms.Button();
            this.Salir = new System.Windows.Forms.Button();
            this.Title_label = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.ContraTextBox = new System.Windows.Forms.TextBox();
            this.regbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Accept
            // 
            this.Accept.BackColor = System.Drawing.Color.DarkRed;
            this.Accept.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Accept.FlatAppearance.BorderSize = 0;
            this.Accept.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Accept.Font = new System.Drawing.Font("SWIsot1", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Accept.ForeColor = System.Drawing.Color.White;
            this.Accept.Location = new System.Drawing.Point(170, 288);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(190, 46);
            this.Accept.TabIndex = 4;
            this.Accept.Text = "Iniciar Sesión";
            this.Accept.UseVisualStyleBackColor = false;
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // Salir
            // 
            this.Salir.BackColor = System.Drawing.Color.DarkRed;
            this.Salir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Salir.Font = new System.Drawing.Font("SWIsot1", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Salir.ForeColor = System.Drawing.Color.White;
            this.Salir.Location = new System.Drawing.Point(170, 369);
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(190, 23);
            this.Salir.TabIndex = 1;
            this.Salir.Text = "Salir";
            this.Salir.UseVisualStyleBackColor = false;
            this.Salir.Click += new System.EventHandler(this.Salir_Click);
            // 
            // Title_label
            // 
            this.Title_label.AutoSize = true;
            this.Title_label.BackColor = System.Drawing.Color.Transparent;
            this.Title_label.Font = new System.Drawing.Font("SWIsot1", 50.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title_label.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Title_label.Location = new System.Drawing.Point(38, 32);
            this.Title_label.Name = "Title_label";
            this.Title_label.Size = new System.Drawing.Size(454, 88);
            this.Title_label.TabIndex = 1;
            this.Title_label.Text = "¡DAYANG!";
            // 
            // NameTextBox
            // 
            this.NameTextBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.NameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NameTextBox.Font = new System.Drawing.Font("SWIsot1", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.NameTextBox.Location = new System.Drawing.Point(38, 169);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(454, 29);
            this.NameTextBox.TabIndex = 2;
            this.NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NameTextBox.Enter += new System.EventHandler(this.NameTextBox_Enter);
            this.NameTextBox.Leave += new System.EventHandler(this.NameTextBox_Leave);
            // 
            // ContraTextBox
            // 
            this.ContraTextBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.ContraTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContraTextBox.Font = new System.Drawing.Font("SWIsot1", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContraTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.ContraTextBox.Location = new System.Drawing.Point(38, 224);
            this.ContraTextBox.Name = "ContraTextBox";
            this.ContraTextBox.Size = new System.Drawing.Size(454, 29);
            this.ContraTextBox.TabIndex = 3;
            this.ContraTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ContraTextBox.Enter += new System.EventHandler(this.ContraTextBox_Enter);
            this.ContraTextBox.Leave += new System.EventHandler(this.ContraTextBox_Leave);
            // 
            // regbutton
            // 
            this.regbutton.BackColor = System.Drawing.Color.DarkRed;
            this.regbutton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.regbutton.FlatAppearance.BorderSize = 0;
            this.regbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.regbutton.Font = new System.Drawing.Font("SWIsot1", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regbutton.ForeColor = System.Drawing.Color.White;
            this.regbutton.Location = new System.Drawing.Point(170, 340);
            this.regbutton.Name = "regbutton";
            this.regbutton.Size = new System.Drawing.Size(190, 23);
            this.regbutton.TabIndex = 5;
            this.regbutton.Text = "Registrarse";
            this.regbutton.UseVisualStyleBackColor = false;
            this.regbutton.Click += new System.EventHandler(this.regbutton_Click);
            // 
            // IniciarSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(514, 413);
            this.Controls.Add(this.regbutton);
            this.Controls.Add(this.ContraTextBox);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.Title_label);
            this.Controls.Add(this.Salir);
            this.Controls.Add(this.Accept);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "IniciarSesion";
            this.Text = "IniciarSesion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button Salir;
        private System.Windows.Forms.Label Title_label;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox ContraTextBox;
        private System.Windows.Forms.Button regbutton;
    }
}