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
            Aceptar2.Visible = false;
            Cancelar2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            PartidaBox.Visible = false;
            JugadorBox.Visible = false;
            JenP.Visible = false;
            Plarga.Visible = false;
            JOnline.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9060);

            //Creamos el socket 
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

            string mensaje = "1/" + textUsuario.Text + "/" + Contraseña.Text;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            if (mensaje == "Correcto")
            {
                MessageBox.Show("Conectado.");
                Aceptar2.Visible = true;
                Cancelar2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                PartidaBox.Visible = true;
                JugadorBox.Visible = true;
                JenP.Visible = true;
                Plarga.Visible = true;
                JOnline.Visible = true;
                Aceptar.Visible = false;
                Cancelar.Visible = false;
                Usuario.Visible = false;
                label2.Visible = false;
                textUsuario.Visible = false;
                Contraseña.Visible = false;
            }
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

        private void Aceptar2_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                if (JenP.Checked)
                {
                    string mensaje = "2/" + JugadorBox.Text + "/" + PartidaBox.Text;
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
                if (Plarga.Checked)
                {
                    string mensaje = "3/" + PartidaBox.Text;
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
                if (JOnline.Checked)
                {
                    string mensaje = "4/"+ JugadorBox.Text;
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
            else
            {
                MessageBox.Show("No estas conectado");
            }
        }

        private void Cancelar2_Click(object sender, EventArgs e)
        {
            string mensaje = "0/";

            if (server != null)
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            Aceptar2.Visible = false;
            Cancelar2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            PartidaBox.Visible = false;
            JugadorBox.Visible = false;
            JenP.Visible = false;
            Plarga.Visible = false;
            JOnline.Visible = false;
            Aceptar.Visible = true;
            Cancelar.Visible = true;
            Usuario.Visible = true;
            label2.Visible = true;
            textUsuario.Visible = true;
            Contraseña.Visible = true;
        }
    }
}
