using System;
using System.Collections.Generic;
using System.Text;

namespace RPNCalculator.Core
{
    public class InsufficientParameterException : Exception
    {
        public StackItemOperator Operator { get; private set; }

        public InsufficientParameterException(StackItemOperator itemOperator)
        {
            Operator = itemOperator;
        }

        public override string ToString()
        {
            return $"operator {Operator.GetOperatorStr()} (position: {Operator.Position}): insucient parameters";
        }
    }
}
