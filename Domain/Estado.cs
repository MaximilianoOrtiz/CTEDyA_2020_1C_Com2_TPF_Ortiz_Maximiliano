using juegoIA;
using System;
using System.Collections.Generic;
using System.Text;

namespace juegoIA.Domain
{
    /*
     * Proposito : Encapsular la informacion que va a recibir cada nodo del arbol
     *            para poder ir armando el arbol con la informacion correspondiente
     *            a cada jugada.
     *            
     * Atributos --->  cartasPropias : Representa a las cartas del jugador que esta asiendo uso de la informacion del arbol
     *           --->  cartasOponente: Representa a las cartas de su oponente 
     *           --->  limite : Corresponde al limite del monticulo actual
     *           --->  proximoturnoHumano : Si esta en TRUE, significa que el proximo jugador a tirar corresponde al humano; FALSE, a la
     *                 inteligencia Artificial
     * 
     */
    public class Estado
    {
        List<int> cartasPropias;
        List<int> cartasOponente;
        int limite;
        bool proximoturnoHumano;

        public Estado(List<int> cartasPropias, List<int> cartasOponente, int limite, bool turnoHumano) {
            this.cartasPropias = cartasPropias;
            this.cartasOponente = cartasOponente;
            this.limite = limite;
            this.proximoturnoHumano = turnoHumano;
        }

        public List<int> getCartasPropias() {
            return cartasPropias;
        }

        public void setCartasPropias(List<int> value) {
            cartasPropias = value;
        }

        public List<int> getCartasOponente() {
            return cartasOponente;
        }

        public void setCartasOponente(List<int> value) {
            cartasOponente = value;
        }

        public int getLimite() {
            return limite;
        }

        public void setLimite(int value) {
            limite = value;
        }

        public bool getProximoturnoHumano() {
            return proximoturnoHumano;
        }

        public void setProximoturnoHumano(bool value) {
            proximoturnoHumano = value;
        }
    }
}
