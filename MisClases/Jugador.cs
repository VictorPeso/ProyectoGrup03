using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;

namespace MisClases
{
    public class Jugador
    { 
        public string nombreCampeon { get; set; }
        public int Puntos_Vida { get; set; }
        public int CasillasMovimiento { get; set; }
        public int Danyo { get; set; }
        public Bitmap ImagenPersonajeCompleta { get; set; }
        public SoundPlayer cancion { get; set; }

        public string nombreJugador { get; set; }
        public int puntos { get; set; }
        public int id { get; set; }
        public Celda celdaactual { get; set; }
        public Celda celdaprovisional { get; set; }
        public bool EstaVivo { get; set; }

        public Jugador(int id, string usuario)
        {
            this.puntos = 0;
            this.id = id;
            this.nombreJugador = usuario;
            this.EstaVivo = true;
        }

        public void SeleccionarCampeon(string nombreCampeon)
        {
            switch (nombreCampeon)
            {
                case "1":
                    this.nombreCampeon = "Benito";
                    this.Puntos_Vida = 4;
                    this.CasillasMovimiento = 4;
                    this.Danyo = 1;
                    this.ImagenPersonajeCompleta = new Bitmap(@"Imagenes\reggaeton.png");
                    this.cancion = new SoundPlayer(@"Sonido\Reggaeton.wav");
                    break;

                case "2":
                    this.nombreCampeon = "Roxy";
                    this.Puntos_Vida = 3;
                    this.CasillasMovimiento = 5;
                    this.Danyo = 2;
                    this.ImagenPersonajeCompleta = new Bitmap(@"Imagenes\heavy.png");
                    this.cancion = new SoundPlayer(@"Sonido\Heavy.wav");
                    break;

                case "3":
                    this.nombreCampeon = "Javier";
                    this.Puntos_Vida = 4;
                    this.CasillasMovimiento = 4;
                    this.Danyo = 1;
                    this.ImagenPersonajeCompleta = new Bitmap(@"Imagenes\flamenco.png");
                    this.cancion = new SoundPlayer(@"Sonido\Flamenco.wav");
                    break;

                case "4":
                    this.nombreCampeon = "Hiyori";
                    this.Puntos_Vida = 3;
                    this.CasillasMovimiento = 4;
                    this.Danyo = 1;
                    this.ImagenPersonajeCompleta = new Bitmap(@"Imagenes\otaku.png");
                    this.cancion = new SoundPlayer(@"Sonido\Otaku.wav");
                    break;

                case "5":
                    this.nombreCampeon = "Ludwig";
                    this.Puntos_Vida = 4;
                    this.CasillasMovimiento = 4;
                    this.Danyo = 1;
                    this.ImagenPersonajeCompleta = new Bitmap(@"Imagenes\clasica.png");
                    this.cancion = new SoundPlayer(@"Sonido\Clasica.wav");
                    break;

                case "6":
                    this.nombreCampeon = "Takumi";
                    this.Puntos_Vida = 4;
                    this.CasillasMovimiento = 6;
                    this.Danyo = 2;
                    this.ImagenPersonajeCompleta = new Bitmap(@"Imagenes\eurobeat.png");
                    this.cancion = new SoundPlayer(@"Sonido\Eurobeat.wav");
                    break;

                case "7":
                    this.nombreCampeon = "Diana";
                    this.Puntos_Vida = 3;
                    this.CasillasMovimiento = 5;
                    this.Danyo = 3;
                    this.ImagenPersonajeCompleta = new Bitmap(@"Imagenes\emo.png");
                    this.cancion = new SoundPlayer(@"Sonido\Emo.wav");
                    break;

                case "8":
                    this.nombreCampeon = "Bruno";
                    this.Puntos_Vida = 6;
                    this.CasillasMovimiento = 3;
                    this.Danyo = 2;
                    this.ImagenPersonajeCompleta = new Bitmap(@"Imagenes\phonk.png");
                    this.cancion = new SoundPlayer(@"Sonido\Phonk.wav");
                    break;
            }
        }
    }
}
