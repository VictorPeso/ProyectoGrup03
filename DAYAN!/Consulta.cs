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

        public Consulta()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            // Necesario para que los elementos de los formularios puedan ser
            // accedidos desde threads diferentes a los que los crearon.
            listaUsuarios.ColumnHeadersVisible = false;
            listaUsuarios.RowHeadersVisible = false;
            listaUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje;

                switch (codigo)
                {
                    case 2:
                        mensaje = trozos[1].Split('\0')[0];
                        if (mensaje == "SI")
                            MessageBox.Show(JugadorTBx.Text + " ha jugado la partida");
                        else
                            MessageBox.Show(JugadorTBx.Text + " no ha jugado la partida");
                        break;

                    case 3:
                        mensaje = trozos[1].Split('\0')[0];
                        if (mensaje == "NF")
                            MessageBox.Show("La partida " + PartidaTBx.Text + " no existe.");
                        else if (mensaje == "SI")
                            MessageBox.Show("La partida " + PartidaTBx.Text + " dura más de 10 minutos.");
                        else
                            MessageBox.Show("La partida " + PartidaTBx.Text + " no dura más de 10 minutos.");
                        break;

                    case 4:
                        mensaje = trozos[1].Split('\0')[0];
                        if (mensaje == "NF")
                            MessageBox.Show(JugadorTBx.Text + " no jugó esta partida.");
                        else if (mensaje == "SI")
                            MessageBox.Show(JugadorTBx.Text + " ganó esta partida.");
                        else
                            MessageBox.Show(JugadorTBx.Text + " no ganó esta partida.");
                        break;

                    case 5:
                        listaUsuarios.ColumnCount = 1;
                        listaUsuarios.RowCount = trozos.Length -1;
                        int i = 1;
                        while (i < trozos.Length)
                        {
                            string u = trozos[i];
                            listaUsuarios[0, i - 1].Value = u;
                            i++;
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
        }
    }
}
