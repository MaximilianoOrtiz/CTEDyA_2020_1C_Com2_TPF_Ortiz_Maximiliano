/*
 *Proporciona una clase abtracta donde cada clase que hereda de la misma
 * debera implementar los metodos abstractos expuestos
 * 
 **/
using System;
using System.Collections.Generic;
using System.Linq;


namespace juegoIA
{
	public abstract class Jugador
	{
		public  abstract void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite);
		public  abstract int descartarUnaCarta();
		public abstract void cartaDelOponente(int carta);
      
    }
}
