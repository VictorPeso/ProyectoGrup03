using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    
    public class Board
    {
        public int dimension { get; set; }
        public Celda[,] matriz_celdas { get; set; }

        public Board (int s)
        {
            this.dimension = s;

            this.matriz_celdas = new Celda[this.dimension, this.dimension];

            for (int i = 0; i < this.dimension; i++)
            {
                for (int j = 0; j < this.dimension; j++)
                {
                    this.matriz_celdas[i, j] = new Celda(i, j);
                }
            }
        }

        public void ColocarPsicionInicial(Jugador jug)
        {
            switch (jug.id)
            {
                case 1:
                    jug.celdaactual = this.matriz_celdas[0, 7];
                    jug.celdaprovisional = jug.celdaactual;
                    break;

                case 2:
                    jug.celdaactual = this.matriz_celdas[7, 0];
                    jug.celdaprovisional = jug.celdaactual;
                    break;

                case 3:
                    jug.celdaactual = this.matriz_celdas[7, 14];
                    jug.celdaprovisional = jug.celdaactual;
                    break;

                case 4:
                    jug.celdaactual = this.matriz_celdas[14, 7];
                    jug.celdaprovisional = jug.celdaactual;
                    break;
            }
        }

        public void ColocarBOSS()
        {
            for (int i = 6; i < 9; i++)
            {
                for (int j = 6; j < 9; j++)
                {
                    this.matriz_celdas[i, j].BOSS = true;
                }
            }
        }

        public void MarcarSiguienteMovimientoLegal (Jugador Jug)
        {
            int x;
            int y;
            int suma;

            // Limpiar todos loa anteriores movimientos legales
            for (int i = 0; i < this.dimension; i++)
            {
                for (int j = 0; j < this.dimension; j++)
                {
                    this.matriz_celdas[i, j].MovimientoLegal = false;
                    this.matriz_celdas[i, j].EstaOcupada = false;
                }
            }

            // Encontrar los movimientos legales y marcarlos como legales
            for (int i = 0; i < this.dimension; i++)
            {
                for (int j = 0; j < this.dimension; j++)
                {
                    x = Math.Abs(i - Jug.celdaactual.posicion.row);
                    y = Math.Abs(j - Jug.celdaactual.posicion.column);
                    suma = x + y;
                    if (EstaDentro(i, j) && (suma <= Jug.CasillasMovimiento))
                    {
                        this.matriz_celdas[i, j].MovimientoLegal = true;
                    }
                }
            }

            this.matriz_celdas[Jug.celdaactual.posicion.row, Jug.celdaactual.posicion.column].EstaOcupada = true;
        }

        private bool EstaDentro(int r, int c)
        {
            bool movimientopermitido = true;
            if (!(r >= 1 && r < matriz_celdas.GetLength(0) - 1 && c >= 1 && c < matriz_celdas.GetLength(1) - 1))
            {
                movimientopermitido = false;
            }
            if (r >= 6 && r <= 8 && c >= 6 && c <= 8)
            {
                movimientopermitido = false;
            }
            return movimientopermitido;
        }

        public void MarcarAtaques(Jugador jug, string direccion)
        {
            for (int i = 0; i < this.dimension; i++)
            {
                for (int j = 0; j < this.dimension; j++)
                {
                    this.matriz_celdas[i, j].Ataque = false;
                    this.matriz_celdas[i, j].EstaOcupada = false;
                }
            }

            switch (jug.nombreCampeon)
            {
                case "Benito":
                    AtaqueReggaeton(jug, direccion);
                    break;

                case "Roxy":
                    AtaqueHeavyMetal(jug, direccion);
                    break;

                case "Javier":
                    AtaqueFlamenco(jug, direccion);
                    break;

                case "Hiyori":
                    AtaqueOtaku(jug, direccion);
                    break;

                case "Ludwig":
                    AtaqueClasica(jug, direccion);
                    break;

                case "Takumi":
                    AtaqueEurobeat(jug, direccion);
                    break;

                case "Diana":
                    AtaqueEmo(jug, direccion);
                    break;

                case "Bruno":
                    AtaquePhonk(jug, direccion);
                    break;
            }
            this.matriz_celdas[jug.celdaprovisional.posicion.row, jug.celdaprovisional.posicion.column].EstaOcupada = true;
        }

        public void AtaqueReggaeton(Jugador jug, string direccion)
        {
            switch (direccion)
            {
                case "Arriba":
                    for (int r = jug.celdaprovisional.posicion.column - 1; r > jug.celdaprovisional.posicion.column - 4; r--)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    //if (jug.celdaprovisional.posicion.row - 1 >= 1 && jug.celdaprovisional.posicion.row - 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row - 1, jug.celdaprovisional.posicion.column - 3].Ataque = true;
                    //}
                    //if (jug.celdaprovisional.posicion.row + 1 >= 1 && jug.celdaprovisional.posicion.row + 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row + 1, jug.celdaprovisional.posicion.column - 3].Ataque = true;
                    //}
                    //if (jug.celdaprovisional.posicion.column + 1 >= 1 && jug.celdaprovisional.posicion.column + 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row, jug.celdaprovisional.posicion.column + 1].Ataque = true;
                    //}
                    break;

                case "Abajo": 
                    for (int r = jug.celdaprovisional.posicion.column + 1; r < jug.celdaprovisional.posicion.column + 4; r++)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    //if (jug.celdaprovisional.posicion.row - 1 >= 1 && jug.celdaprovisional.posicion.row - 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row - 1, jug.celdaprovisional.posicion.column + 3].Ataque = true;
                    //}
                    //if (jug.celdaprovisional.posicion.row + 1 >= 1 && jug.celdaprovisional.posicion.row + 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row + 1, jug.celdaprovisional.posicion.column + 3].Ataque = true;
                    //}
                    //if (jug.celdaprovisional.posicion.column - 1 >= 1 && jug.celdaprovisional.posicion.column - 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row, jug.celdaprovisional.posicion.column - 1].Ataque = true;
                    //}
                    break;

                case "Izquierda": 
                    for (int c = jug.celdaprovisional.posicion.row - 1; c > jug.celdaprovisional.posicion.row - 4; c--)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    //if (jug.celdaprovisional.posicion.column - 1 >= 1 && jug.celdaprovisional.posicion.column - 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row - 3, jug.celdaprovisional.posicion.column - 1].Ataque = true;
                    //}
                    //if (jug.celdaprovisional.posicion.column + 1 >= 1 && jug.celdaprovisional.posicion.column + 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row - 3, jug.celdaprovisional.posicion.column + 1].Ataque = true;
                    //}
                    //if (jug.celdaprovisional.posicion.row + 1 >= 1 && jug.celdaprovisional.posicion.row + 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row + 1, jug.celdaprovisional.posicion.column].Ataque = true;
                    //}
                    break;

                case "Derecha": 
                    for (int c = jug.celdaprovisional.posicion.row + 1; c < jug.celdaprovisional.posicion.row + 4; c++)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    //if (jug.celdaprovisional.posicion.column - 1 >= 1 && jug.celdaprovisional.posicion.column - 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row + 3, jug.celdaprovisional.posicion.column - 1].Ataque = true;
                    //}
                    //if (jug.celdaprovisional.posicion.column + 1 >= 1 && jug.celdaprovisional.posicion.column + 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row + 3, jug.celdaprovisional.posicion.column + 1].Ataque = true;
                    //}
                    //if (jug.celdaprovisional.posicion.row + 1 >= 1 && jug.celdaprovisional.posicion.row + 1 < matriz_celdas.GetLength(0) - 1)
                    //{
                    //    this.matriz_celdas[jug.celdaprovisional.posicion.row - 1, jug.celdaprovisional.posicion.column].Ataque = true;
                    //}
                    break;
            }
        }

        public void AtaqueHeavyMetal(Jugador jug, string direccion)
        {
            switch (direccion)
            {
                case "Arriba":
                    for (int r = jug.celdaprovisional.posicion.column - 1; r > jug.celdaprovisional.posicion.column - 4; r--)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Abajo":
                    for (int r = jug.celdaprovisional.posicion.column + 1; r < jug.celdaprovisional.posicion.column + 4; r++)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Izquierda":
                    for (int c = jug.celdaprovisional.posicion.row - 1; c > jug.celdaprovisional.posicion.row - 4; c--)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;

                case "Derecha":
                    for (int c = jug.celdaprovisional.posicion.row + 1; c < jug.celdaprovisional.posicion.row + 4; c++)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;
            }
        }

        public void AtaqueFlamenco(Jugador jug, string direccion)
        {
            switch (direccion)
            {
                case "Arriba":
                    for (int r = jug.celdaprovisional.posicion.column - 1; r > jug.celdaprovisional.posicion.column - 4; r--)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Abajo":
                    for (int r = jug.celdaprovisional.posicion.column + 1; r < jug.celdaprovisional.posicion.column + 4; r++)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Izquierda":
                    for (int c = jug.celdaprovisional.posicion.row - 1; c > jug.celdaprovisional.posicion.row - 4; c--)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;

                case "Derecha":
                    for (int c = jug.celdaprovisional.posicion.row + 1; c < jug.celdaprovisional.posicion.row + 4; c++)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;
            }
        }

        public void AtaqueOtaku(Jugador jug, string direccion)
        {
            switch (direccion)
            {
                case "Arriba":
                    for (int r = jug.celdaprovisional.posicion.column - 1; r > jug.celdaprovisional.posicion.column - 4; r--)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Abajo":
                    for (int r = jug.celdaprovisional.posicion.column + 1; r < jug.celdaprovisional.posicion.column + 4; r++)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Izquierda":
                    for (int c = jug.celdaprovisional.posicion.row - 1; c > jug.celdaprovisional.posicion.row - 4; c--)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;

                case "Derecha":
                    for (int c = jug.celdaprovisional.posicion.row + 1; c < jug.celdaprovisional.posicion.row + 4; c++)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;
            }
        }

        public void AtaqueClasica(Jugador jug, string direccion)
        {
            switch (direccion)
            {
                case "Arriba":
                    for (int r = jug.celdaprovisional.posicion.column - 1; r > jug.celdaprovisional.posicion.column - 4; r--)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Abajo":
                    for (int r = jug.celdaprovisional.posicion.column + 1; r < jug.celdaprovisional.posicion.column + 4; r++)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Izquierda":
                    for (int c = jug.celdaprovisional.posicion.row - 1; c > jug.celdaprovisional.posicion.row - 4; c--)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;

                case "Derecha":
                    for (int c = jug.celdaprovisional.posicion.row + 1; c < jug.celdaprovisional.posicion.row + 4; c++)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;
            }
        }

        public void AtaqueEurobeat(Jugador jug, string direccion)
        {
            switch (direccion)
            {
                case "Arriba":
                    for (int r = jug.celdaprovisional.posicion.column - 1; r > jug.celdaprovisional.posicion.column - 4; r--)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Abajo":
                    for (int r = jug.celdaprovisional.posicion.column + 1; r < jug.celdaprovisional.posicion.column + 4; r++)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Izquierda":
                    for (int c = jug.celdaprovisional.posicion.row - 1; c > jug.celdaprovisional.posicion.row - 4; c--)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;

                case "Derecha":
                    for (int c = jug.celdaprovisional.posicion.row + 1; c < jug.celdaprovisional.posicion.row + 4; c++)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;
            }
        }

        public void AtaqueEmo(Jugador jug, string direccion)
        {
            switch (direccion)
            {
                case "Arriba":
                    for (int r = jug.celdaprovisional.posicion.column - 1; r > jug.celdaprovisional.posicion.column - 4; r--)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Abajo":
                    for (int r = jug.celdaprovisional.posicion.column + 1; r < jug.celdaprovisional.posicion.column + 4; r++)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Izquierda":
                    for (int c = jug.celdaprovisional.posicion.row - 1; c > jug.celdaprovisional.posicion.row - 4; c--)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;

                case "Derecha":
                    for (int c = jug.celdaprovisional.posicion.row + 1; c < jug.celdaprovisional.posicion.row + 4; c++)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;
            }
        }

        public void AtaquePhonk(Jugador jug, string direccion)
        {
            switch (direccion)
            {
                case "Arriba":
                    for (int r = jug.celdaprovisional.posicion.column - 1; r > jug.celdaprovisional.posicion.column - 4; r--)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Abajo":
                    for (int r = jug.celdaprovisional.posicion.column + 1; r < jug.celdaprovisional.posicion.column + 4; r++)
                    {
                        if (r >= 1 && r < matriz_celdas.GetLength(0) - 1)
                        {
                            this.matriz_celdas[jug.celdaprovisional.posicion.row, r].Ataque = true;
                        }
                    }
                    break;

                case "Izquierda":
                    for (int c = jug.celdaprovisional.posicion.row - 1; c > jug.celdaprovisional.posicion.row - 4; c--)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;

                case "Derecha":
                    for (int c = jug.celdaprovisional.posicion.row + 1; c < jug.celdaprovisional.posicion.row + 4; c++)
                    {
                        if (c >= 1 && c < matriz_celdas.GetLength(1) - 1)
                        {
                            this.matriz_celdas[c, jug.celdaprovisional.posicion.column].Ataque = true;
                        }
                    }
                    break;
            }
        }
    }
}
