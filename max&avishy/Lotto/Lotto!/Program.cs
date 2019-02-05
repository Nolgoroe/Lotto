using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotto__Final
{
    class Program
    {
        static void GenerateRandomSystemNumber(int[] SystemArray)
        {
            Random SystemNumber = new Random();
            // Random generated System numbers
            for (int i = 0; i < SystemArray.Length; i++)
            {
                SystemArray[i] = SystemNumber.Next(1, 47);
                for (int j = 0; j < i; j++)
                {
                    // checking for repeating System numbers and fixing them
                    if (SystemArray[i] == SystemArray[j])
                    {
                        i--;
                        break;
                    }
                }
            }
        }
        static void WriteRules(string rules)
        {
            Console.WriteLine("Welcome to the LOTTERY game!\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Rules for the Lottery game:");
            Console.SetCursorPosition(50, 1);
            Console.WriteLine("1. You must enter 6 numbers between 1-46");
            Console.SetCursorPosition(50, 2);
            Console.WriteLine("2. You cannot input repeating numbers");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("3. You cannot enter letter or words");
            Console.ResetColor();
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Please enter 6 numbers between 1-46");
        }
        static int[] WritePlayerNumbers(int[] PlayerArray)
        {
            string PlayerNumberLine;
            int PlayerNumberCounter = 0, PlayerNumber;
            bool InputPlayerValid;


            for (int p = 0; p < PlayerArray.Length; p++)
            {
                if (PlayerNumberCounter < 6)
                {
                    PlayerNumberLine = Console.ReadLine();
                    InputPlayerValid = int.TryParse(PlayerNumberLine, out PlayerNumber);
                    if ((InputPlayerValid) && (PlayerNumber > 0) && (PlayerNumber < 47))
                    {
                        PlayerArray[p] = PlayerNumber;
                        PlayerNumberCounter++;
                    }

                    // Player Input was Incorrect
                    while (!InputPlayerValid || PlayerNumber <= 0 || PlayerNumber >= 47)
                    {
                        if ((InputPlayerValid) && (PlayerNumber <= 0))
                        {
                            Console.WriteLine("Number is too Low, please try again.");
                            PlayerNumberLine = Console.ReadLine();
                            InputPlayerValid = int.TryParse(PlayerNumberLine, out PlayerNumber);
                        }
                        else if ((InputPlayerValid) && (PlayerNumber >= 47))
                        {
                            Console.WriteLine("Number is too High, please try again.");
                            PlayerNumberLine = Console.ReadLine();
                            InputPlayerValid = int.TryParse(PlayerNumberLine, out PlayerNumber);

                        }
                        else if (!InputPlayerValid)
                        {
                            Console.WriteLine("Youre input was not correct, please try again.");
                            PlayerNumberLine = Console.ReadLine();
                            InputPlayerValid = int.TryParse(PlayerNumberLine, out PlayerNumber);
                        }


                        // if player input is correct
                        if ((InputPlayerValid) && (PlayerNumber > 0) && (PlayerNumber < 47))
                        {
                            PlayerArray[p] = PlayerNumber;
                            PlayerNumberCounter++;
                        }
                    }

                    // checking for repeating Player Numbers and fixing them
                    for (int g = 0; g < p; g++)
                    {
                        if (PlayerArray[p] == PlayerArray[g])
                        {
                            Console.WriteLine("You already chose this number, try again.");
                            p--;
                            PlayerNumberCounter--;
                            break;
                        }
                    }
                }
            }
            return PlayerArray;
        }
        static int CaculateHitsOrMisses(int[] PlayerArray, int[] SystemArray)
        {
            int SumHitNumbers = 0, IsEqualCounter = 0;
            while (IsEqualCounter < 6)
            {
                for (int k = 0; k < 6; k++)
                {
                    for (int o = 0; o < 6; o++)
                    {
                        if (PlayerArray[o] == SystemArray[k])
                        {
                            SumHitNumbers = SumHitNumbers + 1;
                        }
                    }
                    IsEqualCounter++;
                }

                // showing how many numbers were a "HIT" as Green
                if ((SumHitNumbers > 0) && (SumHitNumbers < 6))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("you got {0} numbers right!", SumHitNumbers);
                    Console.ResetColor();
                }
                // showing all "HIT" on numbers as Yellow
                else if (SumHitNumbers == 6)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("WOW you got All Of Them RIGHT,Youre AMAZING!!!");
                    Console.ResetColor();
                }
                // showing all "MISS" on numbers as Red
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Looks like you missed them all.. better luck next time!");
                    Console.ResetColor();
                }
            }
            return IsEqualCounter;
        }
        static void DisplayResults(int[] SystemArray,int[] PlayerArray)
        {
            Console.WriteLine("The lottery numbers were [{0}] ", string.Join(",", SystemArray));
            Console.WriteLine("Your numbers were [{0}]", string.Join(",", PlayerArray));
        }
        static bool ChoseRestartGame(bool IsInputValid, bool restart = false)
        {
            string line;
            string rules = "temp";
            int[] SystemArray = new int[6];
            int[] PlayerArray = new int[6];
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("You want play again ? enter Y or N");
            Console.ResetColor();
            line = Console.ReadLine();
            IsInputValid = (line == "Y" || line == "y") || (line == "N" || line == "n");
            while (!IsInputValid)
            {
                Console.WriteLine("Input was not correct, please enter Y for Yes or N for No");
                line = Console.ReadLine();
                IsInputValid = (line == "Y" || line == "y") || (line == "N" || line == "n");
            }

            //if(line.toLOWER() == 'y')
            if (line == "Y" || line == "y")
            {
                restart = true;
                Console.Clear();
                WriteRules(rules);
                GenerateRandomSystemNumber(SystemArray);
                WritePlayerNumbers(PlayerArray);
                CaculateHitsOrMisses(PlayerArray, SystemArray);
                DisplayResults(SystemArray,PlayerArray);
                ChoseRestartGame(restart);
            }
            else
            {
                restart = false;
                Console.WriteLine("Thanks for playing");
            }
            return restart;
        }
        static void Main(string[] args)
        {
            string rules = "temp";
            bool restart = false;
            int[] SystemArray = new int[6];
            int[] PlayerArray = new int[6];
            do
            {
                Console.WindowWidth = 125;
                WriteRules(rules);
                GenerateRandomSystemNumber(SystemArray);
                WritePlayerNumbers(PlayerArray);
                CaculateHitsOrMisses(PlayerArray, SystemArray);
                DisplayResults(SystemArray,PlayerArray);
                ChoseRestartGame(restart);
            } while (restart == true);
        }
    }
}
