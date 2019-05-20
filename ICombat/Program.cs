using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ICombat {
    internal class Program {
        private static void Main(string[] args) {
            var machine = new CashMachine();

            machine.DisplayBalance();

            bool running = true;
            while (running) {
                var input = Console.ReadLine().Replace("$", string.Empty);
                var command = input.Split(' ')[0];
                try {
                    switch (command) {
                        case "R":
                            machine.Restock();
                            machine.DisplayBalance();
                            break;
                        case "W":
                            var wArg = int.Parse(input.Split(' ')[1]);
                            machine.Withdraw(wArg);
                            break;
                        case "I":
                            var iArg = Array.ConvertAll(input.Split(' ').Skip(1).ToArray(), int.Parse);
                            machine.DisplayDenominations(iArg);
                            break;
                        case "Q":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Command");
                            break;
                    }
                }
                catch (Exception) {
                    //normally would catch exceptions in each command if more detailed error output is needed.
                    Console.WriteLine("Invalid Command");
                }
            }
        }
    }
}