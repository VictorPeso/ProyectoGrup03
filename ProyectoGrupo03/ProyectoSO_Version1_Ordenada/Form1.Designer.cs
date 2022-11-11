namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.Aceptar = new System.Windows.Forms.Button();
            this.Cancelar = new System.Windows.Forms.Button();
            this.Aceptar2 = new System.Windows.Forms.Button();
            this.JenP = new System.Windows.Forms.CheckBox();
            this.Plarga = new System.Windows.Forms.CheckBox();
            this.JOnline = new System.Windows.Forms.CheckBox();
            this.textUsuario = new System.Windows.Forms.TextBox();
            this.Contraseña = new System.Windows.Forms.TextBox();
            this.JugadorBox = new System.Windows.Forms.TextBox();
            this.PartidaBox = new System.Windows.Forms.TextBox();
            this.Usuario = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cancelar2 = new System.Windows.Forms.Button();
            this.Conectados = new System.Windows.Forms.Button();
            this.listConectados = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Aceptar
            // 
            this.Aceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aceptar.Location = new System.Drawing.Point(26, 159);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(109, 43);
            this.Aceptar.TabIndex = 0;
            this.Aceptar.Text = "Aceptar";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // Cancelar
            // 
            this.Cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancelar.Location = new System.Drawing.Point(334, 159);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(109, 43);
            this.Cancelar.TabIndex = 1;
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.button2_Click);
            // 
            // Aceptar2
            // 
            this.Aceptar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aceptar2.Location = new System.Drawing.Point(26, 310);
            this.Aceptar2.Name = "Aceptar2";
            this.Aceptar2.Size = new System.Drawing.Size(109, 43);
            this.Aceptar2.TabIndex = 2;
            this.Aceptar2.Text = "Aceptar";
            this.Aceptar2.UseVisualStyleBackColor = true;
            this.Aceptar2.Click += new System.EventHandler(this.Aceptar2_Click);
            // 
            // JenP
            // 
            this.JenP.AutoSize = true;
            this.JenP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JenP.Location = new System.Drawing.Point(26, 149);
            this.JenP.Name = "JenP";
            this.JenP.Size = new System.Drawing.Size(305, 24);
            this.JenP.TabIndex = 3;
            this.JenP.Text = "Buscar si el jugador jugó en esa partida";
            this.JenP.UseVisualStyleBackColor = true;
            this.JenP.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Plarga
            // 
            this.Plarga.AutoSize = true;
            this.Plarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Plarga.Location = new System.Drawing.Point(26, 208);
            this.Plarga.Name = "Plarga";
            this.Plarga.Size = new System.Drawing.Size(336, 24);
            this.Plarga.TabIndex = 4;
            this.Plarga.Text = "Buscar si la partida duró mas de 10 minutos";
            this.Plarga.UseVisualStyleBackColor = true;
            // 
            // JOnline
            // 
            this.JOnline.AutoSize = true;
            this.JOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JOnline.Location = new System.Drawing.Point(26, 261);
            this.JOnline.Name = "JOnline";
            this.JOnline.Size = new System.Drawing.Size(247, 24);
            this.JOnline.TabIndex = 5;
            this.JOnline.Text = "Buscar si el jugador esta online";
            this.JOnline.UseVisualStyleBackColor = true;
            // 
            // textUsuario
            // 
            this.textUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textUsuario.Location = new System.Drawing.Point(189, 44);
            this.textUsuario.Name = "textUsuario";
            this.textUsuario.Size = new System.Drawing.Size(254, 26);
            this.textUsuario.TabIndex = 6;
            // 
            // Contraseña
            // 
            this.Contraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Contraseña.Location = new System.Drawing.Point(189, 104);
            this.Contraseña.Name = "Contraseña";
            this.Contraseña.PasswordChar = '*';
            this.Contraseña.Size = new System.Drawing.Size(254, 26);
            this.Contraseña.TabIndex = 7;
            // 
            // JugadorBox
            // 
            this.JugadorBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JugadorBox.Location = new System.Drawing.Point(26, 76);
            this.JugadorBox.Name = "JugadorBox";
            this.JugadorBox.Size = new System.Drawing.Size(147, 26);
            this.JugadorBox.TabIndex = 8;
            // 
            // PartidaBox
            // 
            this.PartidaBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartidaBox.Location = new System.Drawing.Point(267, 76);
            this.PartidaBox.Name = "PartidaBox";
            this.PartidaBox.Size = new System.Drawing.Size(147, 26);
            this.PartidaBox.TabIndex = 9;
            // 
            // Usuario
            // 
            this.Usuario.AutoSize = true;
            this.Usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Usuario.Location = new System.Drawing.Point(22, 44);
            this.Usuario.Name = "Usuario";
            this.Usuario.Size = new System.Drawing.Size(64, 20);
            this.Usuario.TabIndex = 10;
            this.Usuario.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Contraseña";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Jugador";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(263, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Partida";
            // 
            // Cancelar2
            // 
            this.Cancelar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancelar2.Location = new System.Drawing.Point(334, 310);
            this.Cancelar2.Name = "Cancelar2";
            this.Cancelar2.Size = new System.Drawing.Size(109, 43);
            this.Cancelar2.TabIndex = 14;
            this.Cancelar2.Text = "Cancelar";
            this.Cancelar2.UseVisualStyleBackColor = true;
            this.Cancelar2.Click += new System.EventHandler(this.Cancelar2_Click);
            // 
            // Conectados
            // 
            this.Conectados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Conectados.Location = new System.Drawing.Point(518, 44);
            this.Conectados.Name = "Conectados";
            this.Conectados.Size = new System.Drawing.Size(109, 43);
            this.Conectados.TabIndex = 15;
            this.Conectados.Text = "Actualizar";
            this.Conectados.UseVisualStyleBackColor = true;
            this.Conectados.Click += new System.EventHandler(this.Conectados_Click);
            // 
            // listConectados
            // 
            this.listConectados.FormattingEnabled = true;
            this.listConectados.Location = new System.Drawing.Point(518, 104);
            this.listConectados.Name = "listConectados";
            this.listConectados.Size = new System.Drawing.Size(120, 95);
            this.listConectados.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 390);
            this.Controls.Add(this.listConectados);
            this.Controls.Add(this.Conectados);
            this.Controls.Add(this.Cancelar2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Usuario);
            this.Controls.Add(this.PartidaBox);
            this.Controls.Add(this.JugadorBox);
            this.Controls.Add(this.Contraseña);
            this.Controls.Add(this.textUsuario);
            this.Controls.Add(this.JOnline);
            this.Controls.Add(this.Plarga);
            this.Controls.Add(this.JenP);
            this.Controls.Add(this.Aceptar2);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.Aceptar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Aceptar;
        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.Button Aceptar2;
        private System.Windows.Forms.CheckBox JenP;
        private System.Windows.Forms.CheckBox Plarga;
        private System.Windows.Forms.CheckBox JOnline;
        private System.Windows.Forms.TextBox textUsuario;
        private System.Windows.Forms.TextBox Contraseña;
        private System.Windows.Forms.TextBox JugadorBox;
        private System.Windows.Forms.TextBox PartidaBox;
        private System.Windows.Forms.Label Usuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Cancelar2;
        private System.Windows.Forms.Button Conectados;
        private System.Windows.Forms.ListBox listConectados;
    }
}

