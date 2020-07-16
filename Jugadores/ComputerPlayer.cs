using juegoIA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
/*
 * Próposito: Representar al jugador con Inteligencia Artificial
 * 
 * Constructor : setea la jugada inicial en 0,0;
 * 
 */

namespace juegoIA
{
    public class ComputerPlayer : Jugador
    {
        private ArbolMiniMax<Naipe> jugadaActual;
        bool proximoTurnoHumano = true;
        private int naipeJugadoPorElHumano;      

        public ComputerPlayer() {
            jugadaActual = new ArbolMiniMax<Naipe>(new Naipe(0, 0));
        }
  
        public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite) {  //Implementar

            jugadaActual.armarArbol(jugadaActual, new Estado(cartasPropias, cartasOponente, limite, proximoTurnoHumano));

            // jugada.recorridoPornivel();
        }

        /*
         * Proposito: Descartar una carta partiendo de la carta que tiro el usuario 
         * 
         */
        public override int descartarUnaCarta() {

            int naipeADescartar = 0;
            List<ArbolMiniMax<Naipe>> jugadas = jugadaActual.getHijos();        
            
            //Para cada jugada correspondiente a las posibles jugadas que puede tirar la IA
            foreach (var jugada in jugadas) {
                //si la carta tirada por el humano es igual a la de la jugada y ademas tiene valor heuristico -1
                //muestro naipes, e inicio una busqueda de la carta que mejor le convenga a la IA actualizando la jugada actual para el proximo turno
                if (jugada.getDatoRaiz().getCarta() == naipeJugadoPorElHumano && jugada.getDatoRaiz().getValorFuncionHeuristica() == -1) {
                    Console.WriteLine("");
                    Console.WriteLine("Naipes disponibles (IA):");
                    foreach (var carta in jugada.getHijos()) {
                        Console.Write(carta.getDatoRaiz().getCarta() + ", ");
                    }
                    ArbolMiniMax<Naipe> naipeAuxADescartar = cartaAdescartar(jugada);              
                    this.jugadaActual = naipeAuxADescartar;
                    naipeADescartar = naipeAuxADescartar.getDatoRaiz().getCarta();
                    Console.WriteLine("jugada IA: " + naipeADescartar);
                    return naipeADescartar;
                }
                //si no,
                else {
                    //verifico que la carta sea igual a la que tiro el humano, y tiro la carta que mejor le convenga a la IA,
                    //actualizando la jugada actual para el proximo turno
                    if (jugada.getDatoRaiz().getCarta() == naipeJugadoPorElHumano) {
                        Console.WriteLine("");
                        Console.WriteLine("Naipes disponibles (IA): ");
                        foreach (var carta in jugada.getHijos()) {
                            Console.Write(carta.getDatoRaiz().getCarta() + ", ");
                        }
                        foreach (var naipeAJugar in jugada.getHijos()) {
                            if (naipeAJugar.getDatoRaiz().getValorFuncionHeuristica() == 1) {
                                naipeADescartar = naipeAJugar.getDatoRaiz().getCarta();
                                Console.WriteLine("jugada IA: " + naipeADescartar);
                                this.jugadaActual = naipeAJugar;
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

        /* 
         * Proposito: Busca entre los hijos de las posibles jugadas que podria realizar la IA y retorna 
         *            la que mayor jugadas con valor heuristico = 1 tenga, con esto se consigue tener un criterio de
         *            seleccion cuando todas las cartas a tirar de la IA son con heuristica -1.
         *         
         *            
         * Argumento: --> jugada: representa la jugada del humano
         */
        private ArbolMiniMax<Naipe> cartaAdescartar(ArbolMiniMax<Naipe> jugada) {
            int positivo = 0, negativo, positivoMaximo = 0;
            ArbolMiniMax<Naipe> naipeADescartar = jugada.getHijos()[0];
            foreach (var naipe in jugada.getHijos()) {
                positivo = 0; negativo = 0;
                foreach (var n in naipe.getHijos()) {   
                    if (n.getDatoRaiz().getValorFuncionHeuristica() == 1)
                        positivo++;
                    else
                        negativo++;
                }
                if (positivo > negativo) {
                    if (positivo > positivoMaximo) {
                        positivoMaximo = positivo;
                        naipeADescartar = naipe;
                    }
                    if(positivoMaximo == naipe.getHijos().Count() -1) {
                        return naipeADescartar;
                    }
                }
            }
            return naipeADescartar;
        }
    }
}
