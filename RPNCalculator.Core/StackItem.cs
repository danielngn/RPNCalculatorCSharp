using System;
using System.Xml.Schema;

namespace RPNCalculator.Core
{
    public enum OperatorType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Sqrt,
        Undo,
        Clear
    }

    public abstract class StackItemBase
    {

    }

    public class StackItemOperator : StackItemBase
    {
        public OperatorType Operator { get; private set; }
        public int Position { get; private set; }

        //Count of numbers needed for this operator
        public int RequiredNumber
        {
            get
            {
                switch(Operator)
                {
                    case OperatorType.Addition:
                    case OperatorType.Subtraction:
                    case OperatorType.Multiplication:
                    case OperatorType.Division:
                        return 2;
                    case OperatorType.Sqrt:
                        return 1;
                    case OperatorType.Undo:
                    case OperatorType.Clear:
                        return 0;
                }
                return -1;
            }
        }

        public StackItemOperator(OperatorType operatorType, int position = -1)
        {
            Operator = operatorType;
            Position = position;
        }

        public string GetOperatorStr()
        {
            switch(Operator)
            {
                case OperatorType.Addition:
                    return "+";
                case OperatorType.Division:
                    return "/";
                case OperatorType.Multiplication:
                    return "*";
                case OperatorType.Sqrt:
                    return "sqrt";
                case OperatorType.Subtraction:
                    return "-";
            }
            return null;
        }
    }

    public class StackItemNumber : StackItemBase
    {
        public decimal Value { get; private set; }

        public StackItemNumber(decimal value)
        {
            Value = value;
        }
    }
}
