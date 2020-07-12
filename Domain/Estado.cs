using juegoIA;
using System;
using System.Collections.Generic;
using System.Text;

namespace juegoIA.Domain
{
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
