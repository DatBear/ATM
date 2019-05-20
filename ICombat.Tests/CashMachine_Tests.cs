using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICombat.Tests {
    [TestClass]
    public class CashMachineTest {

        [TestMethod]
        public void Withdraw_WithValidAmount_UpdatesBalance() {
            var machine = new CashMachine();
            machine.Withdraw(208);

            Assert.AreEqual(machine.CurrentBalance[100], 8);
            Assert.AreEqual(machine.CurrentBalance[5], 9);
            Assert.AreEqual(machine.CurrentBalance[1], 7);
        }

        [TestMethod]
        public void Withdraw_WithInvalidAmount_DoesntUpdateBalance() {
            var machine = new CashMachine();
            machine.Withdraw(2000);
            Assert.AreEqual(machine.CurrentBalance[100], 10);
            Assert.AreEqual(machine.CurrentBalance[50], 10);
            Assert.AreEqual(machine.CurrentBalance[20], 10);
            Assert.AreEqual(machine.CurrentBalance[10], 10);
            Assert.AreEqual(machine.CurrentBalance[5], 10);
            Assert.AreEqual(machine.CurrentBalance[1], 10);

        }

        [TestMethod]
        public void Withdraw_WithNegativeAmount_DoesntUpdateBalance() {
            var machine = new CashMachine();
            machine.Withdraw(-100);
            Assert.AreEqual(machine.CurrentBalance[100], 10);
            Assert.AreEqual(machine.CurrentBalance[50], 10);
            Assert.AreEqual(machine.CurrentBalance[20], 10);
            Assert.AreEqual(machine.CurrentBalance[10], 10);
            Assert.AreEqual(machine.CurrentBalance[5], 10);
            Assert.AreEqual(machine.CurrentBalance[1], 10);

        }

        [TestMethod]
        public void Restock_ResetsBalance() {
            var machine = new CashMachine();

            machine.Withdraw(123);

            machine.Restock();
            Assert.AreEqual(machine.CurrentBalance[100], 10);
            Assert.AreEqual(machine.CurrentBalance[50], 10);
            Assert.AreEqual(machine.CurrentBalance[20], 10);
            Assert.AreEqual(machine.CurrentBalance[10], 10);
            Assert.AreEqual(machine.CurrentBalance[5], 10);
            Assert.AreEqual(machine.CurrentBalance[1], 10);
        }

        [TestMethod]
        public void DisplayDenominations_ReturnsCorrectDenominations() {
            var machine = new CashMachine();

            machine.Withdraw(123);
            
            var denominations = machine.DisplayDenominations(100, 20, 1);
            Assert.AreEqual(denominations[100], 9);
            Assert.AreEqual(denominations[20], 9);
            Assert.AreEqual(denominations[1], 7);
        }

    }
}