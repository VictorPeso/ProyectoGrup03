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
using System.Media;
using MisClases;

namespace DAYANG_v1
{
    public partial class IniciarSesion : Form
    {

        public bool TheresName = false;
        public bool TheresContra = false;
        public bool connected = false;

        Socket server;

        public IniciarSesion()
        {
            InitializeComponent();
            ConfigurarMenu();
        }

        private void ConfigurarMenu()
        {
            NameTextBox.ForeColor = Color.Gray;
            ContraTextBox.ForeColor = Color.Gray;
            NameTextBox.Text = "Nombre de usuario";
            ContraTextBox.Text = "Contraseña";
            ContraTextBox.PasswordChar = '\0';
            //SoundPlayer sound = new SoundPlayer(@"Sonido\prueba.wav");
            //sound.Play();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            if (!TheresName || !TheresContra)
            {
                MessageBox.Show("Falta nombre o contraseña");
            }
            else
            {
                ConectarSocket();

                string mensaje = "1/" + NameTextBox.Text + "/" + ContraTextBox.Text;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] mensajes = mensaje.Split('/');

                if (mensajes[0] == "Correcto")
                {
                    MessageBox.Show("Conectado.");
                    this.Hide();
                    Menu menu = new Menu(server, NameTextBox.Text);
                    menu.ShowDialog();
                    //SeleccionCampeon selchamp = new SeleccionCampeon();
                    //selchamp.ShowDialog();
                    //string Champselected = selchamp.GetCampeonSeleccionado();
                    //Tablero tablero = new Tablero();
                    //tablero.SetCampeon(Champselected);
                    //tablero.ShowDialog();
                    ConfigurarMenu();
                    this.Show();
                }
                else
                {
                    string mensaje2 = "0/" + NameTextBox.Text;

                    byte[] msg3 = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                    server.Send(msg3);

                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                    MessageBox.Show("Contraseña o usuario incorrecto, intentelo de nuevo.");
                }
            }
        }

        private void regbutton_Click(object sender, EventArgs e)
        {
            ConectarSocket();

            string mensaje = "9/" + NameTextBox.Text + "/" + ContraTextBox.Text;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            string[] mensajes = mensaje.Split('/');

            if (mensajes[0] == "Correcto")
            {
                string mensaje2 = "0/" + NameTextBox.Text;

                byte[] msg3 = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                server.Send(msg3);

                server.Shutdown(SocketShutdown.Both);
                server.Close();
                MessageBox.Show("Usuario creado con exito");
            }
            else
            {
                string mensaje2 = "0/" + NameTextBox.Text;

                byte[] msg3 = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                server.Send(msg3);

                server.Shutdown(SocketShutdown.Both);
                server.Close();
                MessageBox.Show("No se ha podido crear usuario");
            }
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NameTextBox_Enter(object sender, EventArgs e)
        {
            if (NameTextBox.Text == "Nombre de usuario")
            {
                NameTextBox.Text = "";
                NameTextBox.ForeColor = Color.White;
                TheresName = true;
            }
        }

        private void NameTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                NameTextBox.ForeColor = Color.Gray;
                NameTextBox.Text = "Nombre de usuario";
                TheresName = false;
            }
        }

        private void ContraTextBox_Enter(object sender, EventArgs e)
        {
            if (ContraTextBox.Text == "Contraseña")
            {
                ContraTextBox.Text = "";
                ContraTextBox.PasswordChar = '*';
                ContraTextBox.ForeColor = Color.White;
                TheresContra = true;
            }
        }

        private void ContraTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ContraTextBox.Text))
            {
                ContraTextBox.ForeColor = Color.Gray;
                ContraTextBox.PasswordChar = '\0';
                ContraTextBox.Text = "Contraseña";
                TheresContra = false;
            }
        }

        private void ConectarSocket()
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPEndPoint ipep = new IPEndPoint(direc, 50056);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("No se ha podido conectar con el servidor");
                return;
            }
        }
    }
}
