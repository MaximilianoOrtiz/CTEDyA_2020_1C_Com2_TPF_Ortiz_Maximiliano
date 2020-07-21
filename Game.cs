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
            menuPrincipal();
            string opcion = Console.ReadLine(), cantidadDeOpciones = "3", opcionSalir = cantidadDeOpciones + 1;

            while (int.Parse(opcion) != int.Parse(cantidadDeOpciones) + 1) {

                switch (opcion) {
                    case "1": {
                            Console.Clear();
                            titulo();
                            Console.WriteLine(" ");
                            Console.WriteLine("------------------------JUEGO INICIADO----------------------- ");
                            Console.WriteLine(" ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Recuerde: Usted podra optar por las siguientes opciones en cualquier momento, ");
                            Console.WriteLine("           antes de su turno. ");
                            Console.WriteLine(" ");
                            menuAlternativo();
                            Console.WriteLine(" ");
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.ForegroundColor = ConsoleColor.White;

                            while (!this.fin()) {
                                Console.WriteLine(" ");
                                this.printScreen();
                                this.turn(player2, player1, naipesHuman); // Juega el usuario
                                if (!this.fin()) {
                                    this.printScreen();
                                    this.turn(player1, player2, naipesComputer); // Juega la IA
                                }
                            }
                            Console.WriteLine();
                            Console.WriteLine(" ");
                            this.printWinner();
                            break;
                        }
                    case "2": {
                            Console.Clear();
                            Game game = new Game();
                            game.play();
                            break;
                        }
                    case "3": {
                            Console.Clear();
                            titulo();
                            ayuda();
                            break;
                        }
                }

                Console.Clear();
                titulo();
                menuPrincipal();
                opcion = (Console.ReadLine());

            }
        }

        /*
         * Imprime por pantalla el limite del monticulo
         * **/
        private void printScreen() {
            Console.WriteLine();
            Console.WriteLine("                     Limite:" + limite.ToString());
        }

        private void turn(Jugador jugador, Jugador oponente, List<int> naipes) {

            if (jugador is HumanPlayer) {
                menuAlternativo();
                Console.Write("Ingrese opcion: ");
                string opcion = Console.ReadLine();
                Console.Write("");
                while (opcion != "5") {
                    switch (opcion) {
                        case "1": {
                                Console.Clear();
                                Game game = new Game();
                                game.play();
                                break;
                            }
                        case "2": {
                                imprimirJugadasDeJugadaActual();
                                break;
                            }
                        case "3": {
                                Console.WriteLine("");
                                imprimirJugadasAPartirDePosiblesJugadas(ingresarJugada());
                                break;
                            }
                        case "4": {
                                imprimirJugadasDadaUnaProfundidad();

                                break;
                            }
                    }
                    if (opcion == "" | int.Parse(opcion) > 5) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Opcion . Ingrese nuevamente: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        opcion = Console.ReadLine();
                    }
                    else {
                        menuAlternativo();
                        Console.WriteLine("");
                        Console.Write("Ingrese opcion: ");
                        Console.Write("");
                        opcion = Console.ReadLine();
                    }
                }
            }
            Console.WriteLine("");
            int carta = jugador.descartarUnaCarta();
            naipes.Remove(carta);
            limite -= carta;
            oponente.cartaDelOponente(carta);
            juegaHumano = !juegaHumano;



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

                // Console.WriteLine("Gano Computer");
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

        private static void menuPrincipal() {

            Console.Clear();
            titulo();
            Console.WriteLine(" ");
            Console.WriteLine("1) Iniciar");
            Console.WriteLine("2) Nueva Partida");
            Console.WriteLine("3) Ayuda");
            Console.WriteLine("4) Salir");
            Console.WriteLine(" ");
        }

        private void menuAlternativo() {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("1) Nueva Partida                                            -");
            Console.WriteLine("--2) Posibles Resultados desde el punto actual              -");
            Console.WriteLine("----3) Posibles Resultados dado un conjunto de jugadas      -");
            Console.WriteLine("------4) Posibles jugadas de una profundidad  dada          -");
            Console.WriteLine("                                                            -");
            Console.WriteLine("5) Contiuar...                                              -");
            Console.WriteLine("-------------------------------------------------------------");


            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ayuda() {
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
            Console.WriteLine("RECUERDE QUE USTED TENDRA ACSESO A LAS SIGUENTES OPCIONES EN CUARQUIEL MOMENTO DEL JUGEO");
            Console.WriteLine("");
            menuAlternativo();
            Console.ReadKey();
        }
        public void imprimirCartar(List<int> naipes) {
            foreach (var naipe in naipes)
                Console.Write(naipe + ", ");
        }

        private List<int> ingresarJugada() {
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


        private void imprimirJugadasDeJugadaActual() {

            ((ComputerPlayer)player1).imprimirJugadasDeJugadaActual(((ArbolGeneral<Naipe>)(((ComputerPlayer)(player1)).getJugadaActual())), new List<ArbolGeneral<Naipe>>(), true);
        }

        private void imprimirJugadasAPartirDePosiblesJugadas(List<int> posiblesjugadas) {

            ((ComputerPlayer)player1).imprimirJugadasAPartirDePosiblesJugadas(posiblesjugadas);
        }


        private void imprimirJugadasDadaUnaProfundidad() {
            ((ComputerPlayer)player1).getJugadaActual().imprimirnivelcompleto(obtenerProfundidad());
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

            return int.Parse(Console.ReadLine());
        }
    }
}
