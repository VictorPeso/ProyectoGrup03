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
using MisClases;

namespace DAYANG_v1
{
    public partial class Menu : Form
    {
        Socket server;
        string Usuario;
        Thread atender;

        string nombredestino;
        string nom;
        int partida;
        int rows;
        int creado = 0;
        string jugadores;
        List<int> partidas = new List<int>();
        List<Jugador> JugadoresEnPartida = new List<Jugador>();
        List<string> JugadoresParaNuevaPartida = new List<string>();
        List<string> JugadoresParaNuevaPartidaConfirmados = new List<string>();
        bool partidacreada = false;

        delegate void DelegadoParaActualizarLista(string frase, int rows);
        delegate void DelegadoParaPonerenLista(string[] mensajes);

        public Menu(Socket s, string nombre)
        {
            InitializeComponent();
            this.server = s;
            this.Usuario = nombre;

            CheckForIllegalCrossThreadCalls = false;
            // Necesario para que los elementos de los formularios puedan ser
            // accedidos desde threads diferentes a los que los crearon.
            listaUsuarios.ColumnHeadersVisible = false;
            listaUsuarios.RowHeadersVisible = false;
            listaUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listaUsuarios.ReadOnly = true;
            //listaUsuarios.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datachat.ColumnHeadersVisible = false;
            datachat.RowHeadersVisible = false;
            datachat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datachat.ReadOnly = true;

            dataGridIntegrantesPartida.ColumnHeadersVisible = false;
            dataGridIntegrantesPartida.RowHeadersVisible = false;
            dataGridIntegrantesPartida.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridIntegrantesPartida.ReadOnly = true;

            HistorialGridView1.ColumnHeadersVisible = false;
            HistorialGridView1.RowHeadersVisible = false;
            //HistorialGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            HistorialGridView1.ReadOnly = true;
        }

        private void CreaGrid(string[] mensaje)
        {
            listaUsuarios.Rows.Clear();
            listaUsuarios.ColumnCount = 1;
            listaUsuarios.RowCount = mensaje.Length - 1;
            int i = 1;
            while (i < mensaje.Length)
            {
                string u = mensaje[i];
                //listaUsuarios[0, i - 1].Value = u;
                listaUsuarios.Rows[i - 1].Cells[0].Value = u;
                i++;
            }

        }

        private void CreaGridEtiqueta3(string[] mensaje)
        {
            int k = 1;
            int filas = (mensaje.Length - 1) / 7;
            HistorialGridView1.Rows.Clear();
            HistorialGridView1.ColumnCount = 8;
            HistorialGridView1.RowCount = filas + 1;
            int i = 0;
            while (i < filas)
            {
                string partida = "Partida " + mensaje[k];
                HistorialGridView1.Rows[i].Cells[0].Value = partida;
                HistorialGridView1.Rows[i].Cells[1].Value = mensaje[k + 1];
                HistorialGridView1.Rows[i].Cells[2].Value = mensaje[k + 2];
                HistorialGridView1.Rows[i].Cells[3].Value = mensaje[k + 3];
                HistorialGridView1.Rows[i].Cells[4].Value = mensaje[k + 4];
                HistorialGridView1.Rows[i].Cells[5].Value = mensaje[k + 5];
                HistorialGridView1.Rows[i].Cells[6].Value = mensaje[k + 6];
                HistorialGridView1.Rows[i].Cells[7].Value = mensaje[k + 7];
                i++;
                k = k + 8;
            }

        }

        private void ActualizarJugadoresEnPartida(List<string> lista)
        {
            dataGridIntegrantesPartida.Rows.Clear();
            dataGridIntegrantesPartida.ColumnCount = 1;
            if (lista.Count != 0)
            {
                dataGridIntegrantesPartida.RowCount = lista.Count;
                int i = 0;
                while (i < lista.Count)
                {
                    dataGridIntegrantesPartida.Rows[i].Cells[0].Value = lista[i];
                    i++;
                }
            }
            else
            {
                dataGridIntegrantesPartida.RowCount = 1;
                dataGridIntegrantesPartida.Rows[0].Cells[0].Value = "";
            }
        }

        private void ActualizaGrid(string frase, int rows)
        {
            int n = rows;
            if (n < 9)
            {
                datachat.RowCount = 8;
                datachat.Rows[n - 1].Cells[0].Value = frase;
            }
            else
            {
                datachat.RowCount = 8;
                datachat.Rows[0].Cells[0].Value = datachat.Rows[1].Cells[0].Value;
                datachat.Rows[1].Cells[0].Value = datachat.Rows[2].Cells[0].Value;
                datachat.Rows[2].Cells[0].Value = datachat.Rows[3].Cells[0].Value;
                datachat.Rows[3].Cells[0].Value = datachat.Rows[4].Cells[0].Value;
                datachat.Rows[4].Cells[0].Value = datachat.Rows[5].Cells[0].Value;
                datachat.Rows[5].Cells[0].Value = datachat.Rows[6].Cells[0].Value;
                datachat.Rows[6].Cells[0].Value = datachat.Rows[7].Cells[0].Value;
                datachat.Rows[7].Cells[0].Value = frase;
            }
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
                        listaUsuarios.Invoke(new DelegadoParaPonerenLista(CreaGrid), new object[] { mensaje });
                        break;
                    case 6:
                        nom = mensaje[1];
                        DialogResult r = MessageBox.Show(mensaje[1] + " quiere que te unas a su partida", "Notificacion", MessageBoxButtons.YesNo);
                        partida = Convert.ToInt32(mensaje[2]);
                        MessageBox.Show(Convert.ToString(partida));
                        //MessageBox.Show(Convert.ToString(r));
                        string respuesta = "6/" + mensaje[1] + "/" + Convert.ToString(r) + "/" + Convert.ToString(partida);
                        if (Convert.ToString(r) == "Yes")
                        {
                            Partida.Invoke(new Action(() =>
                            {
                                Partida.Text = "En partida con " + nom;
                            }));
                        }
                        // Enviamos al servidor el nombre tecleado
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuesta);
                        server.Send(msg);

                        break;
                    case 7:
                        nom = mensaje[1];
                        if (mensaje[2] == "SI")
                        {
                            MessageBox.Show(nom + " acepta jugar contigo");
                            //JugadoresParaNuevaPartidaConfirmados.Add(nom);
                            Partida.Invoke(new Action(() =>
                            {
                                Partida.Text = "En partida con " + nom;
                            }));
                            partida = Convert.ToInt32(mensaje[3]);
                            MessageBox.Show(Convert.ToString(partida));
                        }
                        else
                        {
                            MessageBox.Show(nom + " no acepta jugar contigo");

                        }
                        break;
                    case 8:
                        nom = mensaje[1];
                        Chat.Invoke(new Action(() =>
                        {
                            Chat.Text = nom + ": " + mensaje[2];
                        }));
                        string frase = nom + ": " + mensaje[2];
                        rows = rows + 1;
                        datachat.Invoke(new DelegadoParaActualizarLista(ActualizaGrid), new object[] { frase, rows });
                        break;

                    case 9:
                        string mensaje9 = "0/" + Usuario;
                        if (server != null)
                        {
                            byte[] msg9 = System.Text.Encoding.ASCII.GetBytes(mensaje9);
                            server.Send(msg9);

                            atender.Abort();
                            server.Shutdown(SocketShutdown.Both);
                            server.Close();
                        }
                        this.Close();
                        break;

                    case 10:
                        string[] jugadores = mensaje[2].Split('-');
                        JugadoresParaNuevaPartidaConfirmados.Clear();
                        for(int i = 0; i < jugadores.Length - 1; i++)
                        {
                            JugadoresParaNuevaPartidaConfirmados.Add(jugadores[i]);
                        }
                        for (int i = 0; i < JugadoresParaNuevaPartidaConfirmados.Count; i++)
                        {
                            Jugador jug = new Jugador(i + 1, JugadoresParaNuevaPartidaConfirmados[i]);
                            JugadoresEnPartida.Add(jug);
                        }
                        SeleccionCampeon selchamp = new SeleccionCampeon(server, partida, Usuario, JugadoresParaNuevaPartidaConfirmados.Count);
                        selchamp.ShowDialog();
                        break;

                    case 11:
                        creado++;
                        string[] campeon = mensaje[2].Split('-');
                        for (int i = 0; i < JugadoresEnPartida.Count; i++)
                        {
                            if (JugadoresEnPartida[i].nombreJugador == campeon[0])
                            {
                                JugadoresEnPartida[i].SeleccionarCampeon(campeon[1]);
                            }
                        }

                        if (creado == JugadoresEnPartida.Count)
                        {
                            //Tablero tablero = new Tablero(JugadoresEnPartida, server, Usuario);
                            //tablero.ShowDialog();
                            string mensaje11 = "12/" + partida.ToString();
                            for (int i = 0; i < JugadoresEnPartida.Count; i++)
                            {
                                if (JugadoresEnPartida[i].nombreJugador == campeon[0])
                                {
                                    JugadoresEnPartida[i].SeleccionarCampeon(campeon[1]);
                                }
                            }
                            // Enviamos al servidor el nombre tecleado
                            byte[] msg11 = System.Text.Encoding.ASCII.GetBytes(mensaje11);
                            server.Send(msg11);
                        }
                        break;

                    case 12:
                        string[] movimiento = mensaje[2].Split('-');
                        break;

                    case 13:
                        //for (int i = 0; i < JugadoresEnPartida.Count; i++)
                        //{
                        //    if (JugadoresEnPartida[i].nombreJugador == campeon[0])
                        //    {
                        //        JugadoresEnPartida[i].SeleccionarCampeon(campeon[1]);
                        //    }
                        //}
                        Tablero tablero = new Tablero(JugadoresEnPartida, server, Usuario);
                        tablero.ShowDialog();
                        break;

                    case 20:
                        MessageBox.Show(mensaje[2] + " ha jugado la partida");
                        HistorialGridView1.Invoke(new DelegadoParaPonerenLista(CreaGridEtiqueta3), new object[] { mensaje });
                        break;
                }
            }
        }

        private void PonerEnMarchaTablero()
        {
            MessageBox.Show(JugadoresEnPartida.Count.ToString());
            Tablero tablero = new Tablero(JugadoresEnPartida, server, Usuario);
            tablero.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
            Usuariolb.Text = "Usuario: " + Usuario;
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

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
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

        private void listaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nombredestino = listaUsuarios.Rows[e.RowIndex].Cells[0].Value.ToString();
            bool found = false;
            for (int i = 0; i < JugadoresParaNuevaPartida.Count; i++)
            {
                if (nombredestino == JugadoresParaNuevaPartida[i])
                {
                    found = true;
                }

            }

            if (partidacreada && nombredestino != Usuario)
            {
                if (found)
                {
                    MessageBox.Show("Este usuario ya esta en la partida.");
                }
                else if (JugadoresParaNuevaPartida.Count == 4)
                {
                    MessageBox.Show("Maximo de jugadores alcanzado");
                }
                else
                {
                    JugadoresParaNuevaPartida.Add(nombredestino);
                    ActualizarJugadoresEnPartida(JugadoresParaNuevaPartida);
                }
            }
            else
            {
                MessageBox.Show("Crea primero una partida antes de invitar a otros jugadores.");
            }
        }

        private void Enviar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(texto.Text))
            {
                string mensaje = "7/" + Convert.ToString(partida) + "/" + texto.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            texto.Text = "";
        }

        private void texto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(texto.Text))
                {
                    string mensaje = "7/" + Convert.ToString(partida) + "/" + texto.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                texto.Text = "";
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Seguro que quieres eliminar este usuario?", "Notificacion", MessageBoxButtons.YesNo);
            //MessageBox.Show(Convert.ToString(r));
            if (Convert.ToString(r) == "Yes")
            {
                string mensaje = "8/" + Usuario;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                this.Close();
            }
        }

        private void CrearPartida_Click(object sender, EventArgs e)
        {
            int num = JugadoresParaNuevaPartida.Count - 1;
            string mensaje = "5/" + num;

            for (int i = 1; i < JugadoresParaNuevaPartida.Count; i++)
            {
                mensaje = mensaje + "/" + JugadoresParaNuevaPartida[i];
            }

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void BorrarP_Click(object sender, EventArgs e)
        {
            JugadoresParaNuevaPartida.Clear();
            JugadoresParaNuevaPartida.Add(Usuario);
            ActualizarJugadoresEnPartida(JugadoresParaNuevaPartida);
        }

        private void EmpezarPartida_Click(object sender, EventArgs e)
        {
            //JugadoresParaNuevaPartida.Add("Pedro");
            //JugadoresParaNuevaPartida.Add("Aza");
            //JugadoresParaNuevaPartida.Add("Juan");
            //MessageBox.Show(JugadoresParaNuevaPartida.Count.ToString());

            for (int i = 0; i < JugadoresParaNuevaPartida.Count; i++)
            {
                jugadores = jugadores + JugadoresParaNuevaPartida[i] + "-";
            }

            string mensaje = "10/" + Convert.ToString(partida) + "/1/" + jugadores;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            this.Hide();
            //SeleccionCampeon selchamp = new SeleccionCampeon();
            //selchamp.ShowDialog();
            //JugadoresEnPartida[0].SeleccionarCampeon(selchamp.GetCampeonSeleccionado());

            //JugadoresEnPartida[1].SeleccionarCampeon("4");
            //JugadoresEnPartida[2].SeleccionarCampeon("3");
            //JugadoresEnPartida[3].SeleccionarCampeon("7");

            //Tablero tablero = new Tablero(JugadoresEnPartida, server);
            //tablero.ShowDialog();

            this.Show();
        }

        private void NuevaPartida_Click(object sender, EventArgs e)
        {
            if (!partidacreada)
            {
                partidacreada = true;
                JugadoresParaNuevaPartida.Add(Usuario);
                JugadoresParaNuevaPartidaConfirmados.Add(Usuario);
                ActualizarJugadoresEnPartida(JugadoresParaNuevaPartida);
            }
            
        }

        private void BorrarPartida_Click(object sender, EventArgs e)
        {
            partidacreada = false;
            JugadoresParaNuevaPartida.Clear();
            ActualizarJugadoresEnPartida(JugadoresParaNuevaPartida);
        }

        private void Historial_Click(object sender, EventArgs e)
        {
            string mensaje = "11/" + Usuario;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void Etiquetasbutton_Click(object sender, EventArgs e)
        {
            if (Etiqueta1.Checked)
            {
                int contador = Convert.ToInt32(HistorialGridView1.RowCount.ToString());
                for (int i = 0; i < contador - 1; i++)
                {
                    string host = HistorialGridView1.Rows[i].Cells[2].Value.ToString();
                    string Ju1 = HistorialGridView1.Rows[i].Cells[3].Value.ToString();
                    string Ju2 = HistorialGridView1.Rows[i].Cells[4].Value.ToString();
                    string Ju3 = HistorialGridView1.Rows[i].Cells[5].Value.ToString();
                    if (host != Usuario && Ju1 != Usuario && Ju2 != Usuario && Ju3 != Usuario)
                    {
                        HistorialGridView1.Rows[i].Visible = false;
                    }
                    else
                    {
                    }
                }
                //HistorialGridView1.CurrentCell = null;
            }

            if (Etiqueta2.Checked)
            {
                if (string.IsNullOrEmpty(UsuarioconBox1.Text))
                {

                    MessageBox.Show("Introduce un nombre para usar esta función.");

                    return;

                }
                else
                {
                    int contador = Convert.ToInt32(HistorialGridView1.RowCount.ToString());
                    for (int i = 0; i < contador - 1; i++)
                    {
                        string host = HistorialGridView1.Rows[i].Cells[2].Value.ToString();
                        string Ju1 = HistorialGridView1.Rows[i].Cells[3].Value.ToString();
                        string Ju2 = HistorialGridView1.Rows[i].Cells[4].Value.ToString();
                        string Ju3 = HistorialGridView1.Rows[i].Cells[5].Value.ToString();
                        if (host != UsuarioconBox1.Text && Ju1 != UsuarioconBox1.Text && Ju2 != UsuarioconBox1.Text && Ju3 != UsuarioconBox1.Text)
                        {
                            HistorialGridView1.Rows[i].Visible = false;
                        }
                        else
                        {
                        }
                    }
                }
            }

            if (Etiqueta3.Checked)
            {
                int temp = 0;
                if (string.IsNullOrEmpty(UltimasPartidasBox2.Text))
                {

                    MessageBox.Show("Debe completar la informacion");

                    return;

                }
                else if (int.TryParse(UltimasPartidasBox2.Text, out temp))
                {
                    int contador = Convert.ToInt32(HistorialGridView1.RowCount.ToString());
                    int recontar = contador - Convert.ToInt32(UltimasPartidasBox2.Text);
                    for (int i = 0; i < recontar - 1; i++)
                    {
                        HistorialGridView1.Rows[i].Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Escriba solo numeros");
                }
            }

            if (Etiqueta4.Checked)
            {
                int contador = Convert.ToInt32(HistorialGridView1.RowCount.ToString());
                for (int i = 0; i < contador - 1; i++)
                {
                    HistorialGridView1.Rows[i].Visible = true;
                }

            }
        }
    }
}
