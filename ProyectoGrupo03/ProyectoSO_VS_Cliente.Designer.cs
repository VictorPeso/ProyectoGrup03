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
            this.Contraseña = new System.Windows.Forms.Label();
            this.Usuario = new System.Windows.Forms.Label();
            this.textUsuario = new System.Windows.Forms.TextBox();
            this.textContraseña = new System.Windows.Forms.TextBox();
            this.Aceptar = new System.Windows.Forms.Button();
            this.Cancelar = new System.Windows.Forms.Button();
            this.PartidaBox = new System.Windows.Forms.TextBox();
            this.JugadorBox = new System.Windows.Forms.TextBox();
            this.Jugador = new System.Windows.Forms.Label();
            this.Partida = new System.Windows.Forms.Label();
            this.JenP = new System.Windows.Forms.CheckBox();
            this.Plarga = new System.Windows.Forms.CheckBox();
            this.JOnline = new System.Windows.Forms.CheckBox();
            this.Aceptar2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Contraseña
            // 
            this.Contraseña.AutoSize = true;
            this.Contraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Contraseña.Location = new System.Drawing.Point(28, 92);
            this.Contraseña.Name = "Contraseña";
            this.Contraseña.Size = new System.Drawing.Size(119, 20);
            this.Contraseña.TabIndex = 0;
            this.Contraseña.Text = "CONTRASEÑA";
            // 
            // Usuario
            // 
            this.Usuario.AutoSize = true;
            this.Usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Usuario.Location = new System.Drawing.Point(28, 36);
            this.Usuario.Name = "Usuario";
            this.Usuario.Size = new System.Drawing.Size(84, 20);
            this.Usuario.TabIndex = 1;
            this.Usuario.Text = "USUARIO";
            this.Usuario.Click += new System.EventHandler(this.label2_Click);
            // 
            // textUsuario
            // 
            this.textUsuario.Location = new System.Drawing.Point(203, 38);
            this.textUsuario.Name = "textUsuario";
            this.textUsuario.Size = new System.Drawing.Size(243, 20);
            this.textUsuario.TabIndex = 2;
            // 
            // textContraseña
            // 
            this.textContraseña.Location = new System.Drawing.Point(203, 92);
            this.textContraseña.Name = "textContraseña";
            this.textContraseña.Size = new System.Drawing.Size(243, 20);
            this.textContraseña.TabIndex = 3;
            // 
            // Aceptar
            // 
            this.Aceptar.Location = new System.Drawing.Point(105, 166);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(109, 51);
            this.Aceptar.TabIndex = 4;
            this.Aceptar.Text = "ACEPTAR";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // Cancelar
            // 
            this.Cancelar.Location = new System.Drawing.Point(283, 166);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(109, 51);
            this.Cancelar.TabIndex = 5;
            this.Cancelar.Text = "CANCELAR";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // PartidaBox
            // 
            this.PartidaBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartidaBox.Location = new System.Drawing.Point(725, 94);
            this.PartidaBox.Name = "PartidaBox";
            this.PartidaBox.Size = new System.Drawing.Size(114, 26);
            this.PartidaBox.TabIndex = 6;
            // 
            // JugadorBox
            // 
            this.JugadorBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JugadorBox.Location = new System.Drawing.Point(542, 94);
            this.JugadorBox.Name = "JugadorBox";
            this.JugadorBox.Size = new System.Drawing.Size(114, 26);
            this.JugadorBox.TabIndex = 7;
            this.JugadorBox.TextChanged += new System.EventHandler(this.JugadorBox_TextChanged);
            // 
            // Jugador
            // 
            this.Jugador.AutoSize = true;
            this.Jugador.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Jugador.Location = new System.Drawing.Point(538, 51);
            this.Jugador.Name = "Jugador";
            this.Jugador.Size = new System.Drawing.Size(89, 20);
            this.Jugador.TabIndex = 8;
            this.Jugador.Text = "JUGADOR";
            // 
            // Partida
            // 
            this.Partida.AutoSize = true;
            this.Partida.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Partida.Location = new System.Drawing.Point(721, 51);
            this.Partida.Name = "Partida";
            this.Partida.Size = new System.Drawing.Size(79, 20);
            this.Partida.TabIndex = 9;
            this.Partida.Text = "PARTIDA";
            // 
            // JenP
            // 
            this.JenP.AutoSize = true;
            this.JenP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JenP.Location = new System.Drawing.Point(542, 166);
            this.JenP.Name = "JenP";
            this.JenP.Size = new System.Drawing.Size(305, 24);
            this.JenP.TabIndex = 10;
            this.JenP.Text = "Buscar si el jugador jugó en esa partida";
            this.JenP.UseVisualStyleBackColor = true;
            // 
            // Plarga
            // 
            this.Plarga.AutoSize = true;
            this.Plarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Plarga.Location = new System.Drawing.Point(542, 211);
            this.Plarga.Name = "Plarga";
            this.Plarga.Size = new System.Drawing.Size(336, 24);
            this.Plarga.TabIndex = 11;
            this.Plarga.Text = "Buscar si la partida duró mas de 10 minutos";
            this.Plarga.UseVisualStyleBackColor = true;
            this.Plarga.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // JOnline
            // 
            this.JOnline.AutoSize = true;
            this.JOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JOnline.Location = new System.Drawing.Point(542, 257);
            this.JOnline.Name = "JOnline";
            this.JOnline.Size = new System.Drawing.Size(247, 24);
            this.JOnline.TabIndex = 12;
            this.JOnline.Text = "Buscar si el jugador esta online";
            this.JOnline.UseVisualStyleBackColor = true;
            // 
            // Aceptar2
            // 
            this.Aceptar2.Location = new System.Drawing.Point(652, 330);
            this.Aceptar2.Name = "Aceptar2";
            this.Aceptar2.Size = new System.Drawing.Size(109, 51);
            this.Aceptar2.TabIndex = 13;
            this.Aceptar2.Text = "ACEPTAR";
            this.Aceptar2.UseVisualStyleBackColor = true;
            this.Aceptar2.Click += new System.EventHandler(this.Aceptar2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 488);
            this.Controls.Add(this.Aceptar2);
            this.Controls.Add(this.JOnline);
            this.Controls.Add(this.Plarga);
            this.Controls.Add(this.JenP);
            this.Controls.Add(this.Partida);
            this.Controls.Add(this.Jugador);
            this.Controls.Add(this.JugadorBox);
            this.Controls.Add(this.PartidaBox);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.Aceptar);
            this.Controls.Add(this.textContraseña);
            this.Controls.Add(this.textUsuario);
            this.Controls.Add(this.Usuario);
            this.Controls.Add(this.Contraseña);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Contraseña;
        private System.Windows.Forms.Label Usuario;
        private System.Windows.Forms.TextBox textUsuario;
        private System.Windows.Forms.TextBox textContraseña;
        private System.Windows.Forms.Button Aceptar;
        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.TextBox PartidaBox;
        private System.Windows.Forms.TextBox JugadorBox;
        private System.Windows.Forms.Label Jugador;
        private System.Windows.Forms.Label Partida;
        private System.Windows.Forms.CheckBox JenP;
        private System.Windows.Forms.CheckBox Plarga;
        private System.Windows.Forms.CheckBox JOnline;
        private System.Windows.Forms.Button Aceptar2;
    }
}

