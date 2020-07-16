
using System;

namespace juegoIA
{
    class Juego
    {
        public static void Main(string[] args) {
            {
                Game game = new Game();
                game.play();
                Console.ReadKey();
            }
        }
    }
}