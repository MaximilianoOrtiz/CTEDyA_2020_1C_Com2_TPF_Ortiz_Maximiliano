
using juegoIA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
    public class ComputerPlayer : Jugador
    {
        private ArbolMiniMax<Naipe> arbolMiniMax;
        bool turnoHumano = true;

        public ComputerPlayer() {
            arbolMiniMax = new ArbolMiniMax<Naipe>(new Naipe(0, 0));
        }

        //public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite) {  //Implementar

        //          arbolMiniMax.armarArbol(arbolMiniMax, cartasPropias, cartasOponente, limite, turnoHumano);

        //          arbolMiniMax.recorridoPornivel();

        //}
        public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite) {  //Implementar

            arbolMiniMax.armarArbol(arbolMiniMax, new Estado(cartasPropias, cartasOponente, limite, turnoHumano));

            arbolMiniMax.recorridoPornivel();

        }


        public override int descartarUnaCarta() {
            //Implementar
            return 0;
        }

        public override void cartaDelOponente(int carta) {
            //implementar

        }



        /// si el nodo referenciado maximisa la funcion Heristica
        /// 
        /// <param name="nodoActual"> Representa al nodo referenciado </param>
        //private void maximixar(ArbolMiniMax<int> nodoActual) {
        //    if (nodoActual.getHijos().Count == 0)
        //        nodoActual.getRaiz().SetFuncionHeuristica(1);
        //    else {
        //        foreach(ArbolMiniMax<int> nodo in nodoActual.getHijos()) {
        //            if (nodo.getRaiz().GetFuncionHeuristica() == 1) {
        //                nodoActual.getRaiz().SetFuncionHeuristica(1);
        //                return;
        //            }
        //        }
        //    }
        //}


        ///// si el nodo referenciado minimisa la funcion Heristica
        ///// 
        ///// <param name="nodoActual"> Representa al nodo referenciado </param>
        //private void minimizar(ArbolMiniMax<int> nodoActual) {
        //    if (nodoActual.getHijos().Count == 0)
        //        nodoActual.getRaiz().SetFuncionHeuristica(-1);
        //    else {
        //        foreach (ArbolMiniMax<int> nodo in nodoActual.getHijos()) {
        //            if (nodo.getRaiz().GetFuncionHeuristica() == 1) {
        //                nodoActual.getRaiz().SetFuncionHeuristica(-1);
        //                return;
        //            }
        //        }
        //    }
        //}
    }
}
