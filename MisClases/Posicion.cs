using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    public class Posicion
    {
        public int row { get; set; }
        public int column { get; set; }

        public Posicion(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }
}
