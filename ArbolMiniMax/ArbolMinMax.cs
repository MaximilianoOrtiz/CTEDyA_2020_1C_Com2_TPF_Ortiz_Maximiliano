using juegoIA.Domain;
using System;
using System.Collections.Generic;

namespace juegoIA
{
    /*
     * Proposito: Representar una estructura de datos tipo Arbol General que permita alojar 
     *            en cada uno de sus nodos el estado y valor heuristico de dicha jugada, para
     *            posteriormente utilizarlo en un juego de dos oponentes, donde cada uno conoce de 
     *            antemano las posibles jugadas del otro.
     *            
     * Atributos: ---> raiz :  Nodo del arbol, que contendra datos(en este caso un Naipe) y referencias a sus hijos.       
     * 
     */
    public class ArbolMiniMax<T>
    {

        private NodoMiniMax<T> raiz;

        public ArbolMiniMax(T dato) {
            this.raiz = new NodoMiniMax<T>(dato);
        }

        private ArbolMiniMax(NodoMiniMax<T> nodo) {
            this.raiz = nodo;
        }

        private NodoMiniMax<T> getRaiz() {
            return raiz;
        }

        public T getDatoRaiz() {
            return this.getRaiz().getDato();
        }

        public List<ArbolMiniMax<T>> getHijos() {
            List<ArbolMiniMax<T>> temp = new List<ArbolMiniMax<T>>();
            foreach (var element in this.raiz.getHijos()) {
                temp.Add(new ArbolMiniMax<T>(element));
            }
            return temp;
        }

        public void agregarHijo(ArbolMiniMax<T> hijo) {
            this.raiz.getHijos().Add(hijo.getRaiz());
        }

        public void eliminarHijo(ArbolMiniMax<T> hijo) {
            this.raiz.getHijos().Remove(hijo.getRaiz());
        }

        public bool esVacio() {
            return this.raiz == null;
        }

        public bool esHoja() {
            return this.raiz != null && this.getHijos().Count == 0;
        }

        public int altura() {
            return 0;
        }


        public int nivel(T dato) {
            return 0;
        }

        /*
         * Proposito : Armar un arbol general con todas posibles jugadas que pueden darse respecto al estado que se ingresa
         *             como argumento, asignando un valor heuristico a cada jugada siendo +1 favorable  a la IA y -1 al humano.
         * 
         * Argumentos : ---> naipeInicial : Es el naipe que representa la raiz del arbol total, inicialmente es (0,0)
         *                                  Se utliza como pibote para ir creando los demas subArboles.
         *                                  
         *              ---> estado : Contiene informacion que va a recibir cada nodo del arbol
         *                            para poder ir armando el arbol con la informacion correspondiente
         *                            a cada jugada.
         */
        public void armarArbol(ArbolMiniMax<Naipe> naipeInicial, Estado estado) {

            //turno Humano
            if (estado.getProximoturnoHumano()) {
                //Si el humano tiene cartas para jugar, se las agrego al naipeInicial
                if (estado.getCartasOponente().Count != 0) {
                    foreach (int carta in estado.getCartasOponente())
                        naipeInicial.agregarHijo(new ArbolMiniMax<Naipe>(new Naipe(carta, 0)));
                }
                //Para cada naipe correspondiente a la jugada del humano,
                foreach (ArbolMiniMax<Naipe> naipeActual in naipeInicial.getHijos()) {
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
                        naipeActual.armarArbol(naipeActual, nuevoEstado);
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
                        naipeInicial.agregarHijo(new ArbolMiniMax<Naipe>(new Naipe(carta, 0)));
                }
                foreach (ArbolMiniMax<Naipe> naipeActual in naipeInicial.getHijos()) {
                    int nuevoLimite = estado.getLimite() - naipeActual.getDatoRaiz().getCarta();
                    if (nuevoLimite >= 0) {
                        List<int> nuevasCartasPropias = new List<int>();
                        foreach (var carta in estado.getCartasPropias()) {
                            if (carta != naipeActual.getDatoRaiz().getCarta())
                                nuevasCartasPropias.Add(carta);
                        }
                        Estado nuevoEstado = new Estado(nuevasCartasPropias, estado.getCartasOponente(), nuevoLimite, true);
                        naipeActual.armarArbol(naipeActual, nuevoEstado);
                        miniMax(naipeActual, estado.getProximoturnoHumano());
                    }
                    else
                        naipeActual.getDatoRaiz().setValorFuncionHeuristica(-1);
                }
            }
        }

        //Recorrido Por niveles
        public void recorridoPornivel() {
            Cola<NodoMiniMax<T>> colaPrincipal = new Cola<NodoMiniMax<T>>();
            NodoMiniMax<T> nodoAux;
            if (!this.esVacio()) {
                colaPrincipal.encolar(this.raiz);
                colaPrincipal.encolar(null);
                while (!colaPrincipal.esVacia()) {
                    nodoAux = colaPrincipal.desencolar();
                    if (nodoAux != null) {
                        Console.Write(nodoAux.getDato());
                        List<NodoMiniMax<T>> hijos = nodoAux.getHijos();
                        if (hijos.Count != 0) {
                            foreach (NodoMiniMax<T> hijo in hijos) {
                                colaPrincipal.encolar(hijo);
                            }
                        }
                    }
                    else {
                        if (!colaPrincipal.esVacia())
                            colaPrincipal.encolar(null);
                        Console.WriteLine(" ");
                    }

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
        private void miniMax(ArbolMiniMax<Naipe> naipe, bool turnoHumano) {
            //si tiene solo un hijo, copio el valor heuristico del nodo terminal
            if (naipe.getHijos().Count == 1) {
                ArbolMiniMax<Naipe> naipeTerminal = naipe.getHijos()[0];
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

        private void max(ArbolMiniMax<Naipe> naipe) {
            List<ArbolMiniMax<Naipe>> hijosNaipe = naipe.getHijos();
            if (hijosNaipe.Exists(X => X.getDatoRaiz().getValorFuncionHeuristica() == +1))
                naipe.getDatoRaiz().setValorFuncionHeuristica(+1);
            else
                naipe.getDatoRaiz().setValorFuncionHeuristica(-1);
        }

        private void min(ArbolMiniMax<Naipe> naipe) {
            List<ArbolMiniMax<Naipe>> hijosNaipe = naipe.getHijos();
            if (hijosNaipe.Exists(x => x.getDatoRaiz().getValorFuncionHeuristica() == -1))
                naipe.getDatoRaiz().setValorFuncionHeuristica(-1);
            else
                naipe.getDatoRaiz().setValorFuncionHeuristica(+1);
        }
    }
}
