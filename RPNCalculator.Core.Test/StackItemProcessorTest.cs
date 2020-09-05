using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RPNCalculator.Core.Test
{
    [TestClass]
    public class StackItemProcessorTest
    {
        private StackItemProcessor processor = new StackItemProcessor();

        [TestMethod]
        public void TestApplyInputToStack()
        {
            var result = processor.ParseInputString("2 + - * / sqrt undo clear");
            Assert.AreEqual(8, result.Count);
            Assert.IsInstanceOfType(result[0], typeof(StackItemNumber));
            Assert.AreEqual(2, (result[0] as StackItemNumber).Value);
            Assert.IsInstanceOfType(result[1], typeof(StackItemOperator));
            Assert.AreEqual(OperatorType.Addition, (result[1] as StackItemOperator).Operator);
            Assert.IsInstanceOfType(result[2], typeof(StackItemOperator));
            Assert.AreEqual(OperatorType.Subtraction, (result[2] as StackItemOperator).Operator);
            Assert.IsInstanceOfType(result[3], typeof(StackItemOperator));
            Assert.AreEqual(OperatorType.Multiplication, (result[3] as StackItemOperator).Operator);
            Assert.IsInstanceOfType(result[4], typeof(StackItemOperator));
            Assert.AreEqual(OperatorType.Division, (result[4] as StackItemOperator).Operator);
            Assert.IsInstanceOfType(result[5], typeof(StackItemOperator));
            Assert.AreEqual(OperatorType.Sqrt, (result[5] as StackItemOperator).Operator);
            Assert.IsInstanceOfType(result[6], typeof(StackItemOperator));
            Assert.AreEqual(OperatorType.Undo, (result[6] as StackItemOperator).Operator);
            Assert.IsInstanceOfType(result[7], typeof(StackItemOperator));
            Assert.AreEqual(OperatorType.Clear, (result[7] as StackItemOperator).Operator);
        }

        [TestMethod]
        public void TestGetStringFromStackDecimal()
        {
            List<decimal> myStack = new List<decimal>() { 6, -10, 0 };
            var result = processor.GetStringFromStackDecimal(myStack);
            Assert.AreEqual("6 -10 0", result);
        }

        [TestMethod]
        public void TestExecuteMathOperator()
        {
            var result = processor.ExecuteMathOperator(new StackItemOperator(OperatorType.Sqrt), 9);
            Assert.AreEqual(3, result.Value);
            result = processor.ExecuteMathOperator(new StackItemOperator(OperatorType.Addition), 3, -9);
            Assert.AreEqual(-6, result.Value);
            result = processor.ExecuteMathOperator(new StackItemOperator(OperatorType.Subtraction), 3, -9);
            Assert.AreEqual(12, result.Value);
            result = processor.ExecuteMathOperator(new StackItemOperator(OperatorType.Multiplication), 3.1m, -9);
            Assert.AreEqual(-27.9m, result.Value);
            result = processor.ExecuteMathOperator(new StackItemOperator(OperatorType.Division), 3, 2);
            Assert.AreEqual(1.5m, result.Value);
        }
    }
}
