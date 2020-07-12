using juegoIA.Domain;
using System;
using System.Collections.Generic;

namespace juegoIA
{
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

        public void armarArbol(ArbolMiniMax<Naipe> arbolMiniMax, Estado estado) {

            //turno Humano
            if (estado.getProximoturnoHumano()) {
                if (estado.getCartasOponente().Count != 0) {
                    foreach (int carta in estado.getCartasOponente())
                        arbolMiniMax.agregarHijo(new ArbolMiniMax<Naipe>(new Naipe(carta, 0)));
                }
                foreach (ArbolMiniMax<Naipe> naipeActual in arbolMiniMax.getHijos()) {
                    int nuevoLimite = estado.getLimite() - naipeActual.getDatoRaiz().getCarta();
                    if (nuevoLimite >= 0) {
                        List<int> nuevasCartasOponente = new List<int>();
                        foreach (var carta in estado.getCartasOponente()) {
                            if (carta != naipeActual.getDatoRaiz().getCarta())
                                nuevasCartasOponente.Add(carta);
                        }
                        Estado nuevoEstado = new Estado(estado.getCartasPropias(), nuevasCartasOponente, nuevoLimite, false);
                        naipeActual.armarArbol(naipeActual, nuevoEstado);
                        miniMax(naipeActual, estado.getProximoturnoHumano());
                    }
                    else
                        naipeActual.getDatoRaiz().setValorFuncionHeuristica(+1);
                }
            }
            //turno IA
            else {
                if (estado.getCartasPropias().Count != 0) {
                    foreach (int carta in estado.getCartasPropias())
                        arbolMiniMax.agregarHijo(new ArbolMiniMax<Naipe>(new Naipe(carta, 0)));
                }
                foreach (ArbolMiniMax<Naipe> naipeActual in arbolMiniMax.getHijos()) {
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

        private void miniMax(ArbolMiniMax<Naipe> naipe, bool TurnoHumano) {
            if (naipe.getHijos().Count == 1) {
                ArbolMiniMax<Naipe> naipeTerminal = naipe.getHijos()[0];
                naipe.getDatoRaiz().setValorFuncionHeuristica(naipeTerminal.getDatoRaiz().getValorFuncionHeuristica());
            }
            else {
                if (TurnoHumano) {
                    max(naipe);
                }
                else
                    min(naipe);
            }
        }

        private void max(ArbolMiniMax<Naipe> naipe) {
           List<ArbolMiniMax<Naipe>> hijosNaipe = naipe.getHijos();
           if( hijosNaipe.Exists(X => X.getDatoRaiz().getValorFuncionHeuristica() == +1))
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
