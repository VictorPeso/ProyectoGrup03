
namespace DAYAN_
{
    partial class Registrarse
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Accept = new System.Windows.Forms.Button();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.ContraTextBox = new System.Windows.Forms.TextBox();
            this.Salir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mongolian Baiti", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "DAYAN!";
            // 
            // Accept
            // 
            this.Accept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Accept.Location = new System.Drawing.Point(66, 230);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(275, 47);
            this.Accept.TabIndex = 1;
            this.Accept.Text = "Iniciar Sesión";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // NameTextBox
            // 
            this.NameTextBox.AccessibleName = "";
            this.NameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.NameTextBox.Location = new System.Drawing.Point(66, 134);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(275, 22);
            this.NameTextBox.TabIndex = 2;
            this.NameTextBox.Tag = "";
            this.NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NameTextBox.UseWaitCursor = true;
            this.NameTextBox.Enter += new System.EventHandler(this.NameTextBox_Enter);
            this.NameTextBox.Leave += new System.EventHandler(this.NameTextBox_Leave);
            // 
            // ContraTextBox
            // 
            this.ContraTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContraTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.ContraTextBox.Location = new System.Drawing.Point(66, 185);
            this.ContraTextBox.Name = "ContraTextBox";
            this.ContraTextBox.Size = new System.Drawing.Size(275, 22);
            this.ContraTextBox.TabIndex = 3;
            this.ContraTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ContraTextBox.Enter += new System.EventHandler(this.ContraTextBox_Enter);
            this.ContraTextBox.Leave += new System.EventHandler(this.ContraTextBox_Leave);
            // 
            // Salir
            // 
            this.Salir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Salir.Location = new System.Drawing.Point(66, 283);
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(275, 23);
            this.Salir.TabIndex = 5;
            this.Salir.Text = "Salir";
            this.Salir.UseVisualStyleBackColor = true;
            this.Salir.Click += new System.EventHandler(this.Salir_Click);
            // 
            // Registrarse
            // 
            this.AccessibleName = "Nombre";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 342);
            this.Controls.Add(this.Salir);
            this.Controls.Add(this.ContraTextBox);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.label1);
            this.Name = "Registrarse";
            this.Text = "Registrarse";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox ContraTextBox;
        private System.Windows.Forms.Button Salir;
    }
}

