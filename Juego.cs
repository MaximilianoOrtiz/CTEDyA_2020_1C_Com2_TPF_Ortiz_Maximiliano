
using System;
using System.Collections.Generic;

namespace juegoIA
{
    public class Juego
    {
        private static Game game;

        public static void Main(string[] args) {
            {

                run();
                //Game game = new Game();
                //game.play();

            }
        }
        public static void run() {

            titulo();
            menuPrincipal();
            string opcion = Console.ReadLine();

            while (opcion != "3") {
                try {
                    switch (opcion) {
                        case "1": {
                                Console.Clear();
                                game = new Game();
                                game.play();
                                break;
                            }
                        case "2": {
                                Console.Clear();
                                titulo();
                                ayuda();
                                break;
                            }
                    }
                }
                catch (Exception) {

                }
                Console.Clear();
                titulo();
                menuPrincipal();
                opcion = (Console.ReadLine());

            }

        }

        private static void titulo() {
            Console.Write("********************************************************************************");
            Console.Write("*****                          JUEGO MINIMAX                               *****");
            Console.Write("********************************************************************************");
        }

        private static void menuPrincipal() {

            Console.Clear();
            titulo();
            Console.WriteLine(" ");
            Console.WriteLine("1) Nueva Partida");
            Console.WriteLine("2) Ayuda");
            Console.WriteLine("3) Salir");
            Console.WriteLine(" ");
        }
        private static void menuAlternativo() {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("1) Menu principal                                       -");
            Console.WriteLine("--2) Posibles Resultados desde el punto actual         -");
            Console.WriteLine("----3) Posibles Resultados dado un conjunto de jugadas -");
            Console.WriteLine("------4) Posibles jugadas de una profundidad  dada     -");
            Console.WriteLine("--------------------------------------------------------");
            Console.Write("");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ayuda() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("");
            Console.WriteLine("Bienvenido al juego basado en el teorema MiniMax de John von Neumann");
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("--------------------------REGLAS-------------------------------------");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("1. Se dispone de un mazo con 12 cartas numeradas de 1 a 12 que se reparten en cantidades iguales y de manera aleatoria entre 2 jugadores.");
            Console.WriteLine("");
            Console.WriteLine("2. El juego fija un límite máximo del cual los jugadores no pueden pasarse.");
            Console.WriteLine("");
            Console.WriteLine("3. Los jugadores juegan una vez por turno y en cada uno se tiene que descartar una carta.");
            Console.WriteLine("");
            Console.WriteLine("4. El descarte va formando un montículo cuyo valor es la suma de las cartas que lo integran. El montículo de descarte inicialmente está vacío y su valor es 0.");
            Console.WriteLine("");
            Console.WriteLine("5. El jugador que incorpore la carta al montículo que haga que el valor del mismo supere el límite fijado es aquel que pierde el juego.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("RECUERDE QUE USTED TENDRA ACCESO A LAS SIGUENTES OPCIONES EN CUALQUIER MOMENTO DEL JUEGO");
            Console.WriteLine("");
            menuAlternativo();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("presione ENTER  para volver al menu principal");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
        private static List<int> ingresarJugada() {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Por favor, ingrese secuencia de cartas en el siguiente formato y al terminar ingres 'n'");
            Console.WriteLine("      ");
            Console.WriteLine("               *posible carta Humano.     ");
            Console.WriteLine("               *posible carta Inteligencia Artificial.     ");


            List<int> posiblesjugada = new List<int>();
            Console.WriteLine(" ");
            Console.Write("Carta: ");
            Console.ForegroundColor = ConsoleColor.White;
            string ingreso = Console.ReadLine();
            while (ingreso != "n") {
                int ingresoaux = int.Parse(ingreso);
                posiblesjugada.Add(ingresoaux);
                Console.Write("Carta: ");
                ingreso = Console.ReadLine();
            }
            return posiblesjugada;
        }
    }
}