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
    public partial class GestionUsuarios : Form
    {
        Socket server;
        string Usuario;
        Thread atender;

        List<Jugador> JugadoresEnPartida = new List<Jugador>();
        List<string> JugadoresParaNuevaPartida = new List<string>();
        bool partidacreada = false;

        delegate void DelegadoParaActualizarLista(string frase, int rows);
        delegate void DelegadoParaPonerenLista(string[] mensajes);

        public GestionUsuarios()
        {
            InitializeComponent();
        }

        private void NuevaPartida_Click(object sender, EventArgs e)
        {

        }
    }
}
