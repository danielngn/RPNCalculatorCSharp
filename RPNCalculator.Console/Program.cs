using RPNCalculator.Core;
using System;

namespace RPNCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new Calculator();
            Console.WriteLine("RPN Calculator");            
            Console.Write("Input: ");
            var input = Console.ReadLine();
            while(!string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(calc.ProcessInput(input));
                Console.Write("Input: ");
                input = Console.ReadLine();
            }
        }
    }
}
