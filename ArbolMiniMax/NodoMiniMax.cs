using System;
using System.Collections.Generic;

namespace juegoIA
{
	/// <summary>
	/// Description of NodoMiniMax.
	/// </summary>
	public class NodoMiniMax<T>
	{
		private T dato;
		private List<NodoMiniMax<T>> hijos;
		
		public NodoMiniMax(T dato){		
			this.dato = dato;
			this.hijos = new List<NodoMiniMax<T>>();
		}
	
		public T getDato(){		
			return this.dato; 
		}
		
		public List<NodoMiniMax<T>> getHijos(){		
			return this.hijos;
		}

		public void setDato(T dato){		
			this.dato = dato;
		}
		
		public void setHijos(List<NodoMiniMax<T>> hijos){		
			this.hijos = hijos;
		}
	
	}
}
