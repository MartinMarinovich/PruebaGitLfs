using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Hechicero : Personaje
    {
        public Hechicero(int id, string nombre, short nivel) : base(id,nombre,nivel)
        {

        }

        public override void AplicarBeneficioDeClase()
        {
            puntosDePoder += (int)puntosDePoder * 10 / 100;
        }
    }
}
