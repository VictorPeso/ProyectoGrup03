using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MisClases;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Media;

namespace DAYANG_v1
{
    public partial class SeleccionCampeon : Form
    {
        public string CampeonSeleccionado;
        public Button BotonSeleccionado;
        int timerCount;
        SoundPlayer sound;
        Socket server;
        string usuario;
        int partida;
        int numJugadores;

        public SeleccionCampeon(Socket serv, int partida, string usuario, int num)
        {
            InitializeComponent();
            this.server = serv;
            this.partida = partida;
            this.usuario = usuario;
            this.numJugadores = num;
            timerChampSelect.Interval = 1000;
            timerChampSelect.Start();
            timerCount = 0;
            CampeonSeleccionado = "0";
            BotonSeleccionado = Confirmar;
            Confirmar.Visible = false;
            EspadaPB.Visible = false;
            MovimientoPB.Visible = false;
            VidaPB.Visible = false;
            nombrelbl.Text = "";
            Textolbl.Text = "";
            Generolbl.Text = "";
            AtaqueLBL.Text = "";
            MovimientoLBL.Text = "";
            VidaLBL.Text = "";
        }

        public string GetCampeonSeleccionado()
        {
            return CampeonSeleccionado;
        }

        private void button_Champ1_Click(object sender, EventArgs e)
        {
            CampeonSeleccionado = "1";
            Confirmar.Visible = true;
            EspadaPB.Visible = true;
            MovimientoPB.Visible = true;
            VidaPB.Visible = true;
            nombrelbl.Text = "Benito";
            Generolbl.Text = "Reggaeton";
            Textolbl.Text = "Un músico que es igual de bueno en el ataque que en la defensa";
            AtaqueLBL.Text = "1";
            MovimientoLBL.Text = "4";
            VidaLBL.Text = "4";
            BotonSeleccionado.BackColor = Color.Gainsboro;
            button_Champ1.BackColor = Color.DarkGray;
            BotonSeleccionado = button_Champ1;
            sound = new SoundPlayer(@"Sonido\Reggaeton.wav");
            sound.Play();
        }

        private void button_Champ2_Click(object sender, EventArgs e)
        {
            CampeonSeleccionado = "2";
            Confirmar.Visible = true;
            EspadaPB.Visible = true;
            MovimientoPB.Visible = true;
            VidaPB.Visible = true;
            nombrelbl.Text = "Roxy";
            Generolbl.Text = "Heavy Metal";
            Textolbl.Text = "La mejor defensa es un buen ataque ¡Mata a tus enemigos antes de que lo hagan ellos!";
            AtaqueLBL.Text = "2";
            MovimientoLBL.Text = "5";
            VidaLBL.Text = "3";
            BotonSeleccionado.BackColor = Color.Gainsboro;
            button_Champ2.BackColor = Color.DarkGray;
            BotonSeleccionado = button_Champ2;
            sound = new SoundPlayer(@"Sonido\Heavy.wav");
            sound.Play();
        }

        private void button_Champ3_Click(object sender, EventArgs e)
        {
            CampeonSeleccionado = "3";
            Confirmar.Visible = true;
            EspadaPB.Visible = true;
            MovimientoPB.Visible = true;
            VidaPB.Visible = true;
            nombrelbl.Text = "Javier";
            Generolbl.Text = "Rumba";
            Textolbl.Text = "Un caballero que va al frente para defender sus ideales,lo importante es ser firme.";
            AtaqueLBL.Text = "1";
            MovimientoLBL.Text = "4";
            VidaLBL.Text = "4";
            BotonSeleccionado.BackColor = Color.Gainsboro;
            button_Champ3.BackColor = Color.DarkGray;
            BotonSeleccionado = button_Champ3;
            sound = new SoundPlayer(@"Sonido\Flamenco.wav");
            sound.Play();
        }

        private void button_Champ4_Click(object sender, EventArgs e)
        {
            CampeonSeleccionado = "4";
            Confirmar.Visible = true;
            EspadaPB.Visible = true;
            MovimientoPB.Visible = true;
            VidaPB.Visible = true;
            nombrelbl.Text = "Hiyori";
            Generolbl.Text = "Otaku";
            Textolbl.Text = "¿Acercarse a los enemigos? ¿Por qué? Puedes matarlos desde lejos";
            AtaqueLBL.Text = "1";
            MovimientoLBL.Text = "4";
            VidaLBL.Text = "3";
            BotonSeleccionado.BackColor = Color.Gainsboro;
            button_Champ4.BackColor = Color.DarkGray;
            BotonSeleccionado = button_Champ4;
            sound = new SoundPlayer(@"Sonido\Otaku.wav");
            sound.Play();
        }

        private void button_Champ5_Click(object sender, EventArgs e)
        {
            CampeonSeleccionado = "5";
            Confirmar.Visible = true;
            EspadaPB.Visible = true;
            MovimientoPB.Visible = true;
            VidaPB.Visible = true;
            nombrelbl.Text = "Ludwig";
            Generolbl.Text = "Música Clásica";
            Textolbl.Text = "Un clasico lo es por que es bueno, sigue siendo igual de efectivo.";
            AtaqueLBL.Text = "1";
            MovimientoLBL.Text = "4";
            VidaLBL.Text = "4";
            BotonSeleccionado.BackColor = Color.Gainsboro;
            button_Champ5.BackColor = Color.DarkGray;
            BotonSeleccionado = button_Champ5;
            sound = new SoundPlayer(@"Sonido\Clasica.wav");
            sound.Play();
        }

        private void button_Champ6_Click(object sender, EventArgs e)
        {
            CampeonSeleccionado = "6";
            Confirmar.Visible = true;
            EspadaPB.Visible = true;
            MovimientoPB.Visible = true;
            VidaPB.Visible = true;
            nombrelbl.Text = "Takumi";
            Generolbl.Text = "Eurobeat";
            Textolbl.Text = "Driftea contra tus enemigos con una velocidad vertiginosa";
            AtaqueLBL.Text = "2";
            MovimientoLBL.Text = "6";
            VidaLBL.Text = "4";
            BotonSeleccionado.BackColor = Color.Gainsboro;
            button_Champ6.BackColor = Color.DarkGray;
            BotonSeleccionado = button_Champ6;
            sound = new SoundPlayer(@"Sonido\Eurobeat.wav");
            sound.Play();
        }

        private void button_Champ7_Click(object sender, EventArgs e)
        {
            CampeonSeleccionado = "7";
            Confirmar.Visible = true;
            EspadaPB.Visible = true;
            MovimientoPB.Visible = true;
            VidaPB.Visible = true;
            nombrelbl.Text = "Diana";
            Generolbl.Text = "Emo";
            Textolbl.Text = "Nadie te entiende ... demuéstraselo";
            AtaqueLBL.Text = "3";
            MovimientoLBL.Text = "5";
            VidaLBL.Text = "3";
            BotonSeleccionado.BackColor = Color.Gainsboro;
            button_Champ7.BackColor = Color.DarkGray;
            BotonSeleccionado = button_Champ7;
            sound = new SoundPlayer(@"Sonido\Emo.wav");
            sound.Play();
        }

        private void button_Champ8_Click(object sender, EventArgs e)
        {
            CampeonSeleccionado = "8";
            Confirmar.Visible = true;
            EspadaPB.Visible = true;
            MovimientoPB.Visible = true;
            VidaPB.Visible = true;
            nombrelbl.Text = "Bruno";
            Generolbl.Text = "Phonk";
            Textolbl.Text = "Trabaja mas duro...Aguanta mas daño...Nada puede tocarte...";
            AtaqueLBL.Text = "2";
            MovimientoLBL.Text = "3";
            VidaLBL.Text = "6";
            BotonSeleccionado.BackColor = Color.Gainsboro;
            button_Champ8.BackColor = Color.DarkGray;
            BotonSeleccionado = button_Champ8;
            sound = new SoundPlayer(@"Sonido\Phonk.wav");
            sound.Play();
        }

        private void Confirmar_Click(object sender, EventArgs e)
        {
            ////this.Hide();
            //Jugador jugador1 = new Jugador(1, "Victor");
            //jugador1.SeleccionarCampeon("2");
            //Jugador jugador2 = new Jugador(2, "Pedro");
            //jugador2.SeleccionarCampeon("4");
            //Jugador jugador3 = new Jugador(3, "Jose");
            //jugador3.SeleccionarCampeon("5");
            //Jugador jugador4 = new Jugador(4, "Azahara");
            //jugador4.SeleccionarCampeon("7");

            //List<Jugador> jugadores = new List<Jugador>() { jugador1, jugador2, jugador3, jugador4 };

            //Tablero tablero = new Tablero(jugadores);
            //tablero.ShowDialog();
            //this.Show();

            timerChampSelect.Stop();
            sound.Stop();

            string mensajeX = "10/" + partida.ToString() + "/2/" + usuario + "-" + CampeonSeleccionado;

            byte[] msgx = System.Text.Encoding.ASCII.GetBytes(mensajeX);
            server.Send(msgx);
            this.Close();
            //Tablero tablero = new Tablero(JugadoresEnPartida, server);
            //tablero.ShowDialog();
        }

        private void timerChampSelect_Tick(object sender, EventArgs e)
        {
            if (timerCount == 3000)
            {
                Random rnd = new Random();
                CampeonSeleccionado = rnd.Next(1, 9).ToString();
                timerChampSelect.Stop();
                sound.Stop();
                string mensajeX = "10/" + partida.ToString() + "/2/" + usuario + "-" + CampeonSeleccionado;

                byte[] msgx = System.Text.Encoding.ASCII.GetBytes(mensajeX);
                server.Send(msgx);
                this.Close();
            }

            timerCount++;
        }

        private void SeleccionCampeon_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CampeonSeleccionado == "0")
            {
                Random rnd = new Random();
                CampeonSeleccionado = rnd.Next(1, 9).ToString();
                string mensajeX = "10/" + partida.ToString() + "/2/" + usuario + "-" + CampeonSeleccionado;

                byte[] msgx = System.Text.Encoding.ASCII.GetBytes(mensajeX);
                server.Send(msgx);
                this.Close();
            }
            timerChampSelect.Stop();
            sound.Stop();
        }
    }
}
