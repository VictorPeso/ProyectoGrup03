using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            server.Shutdown(SocketShutdown.Both);
            server.Close();
            
            this.Close();
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);

            }
            catch (SocketException ex)
            {
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            string mensaje = "1/" + Usuario.Text + "/" + Contraseña.Text;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            if (mensaje == "Correctado")
                MessageBox.Show("Conectado.");
            else
            {
                string mensaje2 = "0/";

                byte[] msg3 = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                server.Send(msg3);

                server.Shutdown(SocketShutdown.Both);
                server.Close();
                MessageBox.Show("Contraseña o usuario incorrecto, intentelo de nuevo.");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Aceptar2_Click(object sender, EventArgs e)
        {
            if (JenP.Checked)
            {
                string mensaje = "2/" + Usuario.Text + "/" + Contraseña.Text + "/" + JugadorBox.Text + "/" + PartidaBox.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                if (mensaje == "SI")
                    MessageBox.Show(JugadorBox.Text + " ha jugado la partida");
                else
                    MessageBox.Show(JugadorBox.Text + " no ha jugado la partida");
            }
            else if (Plarga.Checked)
            {
                string mensaje = "3/" + Usuario.Text + "/" + Contraseña.Text + "/" + JugadorBox.Text + "/" + PartidaBox.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];


                if (mensaje == "SI")
                    MessageBox.Show(PartidaBox.Text + " dura mas de 10 minutos");
                else
                    MessageBox.Show(PartidaBox.Text + " no dura mas de 10 minutos");

            }
            else
            {
                string mensaje = "4/" + Usuario.Text + "/" + Contraseña.Text + "/" + JugadorBox.Text + "/" + PartidaBox.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];


                if (mensaje == "SI")
                    MessageBox.Show(JugadorBox.Text + " esta en linea");
                else
                    MessageBox.Show(JugadorBox.Text + " no esta en linea");

            }
        }

        private void JugadorBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

