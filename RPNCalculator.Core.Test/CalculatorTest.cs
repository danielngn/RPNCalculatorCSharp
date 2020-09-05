using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPNCalculator.Core.Test
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void TestCalculatorExample1()
        {
            var c = new Calculator();            
            Assert.AreEqual("stack: 5 2", c.ProcessInput("5 2"));
        }

        [TestMethod]
        public void TestCalculatorExample2()
        {
            var c = new Calculator();            
            Assert.AreEqual("stack: 1.4142135624", c.ProcessInput("2 sqrt"));            
            Assert.AreEqual("stack: 3", c.ProcessInput("clear 9 sqrt"));
        }

        [TestMethod]
        public void TestCalculatorExample3()
        {
            var c = new Calculator();            
            Assert.AreEqual("stack: 3", c.ProcessInput("5 2 -"));            
            Assert.AreEqual("stack: 0", c.ProcessInput("3 -"));            
            Assert.AreEqual("stack: ", c.ProcessInput("clear"));
        }

        [TestMethod]
        public void TestCalculatorExample4()
        {
            var c = new Calculator();
            Assert.AreEqual("stack: 5 4 3 2", c.ProcessInput("5 4 3 2"));            
            Assert.AreEqual("stack: 20", c.ProcessInput("undo undo *"));            
            Assert.AreEqual("stack: 100", c.ProcessInput("5 *"));            
            Assert.AreEqual("stack: 20 5", c.ProcessInput("undo"));
        }

        [TestMethod]
        public void TestCalculatorExample5()
        {
            var c = new Calculator();            
            Assert.AreEqual("stack: 7 6", c.ProcessInput("7 12 2 /"));            
            Assert.AreEqual("stack: 42", c.ProcessInput("*"));            
            Assert.AreEqual("stack: 10.5", c.ProcessInput("4 /"));
        }

        [TestMethod]
        public void TestCalculatorExample6()
        {
            var c = new Calculator();            
            Assert.AreEqual("stack: 1 2 3 4 5", c.ProcessInput("1 2 3 4 5"));            
            Assert.AreEqual("stack: 1 2 3 20", c.ProcessInput("*"));            
            Assert.AreEqual("stack: -1", c.ProcessInput("clear 3 4 -"));
        }

        [TestMethod]
        public void TestCalculatorExample7()
        {
            var c = new Calculator();            
            Assert.AreEqual("stack: 1 2 3 4 5", c.ProcessInput("1 2 3 4 5"));
            Assert.AreEqual("stack: 120", c.ProcessInput("* * * *"));
        }

        [TestMethod]
        public void TestCalculatorExample8()
        {
            var c = new Calculator();
            Assert.AreEqual($"operator * (position: 15): insucient parameters{Environment.NewLine}stack: 11", c.ProcessInput("1 2 3 * 5 + * * 6 5"));
        }
    }
}
