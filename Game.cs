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

        public Game() {
            inicializarJuego();
        }

        private void inicializarJuego() {
            var rnd = new Random();
            limite = rnd.Next(LOWER, UPPER);

            naipesHuman = Enumerable.Range(1, WIDTH).OrderBy(x => rnd.Next()).Take(WIDTH / 2).ToList();

            for (int i = 1; i <= WIDTH; i++) {
                if (!naipesHuman.Contains(i)) {
                    naipesComputer.Add(i);
                }
            }
            player1.incializar(naipesComputer, naipesHuman, limite);
            player2.incializar(naipesHuman, naipesComputer, limite);

        }

        public void play() {
            titulo();
            Console.WriteLine(" ");
            Console.WriteLine("----------------------------JUEGO INICIADO---------------------------- ");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            imprimirCartasDeHUmanoYIntArt();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");

            while (!this.fin()) {
                Console.WriteLine(" ");
                this.printScreen();
                Console.WriteLine("----------------------------------------------------------------------------");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Presione Enter Para continuar");
                Console.ForegroundColor = ConsoleColor.White;
                this.turn(player2, player1, naipesHuman); // Juega el usuario
                if (!this.fin()) {
                    this.printScreen();
                    this.turn(player1, player2, naipesComputer); // Juega la IA
                }
            }
            Console.WriteLine();
            Console.WriteLine(" ");
            this.printWinner();
        }

        /*
         * Imprime por pantalla el limite del monticulo
         * **/
        private void printScreen() {
            Console.WriteLine();
            Console.WriteLine("Limite:" + limite.ToString());
        }

        private void turn(Jugador jugador, Jugador oponente, List<int> naipes) {

            if (jugador is HumanPlayer) {

                Console.ReadKey();
                Console.Clear();
                titulo();
                menuAlternativo();
                Console.Write("");
                Console.Write("");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Ingrese opcion o ENTER para continuar:  ");
                Console.ForegroundColor = ConsoleColor.White;
                string opcion = Console.ReadLine();
                Console.Write("");
                while (opcion != "") {
                    switch (opcion) {
                        case "1": {
                                throw new Exception();
                            }
                        case "2": {
                                imprimirJugadasDeJugadaActual();
                                Console.ReadKey();
                                break;
                            }
                        case "3": {
                                Console.WriteLine("");
                                imprimirJugadasAPartirDePosiblesJugadas(ingresarJugada());
                                Console.ReadKey();
                                break;
                            }
                        case "4": {
                                imprimirJugadasDadaUnaProfundidad();
                                Console.ReadKey();
                                break;
                            }
                    }
                    if (int.Parse(opcion) > 5) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Opcion . Ingrese nuevamente: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        opcion = Console.ReadLine();
                    }
                    else {
                        Console.Clear();
                        titulo();
                        menuAlternativo();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Ingrese opcion o ENTER para continuar:  ");
                        Console.ForegroundColor = ConsoleColor.White;
                        opcion = Console.ReadLine();
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Limite: " + limite);
            }
            Console.WriteLine("----------------------------------------------------------------------------");
            int carta = jugador.descartarUnaCarta();
            naipes.Remove(carta);
            limite -= carta;
            oponente.cartaDelOponente(carta);
            juegaHumano = !juegaHumano;
            


        }

        private bool opcionValida(string opcion) {
            if (opcion == "") {
                return false;
            }
            if (opcion == "5")
                return false;
            return true;
        }

        private void printWinner() {
            if (!juegaHumano) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("********************************************************************************");
                Console.WriteLine("------------               FELICIDADES HAS GANADO!!               --------------");
                Console.WriteLine("********************************************************************************");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();

            }
            else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("********************************************************************************");
                Console.WriteLine("------------                      GANO COMPUTER!!                   ------------");
                Console.WriteLine("********************************************************************************");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();

            }
        }

        private bool fin() {
            return limite < 0;
        }

        private static void titulo() {
            Console.Write("********************************************************************************");
            Console.Write("*****                          JUEGO MINIMAX                               *****");
            Console.Write("********************************************************************************");
        }

        private void menuAlternativo() {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("1) Menu Principal                                      -");
            Console.WriteLine("--2) Posibles Resultados desde el punto actual         -");
            Console.WriteLine("----3) Posibles Resultados dado un conjunto de jugadas -");
            Console.WriteLine("------4) Posibles jugadas de una profundidad  dada     -");
            Console.WriteLine("--------------------------------------------------------");
            Console.Write("");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void imprimirCartasDeHUmanoYIntArt() {
            Console.Write("Naipes IA: ");
            imprimirCartar(naipesComputer);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("Naipes Humano: ");
            imprimirCartar(naipesHuman);
        }

        private void imprimirCartar(List<int> naipes) {
            foreach (var naipe in naipes)
                Console.Write(naipe + ", ");
        }

        private List<int> ingresarJugada() {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Por favor, ingrese secuencia de cartas en el siguiente formato y al terminar ingres 'n'");
            Console.WriteLine("");
            Console.WriteLine("               *posible carta Humano.     ");
            Console.WriteLine("               *posible carta Inteligencia Artificial.     ");
            Console.WriteLine("");

            imprimirCartasDeHUmanoYIntArt();
            Console.WriteLine("");

            List<int> posiblesjugada = new List<int>();
            Console.WriteLine(" ");
            Console.Write("Carta: ");
            Console.ForegroundColor = ConsoleColor.White;
            string ingreso = Console.ReadLine();
            while (ingreso != "") {
                int ingresoaux = int.Parse(ingreso);
                posiblesjugada.Add(ingresoaux);
                Console.Write("Carta: ");
                ingreso = Console.ReadLine();

            }
            return posiblesjugada;
        }


        private void imprimirJugadasDeJugadaActual() {

            ((ComputerPlayer)player1).imprimirJugadasDeJugadaActual(((ArbolGeneral<Naipe>)(((ComputerPlayer)(player1)).getJugadaActual())), new List<ArbolGeneral<Naipe>>(), false);
        }

        private void imprimirJugadasAPartirDePosiblesJugadas(List<int> posiblesjugadas) {

            ((ComputerPlayer)player1).imprimirJugadasAPartirDePosiblesJugadas(posiblesjugadas);
        }


        private void imprimirJugadasDadaUnaProfundidad() {

            ((ComputerPlayer)player1).getJugadaActual().imprimirnivelcompleto((obtenerProfundidad()));
        }

        private int obtenerProfundidad() {
            ArbolGeneral<Naipe> jugadaActual = ((ComputerPlayer)player1).getJugadaActual();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            Console.WriteLine("La altura actual  del arbol es: " + jugadaActual.altura());
            Console.WriteLine();
            Console.WriteLine("Aclaracion: ------> PROFUNDIDAD IMPARES : jugadas Humano ");
            Console.WriteLine("            ------> PROFUNDIDAD PARES: jugadas Inteligencia Artificial");
            Console.WriteLine();

            Console.Write("Que profundidad desea imprimir:  ");
            Console.ForegroundColor = ConsoleColor.White;
            int profundidad = 0;
            string opcion = Console.ReadLine();
            while (opcion == "") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Opcion Invalida. Ingrese Profundida: ");
                Console.ForegroundColor = ConsoleColor.White;
                opcion = Console.ReadLine();
            }
            return profundidad = int.Parse(opcion);

        }
    }
}
