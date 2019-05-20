using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICombat {
    public class CashMachine {
        public Dictionary<int, int> CurrentBalance { get; private set; } = new Dictionary<int, int>();

        private readonly Dictionary<int, int> _initialBalance = new Dictionary<int, int> {
            {1,10},
            {5,10},
            {10,10},
            {20,10},
            {50,10},
            {100,10}
        }; 

        public CashMachine() {
            Restock();
        }

        public void Restock() {
            //future proof for new denominations
            CurrentBalance = new Dictionary<int, int>(_initialBalance).OrderByDescending(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
        }
        

        public string DisplayBalance() {
            StringBuilder sb = new StringBuilder("Machine Balance:\r\n");
            foreach (var bill in CurrentBalance) {
                sb.Append($"${bill.Key} - {bill.Value}\r\n");
            }
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        public Dictionary<int, int> DisplayDenominations(params int[] denominations) {
            StringBuilder sb = new StringBuilder();
            var denoms = new Dictionary<int, int>();
            foreach (var d in denominations) {
                int val;
                CurrentBalance.TryGetValue(d, out val);
                denoms[d] = val;
                sb.Append($"${d} - {val}\r\n");
            }
            Console.WriteLine(sb.ToString());
            return denoms;
        }
        
        //returns Dictionary with keys = denominations, values = number withdrawn.
        public Dictionary<int, int> Withdraw(int amount) {
            var balance = new Dictionary<int, int>(CurrentBalance);
            var requiredMoney = new Dictionary<int, int>();
            int sum = 0;
            foreach (var d in balance.Keys.ToList()) {
                requiredMoney[d] = 0;
                while (balance[d] > 0 && sum + d <= amount) {
                    requiredMoney[d] += 1;
                    balance[d] -= 1;
                    sum += d;
                }
            }

            if (sum == amount) {
                CurrentBalance = balance;
                Console.WriteLine("Success: Dispensed ${0}", sum);
                DisplayBalance();
            } else {
                Console.WriteLine("Failure: insufficient funds");
                return null;
            }

            return requiredMoney;
        }
    }
}