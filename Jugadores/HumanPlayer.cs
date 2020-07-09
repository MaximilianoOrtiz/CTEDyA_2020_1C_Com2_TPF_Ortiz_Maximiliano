
using System;
using System.Collections.Generic;
using System.Linq;


namespace juegoIA
{

	public class HumanPlayer : Jugador
	{
		private List<int> naipes = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite;
		private bool random_card = false;
		
		
		public HumanPlayer(){}
		        
		public HumanPlayer(bool random_card)
		{
			this.random_card = random_card;
		}

		/// 
        /// Inicializa el juego que desarrolllara el jugador humano con las cartas propias, del oponente y el limite fijado
        /// 
        /// <param name="cartasPropias"> cartas del jugador Humano</param>
        /// <param name="cartasOponente">cartas de la IA</param>
        /// <param name="limite">Limite fijado que el monticulo no debe ser superado</param>
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			this.naipes = cartasPropias;
			this.naipesComputer = cartasOponente;
			this.limite = limite;
		}
		
        /// Proposito:
        /// Si se encuentra en randon descarta una carta al azar,
        /// si no , muestra las cartas disponibles al usuario y pide la seleccion de una de ellas
        ///
        /// <returns> Retorna el naipe seleccionado </returns>
		public override int descartarUnaCarta()
		{
			int carta = 0;
			Console.WriteLine("Naipes disponibles (Usuario):");
			for (int i = 0; i < naipes.Count; i++) {
				Console.Write(naipes[i].ToString());
				if (i<naipes.Count-1) {
					Console.Write(", ");
				}
			}
		
			Console.WriteLine();
			if (!random_card) {
				Console.Write("Ingrese naipe:");
				string entrada = Console.ReadLine();
				
				Int32.TryParse(entrada, out carta);
				while (!naipes.Contains(carta)) {
					Console.Write("Opcion Invalida.Ingrese otro naipe:");
					entrada = Console.ReadLine();
					Int32.TryParse(entrada, out carta);
				}
			} else {
				var random = new Random();
				int index = random.Next(naipes.Count);
				carta = naipes[index];
				Console.Write("Ingrese naipe:" + carta.ToString());
			}
			
			return carta;
		}
		
		public override void cartaDelOponente(int carta){
		}
		
	}
}
