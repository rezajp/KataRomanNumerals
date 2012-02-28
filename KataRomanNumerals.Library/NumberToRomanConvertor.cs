using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataRomanNumerals.Library
{
    public class NumberToRomanConvertor
    {
        public IOrderedEnumerable<Step> Steps
        {
            get
            {
                var stepsArray = new[]{
                    new Step() { Value = 1, Symbol = 'I',Level=1, CanSubtract = true },
                new Step() { Value = 5,Level=2, Symbol = 'V'},
                new Step(){Value = 10,Level=3,Symbol='X',CanSubtract=true}
                };
                return stepsArray.OrderByDescending(s => s.Level);
            }
        }
        public string GetRomanValue(int number, int startingLevel = 0, string accumulatedString = "")
        {
            CheckForNegativeValues(number);
            var usingSteps = startingLevel == 0 ? Steps : Steps.Where(s => s.Level < startingLevel);
            foreach (var step in usingSteps)
            {
                var remainder = number % step.Value;
                int quotient = number / step.Value;
                string romanValue = accumulatedString;
                if (quotient == 0)
                {
                    var subtractables = Steps.Where(s => s.Value == step.Value-remainder);
                    if (subtractables.Any() && (subtractables.First().Value + remainder) == step.Value)
                        return subtractables.First().Symbol.ToString() + step.Symbol.ToString();
                }
                if (quotient <= 3)
                    romanValue += new string(step.Symbol, quotient);
                else if (step.CanSubtract)
                    romanValue = step.Symbol.ToString() + Steps.Reverse().SkipWhile(s => s.Value <= step.Value).FirstOrDefault().Symbol;
                else
                    romanValue = Steps.FirstOrDefault(s => s.Value < step.Value).Symbol.ToString() + romanValue;
                if (remainder == 0)
                    return romanValue;

                return GetRomanValue(remainder,step.Level, romanValue);
            }

            throw new NotSupportedException();
        }

        private void CheckForNegativeValues(int number)
        {
            if (number < 1)
                throw new NotSupportedException("This only works for positive numbers");

        }
    }
}
