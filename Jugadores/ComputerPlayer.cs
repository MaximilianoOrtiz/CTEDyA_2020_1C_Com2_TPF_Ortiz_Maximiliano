using juegoIA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
/*
 * PRÓPOSITO: 
 * 
 * 
 * */

namespace juegoIA
{
    public class ComputerPlayer : Jugador
    {
        private ArbolMiniMax<Naipe> arbolMiniMax;
        bool proximoTurnoHumano = true;
        private int naipeJugadoPorElHumano;
        ArbolMiniMax<Naipe> naipeActualAux;


        public ComputerPlayer() {
            arbolMiniMax = new ArbolMiniMax<Naipe>(new Naipe(0, 0));
        }


        public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite) {  //Implementar

            arbolMiniMax.armarArbol(arbolMiniMax, new Estado(cartasPropias, cartasOponente, limite, proximoTurnoHumano));

            // arbolMiniMax.recorridoPornivel();
        }


        public override int descartarUnaCarta() {

            int naipeADescartar = 0;
            List<ArbolMiniMax<Naipe>> hijosNaipes = arbolMiniMax.getHijos();

            foreach (var naipeactual in hijosNaipes) {
                if (naipeactual.getDatoRaiz().getCarta() == naipeJugadoPorElHumano && naipeactual.getDatoRaiz().getValorFuncionHeuristica() == -1) {
                    Console.WriteLine("");
                    Console.WriteLine("Naipes disponibles (IA):");
                    foreach (var carta in naipeactual.getHijos()) {
                        Console.Write(carta.getDatoRaiz().getCarta() + ", ");
                    }
                    naipeADescartar = naipeactual.getHijos()[0].getDatoRaiz().getCarta();
                    arbolMiniMax = naipeactual.getHijos()[0];
                    Console.WriteLine("jugada IA: " + naipeADescartar);
                    return naipeADescartar;
                }
                else {
                    if (naipeactual.getDatoRaiz().getCarta() == naipeJugadoPorElHumano) {
                        Console.WriteLine("");
                        Console.WriteLine("Naipes disponibles (IA): ");
                        foreach (var carta in naipeactual.getHijos()) {
                            Console.Write(carta.getDatoRaiz().getCarta() + ", ");
                        }
                        foreach (var naipeAJugar in naipeactual.getHijos()) {
                            if (naipeAJugar.getDatoRaiz().getValorFuncionHeuristica() == 1) {
                                naipeADescartar = naipeAJugar.getDatoRaiz().getCarta();
                                Console.WriteLine("jugada IA: " + naipeADescartar);
                                arbolMiniMax = naipeAJugar;
                                return naipeADescartar;
                            }
                        }
                    }
                }

            }
            return naipeADescartar;
        }

        public override void cartaDelOponente(int carta) {
            this.naipeJugadoPorElHumano = carta;
        }
    }
}
