using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class ExtensionRandom
    {

        public static LadosMoneda TirarUnaMoneda(this Random random)
        {
            return (LadosMoneda) random.Next(1,3);
        }

    }
}
