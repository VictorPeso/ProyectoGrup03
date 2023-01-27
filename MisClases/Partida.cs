using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    public class Partida
    {
        public Board myTablero { get; set; }
        public Fase FaseActual { get; set; }
        public List<Jugador> ListaJugadores { get; set; }

        public Partida(int d)
        {
            this.myTablero = new Board(d);
            this.FaseActual = Fase.Inicio;
            this.ListaJugadores = new List<Jugador>();
        }

        public enum Fase
        {
            Inicio,
            Seleccion,
            Ejecucion,
            Final
        }

        public void EjecutarFase()
        {
            switch (this.FaseActual)
            {
                case Fase.Inicio:
                    break;

                case Fase.Seleccion:
                    break;

                case Fase.Ejecucion:
                    break;

                case Fase.Final:
                    break;
            }
        }
    }
}
