using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPNCalculator.Core
{
    public class StackItemProcessor
    {
        public List<StackItemBase> ParseInputString(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            List<StackItemBase> result = new List<StackItemBase>();
            int pos = 1;
            var items = input.Split(' ');
            foreach (var item in items)
            {
                StackItemBase stackItem = null;
                stackItem = TryGetOperatorFromString(item, pos);
                if (stackItem == null)
                    stackItem = TryGetNumberFromString(item);
                if (stackItem == null)
                    throw new ArgumentOutOfRangeException();
                result.Add(stackItem);
                pos += item.Length + 1; // count space as well
            }
            return result;
        }

        private StackItemOperator TryGetOperatorFromString(string item, int pos)
        {
            StackItemOperator stackItem = null;
            switch (item)
            {
                case "+":
                    stackItem = new StackItemOperator(OperatorType.Addition, pos);
                    break;
                case "-":
                    stackItem = new StackItemOperator(OperatorType.Subtraction, pos);
                    break;
                case "*":
                    stackItem = new StackItemOperator(OperatorType.Multiplication, pos);
                    break;
                case "/":
                    stackItem = new StackItemOperator(OperatorType.Division, pos);
                    break;
                case var s when s.ToUpper().Equals("SQRT"):
                    stackItem = new StackItemOperator(OperatorType.Sqrt, pos);
                    break;
                case var s when s.ToUpper().Equals("UNDO"):
                    stackItem = new StackItemOperator(OperatorType.Undo, pos);
                    break;
                case var s when s.ToUpper().Equals("CLEAR"):
                    stackItem = new StackItemOperator(OperatorType.Clear, pos);
                    break;
                case var s when s.ToUpper().Equals("UNDO"):
                    stackItem = new StackItemOperator(OperatorType.Undo, pos);
                    break;
            }
            return stackItem;
        }

        private StackItemNumber TryGetNumberFromString(string item)
        {
            decimal result;
            if (decimal.TryParse(item, out result))
            {
                return new StackItemNumber(result);
            }
            return null;
        }

        public string GetStringFromStackDecimal(List<decimal> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            return string.Join(" ", items.Select(x=>String.Format("{0:0.##########}", x)));
        }

        public decimal? ExecuteMathOperator(StackItemOperator itemOperator, decimal number1, decimal number2 = 0)
        {
            if (itemOperator == null)
                throw new ArgumentNullException(nameof(itemOperator));

            switch (itemOperator.Operator)
            {
                case OperatorType.Sqrt:
                    if (number1 < 0)
                        throw new ArgumentOutOfRangeException(nameof(number1));

                    decimal root = number1 / 3;
                    int i;
                    for (i = 0; i < 32; i++)
                        root = (root + number1 / root) / 2;
                    return root;
                case OperatorType.Addition:
                    return number1 + number2;
                case OperatorType.Subtraction:
                    return number1 - number2;
                case OperatorType.Multiplication:
                    return number1 * number2;
                case OperatorType.Division:
                    return number1 / number2;
            }
            return null;
        }

    }
}
