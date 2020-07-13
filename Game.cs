/*
 * Proposito:En esta clase se presenta la interaccion del juego entre el humano y la IA
 * 
 * Constructor:  Inicializa el juego repartiendo de forma aleatoria  6 naipes para cada jugador 
 * 
 * **/
using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
 
	public class Game
	{
		public static int WIDTH = 12; //12
		public static int UPPER = 35; //35
		public static int LOWER = 25; //25
		
		private Jugador player1 = new ComputerPlayer();
		private Jugador player2 = new HumanPlayer();
		private List<int> naipesHuman = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite;
		private bool juegaHumano = false;
		
		
		public Game()
		{
			var rnd = new Random();
            limite = rnd.Next(LOWER, UPPER);
            //limite = 28;
            //naipesHuman.Add(1);
            //naipesHuman.Add(7);
            //naipesHuman.Add(9);
            //naipesHuman.Add(12);
            //naipesHuman.Add(5);
            //naipesHuman.Add(6);

            //naipesComputer.Add(2);
            //naipesComputer.Add(3);
            //naipesComputer.Add(4);
            //naipesComputer.Add(8);
            //naipesComputer.Add(10);
            //naipesComputer.Add(11);
            naipesHuman = Enumerable.Range(1, WIDTH).OrderBy(x => rnd.Next()).Take(WIDTH / 2).ToList();

            for (int i = 1; i <= WIDTH; i++) {
                if (!naipesHuman.Contains(i)) {
                    naipesComputer.Add(i);
                }
            }
            player1.incializar(naipesComputer, naipesHuman, limite);
			player2.incializar(naipesHuman, naipesComputer, limite);
			
		}
        /*
         * Imprime por pantalla el limite del monticulo
         * **/
		private void printScreen()
		{
			Console.WriteLine();
			Console.WriteLine("Limite:" + limite.ToString());
		}
		
        /*
         * 
         * **/
		private void turn(Jugador jugador, Jugador oponente, List<int> naipes)
		{
			int carta = jugador.descartarUnaCarta();
			naipes.Remove(carta);
			limite -= carta;
			oponente.cartaDelOponente(carta);
			juegaHumano = !juegaHumano;
		}
		
		
		
		private void printWinner()
		{
			if (!juegaHumano) {
				Console.WriteLine("Gano el Ud");
			} else {
				Console.WriteLine("Gano Computer");
			}
			
		}
		
		private bool fin()
		{
			return limite < 0;
		}
		
		public void play()
		{
			while (!this.fin()) {
				this.printScreen();
				this.turn(player2, player1, naipesHuman); // Juega el usuario
				if (!this.fin()) {
					this.printScreen();
					this.turn(player1, player2, naipesComputer); // Juega la IA
				}
			}
			this.printWinner();
		}
	}
}
