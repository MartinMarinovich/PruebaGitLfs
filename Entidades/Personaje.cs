using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entidades
{
    public delegate void Ataque(Personaje personaje, int numero);
    public abstract class Personaje
    {
        decimal id;
        short nivel;
        string nombre;
        protected int puntosDeDefensa;
        protected int puntosDeVida;
        protected int puntosDePoder;
        static Random random;
        string titulo;
        const int NIVEL_MAXIMO = 100;
        const int NIVEL_MINIMO = 1;
        public event Ataque AtaqueLanzado;
        public event Ataque AtaqueRecibido;
        
        private Personaje()
        {
            random = new Random();
        }

 
        public Personaje(decimal id, string nombre, short nivel)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentNullException();
            }else if(nivel < NIVEL_MINIMO || nivel > NIVEL_MAXIMO)
            {
                throw new BusinessException("Error, nivel invalido");
            }
            puntosDeDefensa = 100 * nivel;
            puntosDePoder = 100 * nivel;
            puntosDeVida = 500 * nivel;
            this.id = id;
            this.nombre = nombre.Trim();

        }
        public Personaje(decimal id, string nombre) : this(id,nombre,1)
        {

        }

        public abstract void AplicarBeneficioDeClase();

       public static  bool operator ==(Personaje pj1, Personaje pj2)
       {
            return pj1 is not null && pj2 is not null && pj1.GetHashCode() == pj2.GetHashCode();
       }
        public static bool operator !=(Personaje pj1, Personaje pj2)
        {
            return !(pj1 == pj2);
        }

        public override bool Equals(object obj)
        {
            return this == obj as Personaje;
        }
        public string Titulo { set => titulo = value; }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }
        public override string ToString()
        {
            return String.Format("{0}{1}", nombre,string.IsNullOrEmpty(titulo)?"" : $",{titulo}") ;
        }

        public int Atacar()
        {
            Thread.Sleep(random.Next(1000, 5001));
            int puntosDeAtaque = puntosDePoder * random.Next(10, 101) / 100;
           
            if (AtaqueLanzado is not null)
            {
                AtaqueLanzado.Invoke(this, puntosDePoder);
            }

            return puntosDeAtaque;
        }

        public void RecibirAtaque(int puntosDeAtaqueRecibidos)
        {
            puntosDeAtaqueRecibidos -= puntosDeDefensa * random.Next(10, 101) / 100;
            puntosDeVida -= puntosDeAtaqueRecibidos;

            if (puntosDeVida < 0)
            {
                puntosDeVida = 0;
            }

            if (AtaqueRecibido is not null)
            {
                AtaqueRecibido.Invoke(this,puntosDeAtaqueRecibidos);
            }

        }
    }
}
