using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Guerrero : Personaje
    {

        public Guerrero(int id, string nombre, short nivel) : base(id, nombre, nivel)
        {
                
        }
        public override void AplicarBeneficioDeClase()
        {
            puntosDeDefensa += (int)puntosDeDefensa * 10 / 100;
        }
    }
}
