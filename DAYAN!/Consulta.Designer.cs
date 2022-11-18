
namespace DAYAN_
{
    partial class Consulta
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
            this.Consultar = new System.Windows.Forms.Button();
            this.Desconectarse = new System.Windows.Forms.Button();
            this.JgEnPartidaCheck = new System.Windows.Forms.CheckBox();
            this.PartidaLargaCheck = new System.Windows.Forms.CheckBox();
            this.JugadorGanaCheck = new System.Windows.Forms.CheckBox();
            this.listaUsuarios = new System.Windows.Forms.DataGridView();
            this.JugadorTBx = new System.Windows.Forms.TextBox();
            this.PartidaTBx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.listaUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // Consultar
            // 
            this.Consultar.Location = new System.Drawing.Point(25, 254);
            this.Consultar.Name = "Consultar";
            this.Consultar.Size = new System.Drawing.Size(115, 48);
            this.Consultar.TabIndex = 0;
            this.Consultar.Text = "Consultar";
            this.Consultar.UseVisualStyleBackColor = true;
            this.Consultar.Click += new System.EventHandler(this.Consultar_Click);
            // 
            // Desconectarse
            // 
            this.Desconectarse.Location = new System.Drawing.Point(146, 254);
            this.Desconectarse.Name = "Desconectarse";
            this.Desconectarse.Size = new System.Drawing.Size(115, 48);
            this.Desconectarse.TabIndex = 1;
            this.Desconectarse.Text = "Desconectarse";
            this.Desconectarse.UseVisualStyleBackColor = true;
            this.Desconectarse.Click += new System.EventHandler(this.Desconectarse_Click);
            // 
            // JgEnPartidaCheck
            // 
            this.JgEnPartidaCheck.AutoSize = true;
            this.JgEnPartidaCheck.Font = new System.Drawing.Font("Mongolian Baiti", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JgEnPartidaCheck.Location = new System.Drawing.Point(150, 109);
            this.JgEnPartidaCheck.Name = "JgEnPartidaCheck";
            this.JgEnPartidaCheck.Size = new System.Drawing.Size(234, 18);
            this.JgEnPartidaCheck.TabIndex = 2;
            this.JgEnPartidaCheck.Text = "Buscar si el jugador jugó en la partida.";
            this.JgEnPartidaCheck.UseVisualStyleBackColor = true;
            // 
            // PartidaLargaCheck
            // 
            this.PartidaLargaCheck.AutoSize = true;
            this.PartidaLargaCheck.Font = new System.Drawing.Font("Mongolian Baiti", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartidaLargaCheck.Location = new System.Drawing.Point(150, 150);
            this.PartidaLargaCheck.Name = "PartidaLargaCheck";
            this.PartidaLargaCheck.Size = new System.Drawing.Size(264, 18);
            this.PartidaLargaCheck.TabIndex = 3;
            this.PartidaLargaCheck.Text = "Buscar si la partida duró más de 10 minutos.";
            this.PartidaLargaCheck.UseVisualStyleBackColor = true;
            // 
            // JugadorGanaCheck
            // 
            this.JugadorGanaCheck.AutoSize = true;
            this.JugadorGanaCheck.Font = new System.Drawing.Font("Mongolian Baiti", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JugadorGanaCheck.Location = new System.Drawing.Point(150, 191);
            this.JugadorGanaCheck.Name = "JugadorGanaCheck";
            this.JugadorGanaCheck.Size = new System.Drawing.Size(231, 18);
            this.JugadorGanaCheck.TabIndex = 4;
            this.JugadorGanaCheck.Text = "Buscar si el jugador ganó esta partida.";
            this.JugadorGanaCheck.UseVisualStyleBackColor = true;
            // 
            // listaUsuarios
            // 
            this.listaUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listaUsuarios.Location = new System.Drawing.Point(438, 35);
            this.listaUsuarios.Name = "listaUsuarios";
            this.listaUsuarios.Size = new System.Drawing.Size(189, 267);
            this.listaUsuarios.TabIndex = 6;
            // 
            // JugadorTBx
            // 
            this.JugadorTBx.Location = new System.Drawing.Point(24, 127);
            this.JugadorTBx.Name = "JugadorTBx";
            this.JugadorTBx.Size = new System.Drawing.Size(100, 20);
            this.JugadorTBx.TabIndex = 7;
            // 
            // PartidaTBx
            // 
            this.PartidaTBx.Location = new System.Drawing.Point(24, 180);
            this.PartidaTBx.Name = "PartidaTBx";
            this.PartidaTBx.Size = new System.Drawing.Size(100, 20);
            this.PartidaTBx.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mongolian Baiti", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 37);
            this.label1.TabIndex = 9;
            this.label1.Text = "Consulta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Jugador";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Partida";
            // 
            // Consulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 340);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PartidaTBx);
            this.Controls.Add(this.JugadorTBx);
            this.Controls.Add(this.listaUsuarios);
            this.Controls.Add(this.JugadorGanaCheck);
            this.Controls.Add(this.PartidaLargaCheck);
            this.Controls.Add(this.JgEnPartidaCheck);
            this.Controls.Add(this.Desconectarse);
            this.Controls.Add(this.Consultar);
            this.Name = "Consulta";
            this.Text = "Consulta";
            this.Load += new System.EventHandler(this.Consulta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listaUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Consultar;
        private System.Windows.Forms.Button Desconectarse;
        private System.Windows.Forms.CheckBox JgEnPartidaCheck;
        private System.Windows.Forms.CheckBox PartidaLargaCheck;
        private System.Windows.Forms.CheckBox JugadorGanaCheck;
        private System.Windows.Forms.DataGridView listaUsuarios;
        private System.Windows.Forms.TextBox JugadorTBx;
        private System.Windows.Forms.TextBox PartidaTBx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}