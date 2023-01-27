using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    public class Celda
    {
        public Posicion posicion { get; set; }
        public bool EstaOcupada { get; set; }
        public bool MovimientoLegal { get; set; }
        public bool Ataque { get; set; }
        public bool BOSS { get; set; }

        public Celda(int row, int column)
        {
            this.posicion = new Posicion(row, column);
        }
    }

}
