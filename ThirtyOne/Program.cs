using System;
using System.IO;
using ThirtyOne.Helpers;
using ThirtyOne.Models;

namespace ThirtyOne
{
    class Program
    {
        const string GAMEFILENAME = "CurrentGAme.json";

        static void Main(string[] args)
        {
            //Initial test
            Deck deck = new Deck();
            deck.Initialize();
            Random randomNumberGenerator = new Random();
            deck.Shuffle(randomNumberGenerator);
            Card card = deck.DrawCard();

            //Game implementation
            Game game;
            ComputerPlayer computerPlayer;
            if (File.Exists(GAMEFILENAME))
            {
                //Continued game
                Console.WriteLine("Continued Game of 31!");
                game = Game.DeserializeGame(File.ReadAllText(GAMEFILENAME));
                computerPlayer = (ComputerPlayer)game.Players[0]; //Type casting

            } else
            {
                //New game
                Console.WriteLine("Let's play 31!");
                computerPlayer = new ComputerPlayer("Computer");
                game = new Game(randomNumberGenerator, computerPlayer, new ConsolePlayer("You"));
            }
            
           
            bool isGameOver = false;
            while (!isGameOver)
            {
                File.WriteAllText(GAMEFILENAME, game.SerializeGame()); // Save game state

                Console.WriteLine($"{game.CurrentPlayer.Name} turn!");
                isGameOver = game.NextTurn();
                Console.WriteLine($"{computerPlayer.Name} {computerPlayer.LastAction}");
            }

            File.Delete(GAMEFILENAME);

            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"--- GAME OVER, {game.Winner.Name} WON WITH {game.Winner.Hand.ToListString()} ---");
            Console.ReadLine();
        }
    }
}
