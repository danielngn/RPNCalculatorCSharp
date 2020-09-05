using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPNCalculator.Core
{
    public class Calculator
    {
        private StackItemProcessor processor = new StackItemProcessor();
        private List<StackItemBase> inputList = new List<StackItemBase>();
        private List<decimal> outputList = new List<decimal>();

        public string ProcessInput(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            try
            {
                var list = processor.ParseInputString(input);
                foreach (var item in list)
                {
                    switch (item)
                    {
                        case StackItemNumber stackItemNumber:
                            inputList.Add(stackItemNumber);
                            outputList.Add(stackItemNumber.Value);
                            break;
                        case StackItemOperator stackItemOperator:
                            inputList.Add(stackItemOperator);
                            ExecuteTopOperator();
                            break;
                    }
                }
                return GetOutputStack();
            }
            catch (InsufficientParameterException ex)
            {
                return $"{ex}{Environment.NewLine}{GetOutputStack()}";
            }
        }

        public string GetOutputStack()
        {
            return $"stack: {processor.GetStringFromStackDecimal(outputList)}";
        }

        private void ExecuteTopOperator()
        {
            var itemOperator = inputList.Last() as StackItemOperator;
            if (itemOperator == null)
                throw new InvalidOperationException("Missing operator");

            switch (itemOperator.Operator)
            {
                case OperatorType.Clear:
                    inputList.Clear();
                    outputList.Clear();
                    break;
                case OperatorType.Undo:
                    Undo();
                    break;
                default:
                    decimal number1 = 0;
                    decimal number2 = 0;
                    if (itemOperator.RequiredNumber == 1)
                    {
                        if (outputList.Count == 0)
                        {
                            throw new InsufficientParameterException(itemOperator);
                        }
                        number1 = outputList[outputList.Count - 1];
                    }
                    else if (itemOperator.RequiredNumber == 2)
                    {
                        if (outputList.Count <= 1)
                        {
                            throw new InsufficientParameterException(itemOperator);
                        }
                        number1 = outputList[outputList.Count - 2];
                        number2 = outputList[outputList.Count - 1];
                    }
                    var result = processor.ExecuteMathOperator(itemOperator, number1, number2);
                    if (result != null)
                    {
                        inputList.Add(new StackItemNumber(result.Value));
                        for (int i = 0; i < itemOperator.RequiredNumber; i++)
                        {
                            outputList.Remove(outputList.Last());
                        }
                        outputList.Add(result.Value);
                    }
                    break;
            }
        }

        private void Undo()
        {
            if (outputList.Count == 0)
                return;
            outputList.Remove(outputList.Last());
            var last = inputList.Last(); // last is always undo at this moment
            inputList.Remove(last);
            last = inputList.Last(); // last is always a number at this moment
            inputList.Remove(last);
            last = inputList.Last();
            var lastOperator = last as StackItemOperator;
            if (lastOperator != null)
            {
                inputList.Remove(lastOperator);
                if (lastOperator.RequiredNumber == 2)
                {
                    outputList.Add((inputList[inputList.Count - 2] as StackItemNumber).Value);
                }
                outputList.Add((inputList[inputList.Count - 1] as StackItemNumber).Value);
            }
        }
    }
}
