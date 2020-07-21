using System;
using System.Collections.Generic;
using System.Text;

namespace juegoIA
{
    /*
     * Proposito :  Encapsular la información que contendra cada nodo del arbol miniMax
     * 
     * Atributos --> carta : corresponde al valor del naipe jugado y perteneciente a un nodo especifico
     *           --> valorFuncionHeuristica : marca que tan buena es esa carta jugada  
     * 
     */
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
            return "( " + carta + " , " + valorFuncionHeuristica + " ), ";
        }
    }
}
