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

namespace DAYANG_v1
{
    public partial class Tablero : Form
    {
        public Jugador jugador;
        public int id;
        public string usuario;

        Socket server;

        Celda CeldaSeleccionada;
        string AtaqueSeleccionado;

        public Jugador jugador2 = new Jugador(2, "jose2");


        static Partida partida = new Partida(15);
        public Button[,] btnGrid = new Button[partida.myTablero.dimension, partida.myTablero.dimension];

        public List<Panel> paneles;
        public List<PictureBox> pictureboxes;
        public List<Label> NombresLBLs;
        public List<Label> AtaqueLBLs;
        public List<Label> MovLBLs;
        public List<Label> PuntosLBLs;
        public int timerCount;
        public int CountD;

        public Tablero(List<Jugador> ListJug, Socket serv, string u)
        {
            InitializeComponent();
            this.server = serv;
            this.usuario = u;
            paneles = new List<Panel>() { panel_comando_jugador, panel_comando_jugador2, panel_comando_jugador3, panel_comando_jugador4};
            pictureboxes = new List<PictureBox>() { JugadorPBox, Jugador2PBox, Jugador3PBox, Jugador4PBox };
            NombresLBLs = new List<Label>() { NombreJugadorLBL, NombreJugadorLBL2, NombreJugadorLBL3, NombreJugadorLBL4 };
            AtaqueLBLs = new List<Label>() { AtaqueLBL1, AtaqueLBL2, AtaqueLBL3, AtaqueLBL4 };
            MovLBLs = new List<Label>() { MovimientoLBL1, MovimientoLBL2, MovimientoLBL3, MovimientoLBL4 };
            PuntosLBLs = new List<Label>() { PuntosLBL1, PuntosLBL2, PuntosLBL3, PuntosLBL4 };

            panel_comando_jugador2.Visible = false;
            panel_comando_jugador3.Visible = false;
            panel_comando_jugador4.Visible = false;

            timer.Interval = 1000;
            timer.Start();
            timerCount = 0;
            CountD = 15;
            CountDownLBL.Text = CountD.ToString();

            partida.ListaJugadores = ListJug;
            JugadorPBox.BackgroundImageLayout = ImageLayout.Stretch;
            populateGrid();
            AtaqueSeleccionado = ArribaRadioBut.Text;
            ArribaRadioBut.Checked = true;
            partida.myTablero.ColocarBOSS();
        }

        private void pintartableroMovimientos()
        {
            for (int i = 0; i < partida.myTablero.dimension; i++)
            {
                for (int j = 0; j < partida.myTablero.dimension; j++)
                {
                    btnGrid[i, j].Text = "";
                    btnGrid[i, j].BackColor = Color.Transparent;
                    btnGrid[i, j].BackgroundImage = null;

                    if (partida.myTablero.matriz_celdas[i, j].EstaOcupada == true)
                    {
                        btnGrid[i, j].BackgroundImage = jugador.ImagenPersonajeCompleta;
                        btnGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    }

                    else if (partida.myTablero.matriz_celdas[i, j].MovimientoLegal == true)
                    {
                        btnGrid[i, j].BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void pintartableroAtaques()
        {
            for (int i = 0; i < partida.myTablero.dimension; i++)
            {
                for (int j = 0; j < partida.myTablero.dimension; j++)
                {
                    btnGrid[i, j].Text = "";
                    btnGrid[i, j].BackColor = Color.Transparent;
                    btnGrid[i, j].BackgroundImage = null;



                    if (partida.myTablero.matriz_celdas[i, j].EstaOcupada == true)
                    {
                        btnGrid[i, j].BackgroundImage = jugador.ImagenPersonajeCompleta;
                        btnGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        //btnGrid[i, j].Text = "Caballero";
                    }

                    else if (partida.myTablero.matriz_celdas[i, j].Ataque == true)
                    {
                        //btnGrid[i, j].Text = "Legal";
                        btnGrid[i, j].BackColor = Color.Red;
                    }
                }
            }
        }

        private void populateGrid()
        {
            int buttonSize = panel_tablero.Width / partida.myTablero.dimension;
            panel_tablero.Height = panel_tablero.Width;

            for (int i = 0; i < partida.myTablero.dimension; i++)
            {
                for (int j = 0; j < partida.myTablero.dimension; j++)
                {
                    btnGrid[i, j] = new Button();

                    btnGrid[i, j].Height = buttonSize;
                    btnGrid[i, j].Width = buttonSize;

                    btnGrid[i, j].Click += Grid_Button_Click;

                    panel_tablero.Controls.Add(btnGrid[i, j]);

                    btnGrid[i, j].Location = new Point(i * buttonSize, j * buttonSize);

                    btnGrid[i, j].FlatStyle = FlatStyle.Flat;
                    btnGrid[i, j].FlatAppearance.BorderSize = 0;
                    btnGrid[i, j].FlatAppearance.BorderColor = Color.Black;
                    btnGrid[i, j].FlatAppearance.MouseOverBackColor = Color.Red;
                    btnGrid[i, j].FlatAppearance.MouseDownBackColor = Color.Green;
                    btnGrid[i, j].Font = new Font(Font, FontStyle.Bold);

                    btnGrid[i, j].Text = i + "|" + j;
                    btnGrid[i, j].Tag = new Point(i, j);
                }
            }
        }

        private void Grid_Button_Click(object sender, EventArgs e)
        {
            Button botonClicado = (Button) sender;
            Point location = (Point) botonClicado.Tag;

            int x = location.X;
            int y = location.Y;

            Celda CeldaProvisional = partida.myTablero.matriz_celdas[x, y];

            if (CeldaProvisional.MovimientoLegal)
            {
                jugador.celdaprovisional = CeldaProvisional;
                partida.myTablero.MarcarAtaques(jugador, AtaqueSeleccionado);
                pintartableroAtaques();
                PintarJugadores();
                btnGrid[jugador.celdaactual.posicion.row, jugador.celdaactual.posicion.column].BackgroundImage = null;
            }

            else
            {
                MessageBox.Show("Movimiento no permitido");
            }
        }

        private void Tablero_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < partida.ListaJugadores.Count; i++)
            {
                partida.myTablero.ColocarPsicionInicial(partida.ListaJugadores[i]);
            }
            //jugador = partida.ListaJugadores[0];

            jugador = partida.ListaJugadores[BuscarPosicion(usuario)];

            partida.myTablero.MarcarSiguienteMovimientoLegal(jugador);

            pintartableroMovimientos();

            PintarJugadores();

            ColocarPaneles();
        }

        private void Confirmar_Click(object sender, EventArgs e)
        {
            jugador.celdaactual = jugador.celdaprovisional;
            partida.myTablero.MarcarSiguienteMovimientoLegal(jugador);
            pintartableroMovimientos();
            CeldaSeleccionada = jugador.celdaactual;
            PintarJugadores();
        }

        private void CambiarMovimientoBut_Click(object sender, EventArgs e)
        {
            jugador.celdaprovisional = jugador.celdaactual;
            partida.myTablero.MarcarSiguienteMovimientoLegal(jugador);
            pintartableroMovimientos();
            PintarJugadores();
        }

        private void RadioBut_CheckedChange(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton) sender;

            if (rb.Text != AtaqueSeleccionado)
            {
                AtaqueSeleccionado = rb.Text;
                partida.myTablero.MarcarAtaques(jugador, AtaqueSeleccionado);
                pintartableroAtaques();
                PintarJugadores();
                btnGrid[jugador.celdaactual.posicion.row, jugador.celdaactual.posicion.column].BackgroundImage = null;
            }
        }

        private void ColocarVidas()
        {
            for (int j = 0; j < partida.ListaJugadores.Count; j++)
            {
                int x = 117;
                int y = 45;

                for (int i = 0; i < partida.ListaJugadores[j].Puntos_Vida; i++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = new Size(35, 35);

                    paneles[j].Controls.Add(pb);

                    pb.Location = new Point(x, y);
                    pb.BackgroundImage = new Bitmap(@"Imagenes\corazon.png");
                    pb.BackgroundImageLayout = ImageLayout.Stretch;

                    x = x + 35;
                }
            }           
        }

        private void ColocarPuntos()
        {
            for (int i = 0; i < partida.ListaJugadores.Count; i++)
            {
                PuntosLBLs[i].Text = partida.ListaJugadores[i].puntos.ToString();
            }
        }

        private void ColocarPaneles()
        {
            for (int i = 0; i < partida.ListaJugadores.Count; i++)
            {
                paneles[i].Visible = true;
                NombresLBLs[i].Text = partida.ListaJugadores[i].nombreJugador;
                AtaqueLBLs[i].Text = partida.ListaJugadores[i].Danyo.ToString();
                MovLBLs[i].Text = partida.ListaJugadores[i].CasillasMovimiento.ToString();
                
                pictureboxes[i].BackgroundImage = partida.ListaJugadores[i].ImagenPersonajeCompleta;
                pictureboxes[i].BackgroundImageLayout = ImageLayout.Stretch;
            }
            ColocarVidas();
            ColocarPuntos();
        }

        private void PintarJugadores()
        {
            for (int i = 0; i < partida.ListaJugadores.Count; i++)
            {
                btnGrid[partida.ListaJugadores[i].celdaactual.posicion.row, partida.ListaJugadores[i].celdaactual.posicion.column].BackgroundImage = partida.ListaJugadores[i].ImagenPersonajeCompleta;
                btnGrid[partida.ListaJugadores[i].celdaactual.posicion.row, partida.ListaJugadores[i].celdaactual.posicion.column].BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        public int BuscarPosicion(string nombre)
        {
            int num = 0;
            for (int i = 0; i < partida.ListaJugadores.Count; i++)
            {
                if (partida.ListaJugadores[i].nombreJugador == nombre)
                {
                    num = i;
                }
            }
            return num;
        }

        public void RecibirMensaje(string[] mensaje)
        {
            //trozear mensaje y asignar cada posicion provisional y ataque a cada jugador.
            partida.ListaJugadores[BuscarPosicion(mensaje[0])].celdaprovisional.posicion.row = Convert.ToInt32(mensaje[1]);
            partida.ListaJugadores[BuscarPosicion(mensaje[0])].celdaprovisional.posicion.column = Convert.ToInt32(mensaje[2]);


            if (partida.myTablero.matriz_celdas[Convert.ToInt32(mensaje[1]), Convert.ToInt32(mensaje[2])].EstaOcupada)
            {
                partida.ListaJugadores[BuscarPosicion(mensaje[0])].celdaprovisional = partida.ListaJugadores[BuscarPosicion(mensaje[0])].celdaactual;
            }
            else
            {
                for (int i = 0; i < partida.ListaJugadores.Count; i++)
                {
                    partida.ListaJugadores[i].celdaactual = partida.ListaJugadores[i].celdaprovisional;
                }
                partida.myTablero.matriz_celdas[Convert.ToInt32(mensaje[1]), Convert.ToInt32(mensaje[2])].EstaOcupada = true;
                partida.myTablero.MarcarSiguienteMovimientoLegal(jugador);
                pintartableroMovimientos();
                CeldaSeleccionada = jugador.celdaactual;
                PintarJugadores();
            }
        }

        public bool HayConflicto()
        {
            bool hayconflicto = false;

            for (int i = 0; i < partida.ListaJugadores.Count; i++)
            {
                for (int j = 0; j < partida.ListaJugadores.Count; j++)
                {
                    if (partida.ListaJugadores[i].celdaactual.posicion.row == partida.ListaJugadores[j].celdaactual.posicion.row && partida.ListaJugadores[i].celdaactual.posicion.column == partida.ListaJugadores[j].celdaactual.posicion.column)
                    {
                        hayconflicto = true;
                    }
                }
            }
            return hayconflicto;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timerCount++;
            CountD--;
            CountDownLBL.Text = CountD.ToString();
            if (timerCount % 15 == 0)
            {
                CeldaSeleccionada = jugador.celdaprovisional;
                string mensaje = "10/"+ partida + "/3/" + jugador.nombreJugador + "/" + CeldaSeleccionada.posicion.row.ToString() + "-" + CeldaSeleccionada.posicion.column.ToString() + "-" + AtaqueSeleccionado;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibir mensaje del server con todos los movimientos de los participantes en el menu y meterlo aqui.
                CountD = 15;
            }
        }
    }
}
