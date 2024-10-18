using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jing_Lab3
{
    public class Player
    {

        public string Name;
        public int Die1;
        public int Die2;
        public int Die3;
        public int Die4;
      


        public bool sixUsed = false;
        public bool eightUsed = false;
        public bool twelveUsed = false;
        public bool twentyUsed = false;


        public Player(string playerName) 
        {
            Name = playerName;
            Die1 = 6;
            Die2 = 8;
            Die3 = 12;
            Die4 = 20;
        
        }

        public int ChooseDie() 
        {
            Console.WriteLine($"\n{Name}, please choose a die: 6, 8, 12 or 20\n");

            int chosenDie = int.Parse( Console.ReadLine() );

            if ( chosenDie == 6 && !sixUsed ) 
            {
            
                sixUsed = true;
                return Die1;
            
            }
            else if ( chosenDie == 8 && !eightUsed )
            {
            
                eightUsed = true;
                return Die2;
            
            }
            else if ( chosenDie == 12 && !twelveUsed ) 
            {

                twelveUsed = true;
                return Die3;
            
            }
            else if ( chosenDie == 20 && !twentyUsed ) 
            {
            
                twentyUsed = true;
                return Die4;
            
            }
            else 
            {

                Console.WriteLine("\nYou have alredy used this die. Please choose another.\n");
                return ChooseDie();
            
            }
      
        }

        public int ComputerChooseDie() 
        {
        
            Random rand = new Random();
            int[] diceOptions = { 6, 8, 12, 20 };

            while (true) 
            {
            
                int chosenDie = diceOptions[rand.Next(0, diceOptions.Length)];

                if ( chosenDie == 6 && !sixUsed) 
                {
                
                    sixUsed= true;
                    return Die1;
                
                }
                else if ( chosenDie == 8 && !eightUsed) 
                {
                
                    eightUsed = true;
                    return Die2;
                
                }
                else if ( chosenDie == 12 && !twelveUsed) 
                {
                
                    twelveUsed= true;
                    return Die3;
                
                }
                else if ( chosenDie == 20 && !twentyUsed) 
                {
                
                    twentyUsed= true;
                    return Die4;
                
                }

            }


        }

    }
}
