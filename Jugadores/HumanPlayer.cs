
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


        public HumanPlayer() { }

        public HumanPlayer(bool random_card) {
            this.random_card = random_card;
        }

        /*
         * Proposito: Inicializa el juego que desarrolllara el jugador humano con las cartas propias, del oponente y el limite fijado
         * 
         * Argumentos : ---> cartasPropias: cartas del jugador Humano
         *              ---> cartasOponente: cartas de la IA
         *              ---> limite: Limite fijado que el monticulo no debe ser superado
         */
        public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite) {
            this.naipes = cartasPropias;
            this.naipesComputer = cartasOponente;
            this.limite = limite;
        }

        /*
         * Proposito:  Si se encuentra en randon descarta una carta al azar,
         *             si no, muestra las cartas disponibles al usuario y pide la seleccion de una de ellas
         * 
         */
        public override int descartarUnaCarta() {
           
            Console.ForegroundColor = ConsoleColor.Green;
            int carta = 0;
            Console.Write("Naipes disponibles (Usuario):    ");
            for (int i = 0; i < naipes.Count; i++) {
                Console.Write(naipes[i].ToString());
                if (i < naipes.Count - 1) {
                    Console.Write(", ");
                }
            }
           
            if (!random_card) {
                Console.Write("     Su jugada: ");
                string entrada = Console.ReadLine();


                Int32.TryParse(entrada, out carta);

                while (!naipes.Contains(carta)) {
                    Console.Write("Opcion Invalida.Ingrese otro naipe:");
                    entrada = Console.ReadLine();
                    Int32.TryParse(entrada, out carta);
                }
            }
            else {
                var random = new Random();
                int index = random.Next(naipes.Count);
                carta = naipes[index];
                Console.Write("     Ingrese naipe:" + carta.ToString());
            }
            Console.ForegroundColor = ConsoleColor.White;
            return carta;
        }

        public override void cartaDelOponente(int carta) {
        }
    }
}
