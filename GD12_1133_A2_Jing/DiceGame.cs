using Jing_Lab2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Jing_Lab3
{
    public class DiceGame
    {
        public Player player;
        public Player computer;
        public DieRoller dieRoller;
        public int playerTotalScore = 0;
        public int computerTotalScore = 0;
        public bool gameRunning = true;



        public void gameStart()
        {
            
            {
                Console.WriteLine(" \nHello!\n");
                

                Console.WriteLine("   -   -   -   -       -   -        -   -   -      ");
                Console.WriteLine("  / \\ / \\ / \\ / \\     / \\ / \\      / \\ / \\ / \\    ");
                Console.WriteLine(" [ d | i | c | e ]   [ o | r ]    [ d | i | e ]  ");
                Console.WriteLine("  \\ / \\ / \\ / \\ /     \\ / \\ /      \\ / \\ / \\ /    ");
                Console.WriteLine("   -   -   -   -       -   -        -   -   -   ");


               
                Console.WriteLine(" \nMay I ask your name?\n ");
                string playerName = Console.ReadLine();
                

                player = new Player(playerName);
                computer = new Player("computer");


                Console.WriteLine($" \nHi, {playerName}! Nice to see you here ^^\n ");
                Console.WriteLine("Welcome to the game of win or lose. Each of us will has 4 dice to use.");
                Console.WriteLine("The dice are: d6, d8, d12, d20");
                Console.WriteLine("You can choose wich dice you want to use, and then roll it!");
                Console.WriteLine("This is a round base dice game where you'll compete against me.");
                Console.WriteLine("This game is play over four round, after each round, we will compare the values of our rolls.");
                Console.WriteLine("Whoever gets the higher roll wins the round, and the winner scores points equal to the difference betewwn the two rolls.");
                Console.WriteLine("After all four rounds, we’ll compare our total scores. The player with the highest total score wins the game!");
                Console.WriteLine("Remember, once you use a die, you can’t use it again. So choose wisely!");
                Console.WriteLine(" \nDo you want to play dice or die? (yes or no)\n");
                string userInput = Console.ReadLine();

                
                

                if (userInput == "yes")
                {

                    gameRunning = true;
                    Console.WriteLine("\nGreat! Let's go!\n");
                    RunGame();

                }
                else
                {

                    gameRunning = false;
                    Console.WriteLine("Oh no");

                }

               

            }
        }
        public void RunGame()
        {
            
            player.sixUsed = false;
            player.eightUsed = false;
            player.twelveUsed = false;
            player.twentyUsed = false;

            computer.sixUsed = false;
            computer.eightUsed = false;
            computer.twelveUsed = false;
            computer.twentyUsed = false;

            
            playerTotalScore = 0;
            computerTotalScore = 0;

            dieRoller = new DieRoller();

            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine("\n*Round " + i + "*\n");
                PlayRound();
            }

            Outro();
        }

        public void PlayRound()
        {

            int playerDie = player.ChooseDie();
            int playerRollResult = dieRoller.Roll(playerDie);
            
            Console.WriteLine($"\nPlayer rolled a {playerDie}-side die and got: {playerRollResult}");

            int computerDie = computer.ComputerChooseDie();
            int computerRollResult = dieRoller.Roll(computerDie);
            Console.WriteLine($"Computer rolled a {computerDie}-sided die and got: {computerRollResult}");


            int roundDifference = playerRollResult - computerRollResult;
            Console.WriteLine($"\nRound result: Player {playerRollResult} vs Computer {computerRollResult}");


            if (roundDifference > 0)
            {

                playerTotalScore += roundDifference;

            }
            else if (roundDifference < 0)
            {

                computerTotalScore += Math.Abs(roundDifference);

            }


        }


        public void Outro()
        {

            Console.WriteLine("\nGame Over\n");
            Console.WriteLine($"Player total score: {playerTotalScore}");
            Console.WriteLine($"Computer total score: {computerTotalScore} \n");


            if (playerTotalScore > computerTotalScore)
            {

                Console.WriteLine("Player wins the game \n");

            }
            else if (playerTotalScore < computerTotalScore)
            {
                Console.WriteLine("Computer wins the game \n");

            }
            else
            {

                Console.WriteLine("It's a tie \n");

            }



            Console.WriteLine("\nDo you want to play again? (yes or no)\n ");
            string playAgainInput = Console.ReadLine();


            if (playAgainInput == "yes")
            {
                RunGame();
            }
            else 
            {
            
                Console.WriteLine("\nThanks for playing with me! See you next time!\n");
            
            }
        }

    }
}