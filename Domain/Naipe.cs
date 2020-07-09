/*
 * Esta clase conforma el atributo basico que va a componer un arbol miniMax,
 * en el mismo se encuentra los cuatros estados elementales para poder proceder con el armado
 * de cada sub-arbol a partir de la raiz.
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace juegoIA
{
    public class Naipe
    {
        private int carta;
        private int valorFuncionHeuristica;

        public Naipe(int carta, int valorFuncionHeuristica) {
            this.carta = carta;
            this.valorFuncionHeuristica = valorFuncionHeuristica;
        }

        public int getCarta() {
            return carta;
        }

        public void setCarta(int value) {
            carta = value;
        }

        public int getValorFuncionHeuristica() {
            return valorFuncionHeuristica;
        }

        public void setValorFuncionHeuristica(int value) {
            valorFuncionHeuristica = value;
        }

        public override string ToString() {
            return "( " + carta + " , " + valorFuncionHeuristica + " )";
        }


        //private string estado;
        //private int naipe;
        //private List<int> cartasPropias;
        //private List<int> cartasOponentes;
        //private int limite;
        //private string turno;

        //public Naipe(int naipe, string estado, List<int> cartasPropias, List<int> cartasOponentes, int limite, string turno) {
        //    this.estado = estado;
        //    this.cartasPropias = cartasPropias;
        //    this.cartasOponentes = cartasOponentes;
        //    this.limite = limite;
        //    this.turno = turno;
        //}

        //public Naipe(int naipe, List<int> cartasPropias, List<int> cartasOponentes, int limite, string turno) {
        //    this.naipe = naipe;
        //    this.cartasPropias = cartasPropias;
        //    this.cartasOponentes = cartasOponentes;
        //    this.limite = limite;
        //    this.turno = turno;
        //}

        //public List<int> getCartasPropias() {
        //    return cartasPropias;
        //}

        //public void setCartasPropias(List<int> value) {
        //    cartasPropias = value;
        //}

        //public List<int> getCartasOponentes() {
        //    return cartasOponentes;
        //}

        //public void setCartasOponentes(List<int> value) {
        //    cartasOponentes = value;
        //}

        //public int getLimite() {
        //    return limite;
        //}

        //public void setLimite(int value) {
        //    limite = value;
        //}

        //public string getTurno() {
        //    return turno;
        //}

        //public void setTurno(string value) {
        //    turno = value;
        //}
    }
}
