using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jing_Lab3;

namespace GD12_1133_A2_Jing
{
    public abstract class Room
    {
        // Name of the room
        public string Name { get; private set; }

        // Dierctions to other rooms
        public Room North { get; set; }
        public Room South { get; set; }
        public Room East { get; set; }
        public Room West { get; set; }

        // Obstacless in the room and the required item to pass them
        private Dictionary<string, (string Obstacle, string RequiredItem)> obstacles = new Dictionary<string, (string, string)>();


        // Set the room name
        protected Room(string name)
        {
            Name = name;
        }


        // Add an obstacle to a direction that requires an tiem to bypass
        public void AddObstacle(string direction, string obstacle, string requiredItem)
        {
            obstacles[direction] = (obstacle, requiredItem);
        }


        // When player enter a room 
        public virtual void OnRoomEntered(List<string> inventory)
        {
            Console.WriteLine($"\nYou have entered the {Name}.\n");
        }


        // When player leaves a room
        public virtual void OnRoomExit() { }

        
        // Get the availabe directions the player can move
        public string GetAvailableDirections()
        {
            List<string> directions = new List<string>();
            if (North != null) directions.Add("north");
            if (South != null) directions.Add("south");
            if (East != null) directions.Add("east");
            if (West != null) directions.Add("west");
            return $"({string.Join(", ", directions)})";
        }


        
        public virtual Room GetRoomInDirection(string direction, List<string> inventory)          // Get the room in the chosen direction, checking for obstacles
        {
            if (obstacles.ContainsKey(direction))
            {
                var obstacle = obstacles[direction];

                string obstacleMessage = obstacle.Obstacle switch                                 // Show different messages based on the type of obstacle
                {
                    "Ivy" => "The door is blocked by ivy. You need something to cut it.",
                    "Broke" => "The door is broken. You need something to fix it.",
                    "Locked" => "The door is locked. You need a key to unlock it.",
                    _ => $"The path is blocked by {obstacle.Obstacle}. You need something to pass."
                };

               
                if (!inventory.Contains(obstacle.RequiredItem))                                   // If player doesn't have the required item, show the obstacle message
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{obstacleMessage}\n");
                    Console.ResetColor();
                    return null;
                }
                else
                {
                    string removeMessage = obstacle.RequiredItem switch                           // If player removed the obstacle, show message
                    {
                        "Sword" => "You use the sword to cut the ivy.",
                        "Wood" => "You use the wood to fix the broken door.",
                        "Key" => "You use the key to unlock the door.",
                        _ => $"You use the {obstacle.RequiredItem} to bypass the {obstacle.Obstacle}."
                    };
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\n{removeMessage}\n");
                    Console.ResetColor();
                }
            }

            return direction switch                              // Return the room in the chosen direction
            {
                "north" => North,
                "south" => South,
                "east" => East,
                "west" => West,
                _ => null
            };
            
        }
    }

    // Derived classes for specific rooms
    public class EntranceHall : Room
    {
        public EntranceHall() : base("Entrance Hall") { }

        public override void OnRoomEntered(List<string> inventory)
        {
            base.OnRoomEntered(inventory);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   It's the grand entrance to the castle.\n");
            Console.ResetColor();
        }
    }

    public class GuestRoom : Room
    {
        public GuestRoom() : base("Guest Room") { }

        public override void OnRoomEntered(List<string> inventory)
        {
            base.OnRoomEntered(inventory);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   A cozy room with old furniture.\n");
            Console.ResetColor();
        }
    }

    public class SmallBackyard : Room
    {
        public SmallBackyard() : base("Small Backyard") { }

        public override void OnRoomEntered(List<string> inventory)
        {
            base.OnRoomEntered(inventory);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   A quiet place behind the castle with an ancient tree.\n");
            Console.ResetColor();

            if (inventory.Contains("Ax") && !inventory.Contains("Wood"))        // If the player has an ax but hasn't collected wood yet, they can chop the tree
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   You use the ax to cut the tree and collect some wood.\n");
                Console.ResetColor();
                inventory.Add("Wood");
            }
            else if (!inventory.Contains("Ax"))                                 // If thet don't have ax
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("   The tree seems sturdy. You need somthing to chop it.\n");
                Console.ResetColor();
            }
        }
    }

    public class WeaponRoom : Room
    {
        public WeaponRoom() : base("Weapon Room") { }

        public override void OnRoomEntered(List<string> inventory)
        {
            base.OnRoomEntered(inventory);
            if (!inventory.Contains("Sword"))      // If the player hasn't picked up the sword yet
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   You found a sword.\n");
                Console.ResetColor();
                inventory.Add("Sword");
            }
            else                                   // If the player already had sword
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("   You have already collected the sword here.\n");
                Console.ResetColor();
            }
        }
    }



    public class GardenRoom : Room
    {
        public GardenRoom() : base("Garden") { }

        public override void OnRoomEntered(List<string> inventory)
        {
            base.OnRoomEntered(inventory);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   A lush garden filled with beautiful plants. A fairy appears.\n");
            Console.ResetColor();

            if (!inventory.Contains("Dice"))                 // If player doesn't have the dice yet, thet can't play with the fairy
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("   The fairy says, 'If you have a dice, we can play a game. If you win, I will give you a mysterious item.'\n");
                Console.ResetColor();
            }
            else if (inventory.Contains("Dice") && !inventory.Contains("Carrot"))     // If player had dice, the dice game will start
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("   The fairy says, 'Let's play!\n'");
                Console.ResetColor();
                DiceGame gameManager = new DiceGame();  
                gameManager.gameStart();

                if (gameManager.playerTotalScore > gameManager.computerTotalScore)    // If player wins the dice game, thet get a carrot
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("   You won! The fairy gives you an Carrot.\n");
                    Console.ResetColor();
                    inventory.Add("Carrot");
                    
                }
                else                                                                  // If player lose the game
                {
                    Console.ForegroundColor = ConsoleColor.Blue;                              
                    Console.WriteLine("   You lost the game. The fairy disappears.\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;                          // If player alredy had key
                Console.WriteLine("   The fairy says, 'You already have my gift. Go on your way.'\n");
                Console.ResetColor();
            }
        }
    }


    public class StorageRoom : Room
    {
        public StorageRoom() : base("Storage Room") { }

        public override void OnRoomEntered(List<string> inventory)      // When the player enters the storage room
        {
            base.OnRoomEntered(inventory);
            if (!inventory.Contains("Ax"))                              // If the player hasn't collected the ax yet, they find it here
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   You found an ax.\n");
                Console.ResetColor();
                inventory.Add("Ax");
            }
            else                                                        // If player alredy has the ax, there is nothing eles here
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("   There’s nothing more of use here.\n");
                Console.ResetColor();
            }
        }
    }

    public class EntertainmentRoom : Room
    {
        public EntertainmentRoom() : base("Entertainment Room") { }

        public override void OnRoomEntered(List<string> inventory)     // If the player enters the entertainment room
        {
            base.OnRoomEntered(inventory);
            if (!inventory.Contains("Dice"))                           // If the player hasn't collected the dice yet, they find it here
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   You found a dice.\n");
                Console.ResetColor();
                inventory.Add("Dice");
            }
            else                                                       // If player already has the dice, there's nothing else here
            {
                Console.WriteLine("   There’s nothing more of use here.\n");
            }
        }
    }

    public class ArtRoom : Room
    {
        public ArtRoom() : base("Art Room") { }

        public override void OnRoomEntered(List<string> inventory)     // When the player enters the art room
        {
            base.OnRoomEntered(inventory);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   A room filled with paintings. You see a bunny here.\n");
            Console.ResetColor();

            if (!inventory.Contains("Carrot"))                                     // If player doesn't have a carrot, the bunny just hops away
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("   The bunny sniffs you and hops away.\n");
                Console.ResetColor();
            }
            else if (inventory.Contains("Carrot") && !inventory.Contains("Key"))   // If the player has a carrot but doesn't have the key, the bunny gives them a key
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   The bunny eats your Carrot and gives you a key.\n");
                Console.ResetColor();
                inventory.Remove("Carrot");
                inventory.Add("Key");
            }
            else                                                                   // If the player already has the key, the bunny has nothing more to give
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("   The bunny looks happy and has nothing more to give (only the poop).\n");
                Console.ResetColor();
            }
        }
    }

    public class Cellar : Room
    {
        public Cellar() : base("Cellar") { }

        public override void OnRoomEntered(List<string> inventory)    // When the player enters the cellar
        {
            base.OnRoomEntered(inventory);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nYou have found a treasure! Congratulations!\n");
            Console.ResetColor();

            
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("You have reached the end of your adventure.\nThank you for playing!");     // End the game
            Console.ResetColor();
            Environment.Exit(0); // Exit the application
        }
    }
}
