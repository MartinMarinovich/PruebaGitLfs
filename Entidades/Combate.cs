using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public delegate void EstadoCombate(IJugador jugado1, IJugador jugador2);
    public delegate void FinalizacionCombate(IJugador jugado1);
    public sealed class Combate
    {
        public event EstadoCombate RondaIniciada;
        public event FinalizacionCombate CombateFinalizado;
        static Random randoom;
        IJugador atacado;
        IJugador atacante;
        static Combate()
        {
            randoom = new Random();
        }


        private IJugador SeleccionarJugadorAleatoriamente(IJugador jugador1, IJugador jugador2)
        {
            return randoom.TirarUnaMoneda() == LadosMoneda.Cara ? jugador1 : jugador2;
        }
        private IJugador SeleccionarPrimerAtacante(IJugador jugador1, IJugador jugador2)
        {
            if (jugador1.Nivel == jugador2.Nivel)
            {
                return this.SeleccionarJugadorAleatoriamente(jugador1, jugador2);
            }

            return jugador1.Nivel < jugador2.Nivel ? jugador1 : jugador2;

        }

        private void IniciarRonda()
        {
            if (RondaIniciada is not null)
            {
                RondaIniciada.Invoke(atacante, atacado);
            }
            atacado.RecibirAtaque(atacante.Atacar());
        }

        public IJugador EvaluarGanador()
        {
            if (atacado.PuntosDeVida == 0)
            {
                return atacante;
            }
            else
            {
                IJugador auxJugador;
                auxJugador = atacante;
                atacante = atacado;
                atacado = auxJugador;

            }

            return null;
        }

        public Task IniciarCombate()
        {
            return Task.Run(Combatir);
        }
        private void Combatir()
        {
            IniciarRonda();
            IJugador ganador  = EvaluarGanador();
            while (ganador is null)
            {
                IniciarRonda();
                ganador = EvaluarGanador();
            }

            if (CombateFinalizado is not null)
            {
                CombateFinalizado(ganador);
            }
                ResultadoCombate resultado = new ResultadoCombate(ganador.ToString(),atacado.ToString(),DateTime.Now);
        }
        public void GuardarResultado(ResultadoCombate resultadoCombate)
        {
            if (resultadoCombate is not null)
            {
              //  string json = JsonSerializer.Serialize(resultado);
                //new Logger(GetFolderPath(SpecialFolder.Desktop) + "\\resultados.json").GuardarLog(json);
            }
        }
    }
}
