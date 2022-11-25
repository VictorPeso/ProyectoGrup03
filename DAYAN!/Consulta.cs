using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DAYAN_
{
    public partial class Consulta : Form
    {
        Socket server;
        Thread atender;
        string Usuario;
        string nombredestino;

        public Consulta()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            // Necesario para que los elementos de los formularios puedan ser
            // accedidos desde threads diferentes a los que los crearon.
            listaUsuarios.ColumnHeadersVisible = false;
            listaUsuarios.RowHeadersVisible = false;
            listaUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listaUsuarios.ReadOnly = true;
            //listaUsuarios.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void SetSocket(Socket s)
        {
            this.server = s;
        }

        public void SetUsuario(string u)
        {
            this.Usuario = u;
        }

        public void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string trozo = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] mensaje = trozo.Split('/');
                int codigo = Convert.ToInt32(mensaje[0]);

                switch (codigo)
                {
                    case 2:
                        if (mensaje[1] == "SI")
                            MessageBox.Show(JugadorTBx.Text + " ha jugado la partida");
                        else
                            MessageBox.Show(JugadorTBx.Text + " no ha jugado la partida");
                        break;

                    case 3:
                        if (mensaje[1] == "NF")
                            MessageBox.Show("La partida " + PartidaTBx.Text + " no existe.");
                        else if (mensaje[1] == "SI")
                            MessageBox.Show("La partida " + PartidaTBx.Text + " dura más de 10 minutos.");
                        else
                            MessageBox.Show("La partida " + PartidaTBx.Text + " no dura más de 10 minutos.");
                        break;

                    case 4:
                        if (mensaje[1] == "NF")
                            MessageBox.Show(JugadorTBx.Text + " no jugó esta partida.");
                        else if (mensaje[1] == "SI")
                            MessageBox.Show(JugadorTBx.Text + " ganó esta partida.");
                        else
                            MessageBox.Show(JugadorTBx.Text + " no ganó esta partida.");
                        break;

                    case 5:
                        listaUsuarios.ColumnCount = 1;
                        listaUsuarios.RowCount = mensaje.Length -1;
                        int i = 1;
                        while (i < mensaje.Length)
                        {
                            string u = mensaje[i];
                            listaUsuarios[0, i - 1].Value = u;
                            i++;
                        }
                        break;
                    case 6:
                        DialogResult r = MessageBox.Show(mensaje[1] + " quiere que te unas a su partida", "Notificacion", MessageBoxButtons.YesNo);
                        //MessageBox.Show(Convert.ToString(r));

                        string respuesta = "6/" + mensaje[1] + "/" + Convert.ToString(r);
                        // Enviamos al servidor el nombre tecleado
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuesta);
                        server.Send(msg);

                        break;
                    case 7:
                        string nom = mensaje[1];
                        if (mensaje[2] == "SI")
                        {
                            MessageBox.Show(nom + " acepta jugar contigo");
                        }
                        else
                        {
                            MessageBox.Show(nom + " no acepta jugar contigo");
                        }
                        break;
                }
            }
        }

        private void Desconectarse_Click(object sender, EventArgs e)
        {
            string mensaje = "0/" + Usuario;
            if (server != null)
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                atender.Abort();
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            this.Close();
        }

        private void Consultar_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                if (JgEnPartidaCheck.Checked)
                {
                    string mensaje = "2/" + JugadorTBx.Text + "/" + PartidaTBx.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                if (PartidaLargaCheck.Checked)
                {
                    string mensaje = "3/" + PartidaTBx.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                if (JugadorGanaCheck.Checked)
                {
                    string mensaje = "4/" + JugadorTBx.Text + "/" + PartidaTBx.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
            }
            else
            {
                MessageBox.Show("No estas conectado");
            }
        }

        private void Consulta_Load(object sender, EventArgs e)
        {
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
            Usuariolb.Text = "Usuario: " + Usuario;
        }

        private void InvitarPartida_Click(object sender, EventArgs e)
        {
            string mensaje = "5/" + nombredestino;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //MessageBox.Show(nombredestino);
        }

        private void listaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nombredestino = listaUsuarios.Rows[e.RowIndex].Cells[0].Value.ToString();
        }
    }
}
