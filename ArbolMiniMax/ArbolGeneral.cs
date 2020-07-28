
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
    public class ArbolGeneral<T>
    {

        private NodoGeneral<T> raiz;

        public ArbolGeneral(T dato) {
            this.raiz = new NodoGeneral<T>(dato);
        }

        private ArbolGeneral(NodoGeneral<T> nodo) {
            this.raiz = nodo;
        }

        private NodoGeneral<T> getRaiz() {
            return raiz;
        }

        public T getDatoRaiz() {
            return this.getRaiz().getDato();
        }

        public List<ArbolGeneral<T>> getHijos() {
            List<ArbolGeneral<T>> temp = new List<ArbolGeneral<T>>();
            foreach (var element in this.raiz.getHijos()) {
                temp.Add(new ArbolGeneral<T>(element));
            }
            return temp;
        }

        public void agregarHijo(ArbolGeneral<T> hijo) {
            this.raiz.getHijos().Add(hijo.getRaiz());
        }

        public void eliminarHijo(ArbolGeneral<T> hijo) {
            this.raiz.getHijos().Remove(hijo.getRaiz());
        }

        public bool esVacio() {
            return this.raiz == null;
        }

        public bool esHoja() {
            return this.raiz != null && this.getHijos().Count == 0;
        }

        public int altura() {
            int alturaMaxima = 0, alturaActual = 0;
            if (esVacio())
                return 0;
            else {
                List<ArbolGeneral<T>> listaDehijos = getHijos();
                foreach (ArbolGeneral<T> arbolActual in listaDehijos) {
                    alturaActual = arbolActual.altura() + 1;
                    if (alturaActual > alturaMaxima)
                        alturaMaxima = alturaActual;
                }
            }
            return alturaMaxima;
        }

        public int ancho() {
            int anchoActual = 0, anchoMaximo = 0;
            Cola<NodoGeneral<T>> colaPrincipal = new Cola<NodoGeneral<T>>();
            NodoGeneral<T> nodoAux;

            //si el nodo raiz no tiene hijo es un arbol con anchura 1
            colaPrincipal.encolar(this.raiz);
            colaPrincipal.encolar(null);
            nodoAux = colaPrincipal.tope();
            List<NodoGeneral<T>> Hijos = nodoAux.getHijos();
            if (Hijos.Count == 0)
                return 1;
            while (!colaPrincipal.esVacia()) { //voy agregando a colaPrincipal todos los nodos que ahi por nivel llevando un conteo de los mismo
                nodoAux = colaPrincipal.desencolar();
                if (nodoAux != null) {
                    List<NodoGeneral<T>> hijos = nodoAux.getHijos();
                    if (hijos.Count != 0) {
                        foreach (NodoGeneral<T> hijo in hijos) {
                            colaPrincipal.encolar(hijo);
                            anchoActual += 1;
                        }
                    }
                }
                else { // verifico y actualizo mi maxima cantidad de nodos por nivel contados
                    if (anchoActual > anchoMaximo)
                        anchoMaximo = anchoActual;
                    if (!colaPrincipal.esVacia())
                        colaPrincipal.encolar(null);
                    anchoActual = 0;
                }
            }
            return anchoMaximo;
        }

        public void imprimirnivelcompleto(int nivel) {
            Console.WriteLine();
            int nivelAactual = 0;
            Cola<NodoGeneral<T>> colaPrincipal = new Cola<NodoGeneral<T>>();
            NodoGeneral<T> nodoAux;
            if (!this.esVacio()) {
                colaPrincipal.encolar(this.raiz);
                colaPrincipal.encolar(null);
                while (!colaPrincipal.esVacia()) {
                    nodoAux = colaPrincipal.desencolar();
                    if (nodoAux != null) {
                        if (nivelAactual == nivel) {
                            if (nivel % 2 == 0) {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(nodoAux.getDato());
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(nodoAux.getDato());
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        else {
                            List<NodoGeneral<T>> hijos = nodoAux.getHijos();
                            if (hijos.Count != 0) {
                                foreach (NodoGeneral<T> hijo in hijos) {
                                    colaPrincipal.encolar(hijo);
                                }
                            }
                        }
                    }
                    else {
                        nivelAactual++;
                        if (!colaPrincipal.esVacia())
                            colaPrincipal.encolar(null);
                    }

                }
            }
        }
    }
}
