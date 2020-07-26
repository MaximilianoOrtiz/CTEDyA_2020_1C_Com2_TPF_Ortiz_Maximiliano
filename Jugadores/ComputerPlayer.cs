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
        private ArbolGeneral<Naipe> jugadaActual;
        bool proximoTurnoHumano = true;
        private int naipeJugadoPorElHumano;

        public ComputerPlayer() {
            jugadaActual = new ArbolGeneral<Naipe>(new Naipe(0, 0));

        }

        public ArbolGeneral<Naipe> getJugadaActual() {
            return jugadaActual;
        }

        public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite) {  //Implementar

            armarArbol(jugadaActual, new Estado(cartasPropias, cartasOponente, limite, proximoTurnoHumano));

        }

        /*
         * Proposito: Descartar una carta partiendo de la carta que tiro el usuario 
         * 
         */
        public override int descartarUnaCarta() {

            Console.ForegroundColor = ConsoleColor.Yellow;
            int naipeADescartar = 0;
            List<ArbolGeneral<Naipe>> jugadas = jugadaActual.getHijos();

            //Para cada jugada correspondiente a las posibles jugadas que puede tirar la IA
            foreach (var jugada in jugadas) {
                //si la carta tirada por el humano es igual a la de la jugada y ademas tiene valor heuristico -1
                //muestro naipes, e inicio una busqueda de la carta que mejor le convenga a la IA actualizando la jugada actual para el proximo turno
                if (jugada.getDatoRaiz().getCarta() == naipeJugadoPorElHumano && jugada.getDatoRaiz().getValorFuncionHeuristica() == -1) {
                    Console.WriteLine("");
                    Console.Write("Naipes disponibles (IA):      ");
                    foreach (var carta in jugada.getHijos()) {
                        Console.Write(carta.getDatoRaiz().getCarta() + ", ");
                    }
                    //Console.WriteLine("");
                    ArbolGeneral<Naipe> naipeAuxADescartar = cartaAdescartar(jugada);
                    this.jugadaActual = naipeAuxADescartar;
                    naipeADescartar = naipeAuxADescartar.getDatoRaiz().getCarta();
                    Console.Write("     jugada IA: " + naipeADescartar);
                    Console.ForegroundColor = ConsoleColor.White;
                    return naipeADescartar;
                }
                //si no,
                else {
                    //verifico que la carta sea igual a la que tiro el humano, y tiro la carta que mejor le convenga a la IA,
                    //actualizando la jugada actual para el proximo turno
                    if (jugada.getDatoRaiz().getCarta() == naipeJugadoPorElHumano) {
                        foreach (var naipeAJugar in jugada.getHijos()) {
                            if (naipeAJugar.getDatoRaiz().getValorFuncionHeuristica() == 1) {
                                naipeADescartar = naipeAJugar.getDatoRaiz().getCarta();
                                Console.Write("     jugada IA:   " + naipeADescartar);
                                this.jugadaActual = naipeAJugar;
                                Console.ForegroundColor = ConsoleColor.White;
                                return naipeADescartar;
                            }
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            return naipeADescartar;
        }

        public override void cartaDelOponente(int carta) {
            this.naipeJugadoPorElHumano = carta;
        }

        public void imprimirJugadasDeJugadaActual(ArbolGeneral<Naipe> jugadaActual, List<ArbolGeneral<Naipe>> camino, bool turnoIA) {
            camino.Add(jugadaActual);
            if (jugadaActual.esHoja()) {
                impimirCamino(camino, turnoIA);
            }
            else {
                foreach (var jugada in jugadaActual.getHijos()) {
                    imprimirJugadasDeJugadaActual(jugada, camino, turnoIA);
                    camino.RemoveAt(camino.Count - 1);
                }
            }
        }

        public void imprimirJugadasAPartirDePosiblesJugadas(List<int> posiblesjugadas) {
            bool seEncontroJugada = true, turnoIA = true;
            if (posiblesjugadas.Count % 2 == 0) {
                turnoIA = true;
            }
            else
                turnoIA = false;

            ArbolGeneral<Naipe> jugadaAux = jugadaActual;
            foreach (var carta in posiblesjugadas) {
                if (seEncontroJugada) {
                    foreach (var jugada in jugadaAux.getHijos()) {
                        seEncontroJugada = false;
                        if (jugada.getDatoRaiz().getCarta() == carta) {
                            jugadaAux = jugada;
                            seEncontroJugada = true;

                            break;
                        }
                    }
                }
                else
                    break;
            }
            if (seEncontroJugada == false)
                Console.WriteLine("No se encontro la posible jugada");
            else {
                Console.WriteLine(" ");
                Console.WriteLine("La jugadas posibles jugadas son :");
                Console.WriteLine(" ");
                imprimirJugadasDeJugadaActual(jugadaAux, new List<ArbolGeneral<Naipe>>(), turnoIA);
            }

        }

        public void imprimirJugadasDadaUnaProfundidad(int profundidad) {
            jugadaActual.imprimirnivelcompleto(profundidad);
        }

        /*
         * Proposito : Armar un arbol general con todas las posibles jugadas que pueden darse respecto al estado que se ingresa
         *             como argumento, asignando un valor heuristico a cada jugada siendo +1 favorable  a la IA y -1 al humano.
         * 
         * Argumentos : ---> naipeInicial : Es el naipe que representa la raiz del arbol total, inicialmente es (0,0)
         *                                  Se utliza como pibote para ir creando los demas subArboles.
         *                                  
         *              ---> estado : Contiene informacion que va a recibir cada nodo del arbol
         *                            para poder ir armando el arbol con la informacion correspondiente
         *                            a cada jugada.
         */
        private void armarArbol(ArbolGeneral<Naipe> naipeInicial, Estado estado) {

            //turno Humano
            if (estado.getProximoturnoHumano()) {
                //Si el humano tiene cartas para jugar, se las agrego al naipeInicial
                if (estado.getCartasOponente().Count != 0) {
                    foreach (int carta in estado.getCartasOponente())
                        naipeInicial.agregarHijo(new ArbolGeneral<Naipe>(new Naipe(carta, 0)));
                }
                //Para cada naipe correspondiente a la jugada del humano,
                foreach (ArbolGeneral<Naipe> naipeActual in naipeInicial.getHijos()) {
                    int nuevoLimite = estado.getLimite() - naipeActual.getDatoRaiz().getCarta();
                    //verifico
                    //si el nuevo limite no es caso base
                    if (nuevoLimite >= 0) {
                        //  actualizo su estado e inicio una llamada recursiva armarArbol, con el naipeActual y su estado actualizado
                        //  al volvel de la recursion asigno el valor de la funcion heuristica a los nodos intermedios hasta su raiz.
                        List<int> nuevasCartasOponente = new List<int>();
                        foreach (var carta in estado.getCartasOponente()) {
                            if (carta != naipeActual.getDatoRaiz().getCarta())
                                nuevasCartasOponente.Add(carta);
                        }
                        Estado nuevoEstado = new Estado(estado.getCartasPropias(), nuevasCartasOponente, nuevoLimite, false);
                        armarArbol(naipeActual, nuevoEstado);
                        miniMax(naipeActual, estado.getProximoturnoHumano());
                    }
                    //si es caso base
                    else
                        //setea el valor heuristico de las jugadas terminales
                        naipeActual.getDatoRaiz().setValorFuncionHeuristica(+1);
                }
            }
            //turno IA
            //El algoritmo es lo mismo que en el turno del humano, con la diferencia que utiliza las cartas de la IA
            else {
                if (estado.getCartasPropias().Count != 0) {
                    foreach (int carta in estado.getCartasPropias())
                        naipeInicial.agregarHijo(new ArbolGeneral<Naipe>(new Naipe(carta, 0)));
                }
                foreach (ArbolGeneral<Naipe> naipeActual in naipeInicial.getHijos()) {
                    int nuevoLimite = estado.getLimite() - naipeActual.getDatoRaiz().getCarta();
                    if (nuevoLimite >= 0) {
                        List<int> nuevasCartasPropias = new List<int>();
                        foreach (var carta in estado.getCartasPropias()) {
                            if (carta != naipeActual.getDatoRaiz().getCarta())
                                nuevasCartasPropias.Add(carta);
                        }
                        Estado nuevoEstado = new Estado(nuevasCartasPropias, estado.getCartasOponente(), nuevoLimite, true);
                        armarArbol(naipeActual, nuevoEstado);
                        miniMax(naipeActual, estado.getProximoturnoHumano());
                    }
                    else
                        naipeActual.getDatoRaiz().setValorFuncionHeuristica(-1);
                }
            }
        }

        /*
         * Proposito: Asignar a los nodos intermedios del arbol un valor heuristico indicando 
         *            lo buena(+1) o mala(-1) que fue la jugada.
         *  
         * Argumento ---> naipe: naipe actual que se esta evaluando
         *           ---> turnoHumano : indica si ese naipe corresponde al turno del humano o no.
         * 
         */
        private void miniMax(ArbolGeneral<Naipe> naipe, bool turnoHumano) {
            //si tiene solo un hijo, copio el valor heuristico del nodo terminal
            if (naipe.getHijos().Count == 1) {
                ArbolGeneral<Naipe> naipeTerminal = naipe.getHijos()[0];
                naipe.getDatoRaiz().setValorFuncionHeuristica(naipeTerminal.getDatoRaiz().getValorFuncionHeuristica());
            }
            // si no
            else {
                /*turno humano : maximiso la funcion heuristica, ya que todo nodo terminal que tenga el valor maximo
                                 elegido (+1), favoresera a la IA, por ende, si el humano tira esta carta el valor heuristico 
                                 le indicara a la IA que por ese camino gana.
                 */
                if (turnoHumano) {
                    max(naipe);
                }
                /*turno IA : minimiso la funcion heuristica ya que todo nodo terminal que tenga el valor minimo
                             elegido(-1), favoresera al humano, por ende, si la IA tira esta carta, el valor heuristico
                             le indicara a la IA que por ese camino pierde.
                */
                else
                    min(naipe);
            }
        }

        private void max(ArbolGeneral<Naipe> naipe) {
            List<ArbolGeneral<Naipe>> hijosNaipe = naipe.getHijos();
            if (hijosNaipe.Exists(X => X.getDatoRaiz().getValorFuncionHeuristica() == +1))
                naipe.getDatoRaiz().setValorFuncionHeuristica(+1);
            else
                naipe.getDatoRaiz().setValorFuncionHeuristica(-1);
        }

        private void min(ArbolGeneral<Naipe> naipe) {
            List<ArbolGeneral<Naipe>> hijosNaipe = naipe.getHijos();
            if (hijosNaipe.Exists(x => x.getDatoRaiz().getValorFuncionHeuristica() == -1))
                naipe.getDatoRaiz().setValorFuncionHeuristica(-1);
            else
                naipe.getDatoRaiz().setValorFuncionHeuristica(+1);
        }

        /* 
         * Proposito: Busca entre los hijos de las posibles jugadas que podria realizar la IA y retorna 
         *            la que mayor jugadas con valor heuristico = 1 tenga, con esto se consigue tener un criterio de
         *            seleccion cuando todas las cartas a tirar de la IA son con heuristica -1.
         *         
         *            
         * Argumento: --> jugada: representa la jugada del humano
         */
        private ArbolGeneral<Naipe> cartaAdescartar(ArbolGeneral<Naipe> jugada) {
            int positivo = 0, negativo, positivoMaximo = 0;
            ArbolGeneral<Naipe> naipeADescartar = jugada.getHijos()[0];
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
                    if (positivoMaximo == naipe.getHijos().Count() - 1) {
                        return naipeADescartar;
                    }
                }
            }
            return naipeADescartar;
        }

        private void impimirCamino(List<ArbolGeneral<Naipe>> camino, bool esturnoIA) {
            bool turnoIA = esturnoIA;

            int contador = 0, tamañoCamino = camino.Count;

            foreach (var jugada in camino) {
                if (turnoIA) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("(IA) " + jugada.getDatoRaiz().ToString() + ", ");
                    contador++;
                    if (contador == tamañoCamino) {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" Gana Hum");
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                    else
                        turnoIA = false;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("(Hum) " + jugada.getDatoRaiz().ToString() + ", ");
                    contador++;
                    if (contador == tamañoCamino) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" Gana IA");
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                    else
                        turnoIA = true;
                }
            }


        }


    }
}
