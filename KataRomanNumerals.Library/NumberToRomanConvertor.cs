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
                    new Step() { Value = 1, Symbol = 'I', CanSubtract = true },
                new Step() { Value = 5, Symbol = 'V', CanSubtract = false }
                };
                return stepsArray.OrderByDescending(s => s.Value);
            }
        }
        public string GetRomanValue(int number, string accumulatedString = "")
        {
            CheckForNegativeValues(number);

            foreach (var step in Steps.Where(s => number >= s.Value))
            {
                var remainder = number % step.Value;
                int quotient = number / step.Value;
                string romanValue = accumulatedString;
                if (quotient <= 3)
                    romanValue += new string(step.Symbol, quotient);
                else if (step.CanSubtract)
                    romanValue = step.Symbol.ToString() + Steps.Reverse().SkipWhile(s => s.Value <= step.Value).FirstOrDefault().Symbol;
                else
                    romanValue = Steps.FirstOrDefault(s => s.Value < step.Value).Symbol.ToString() + romanValue;
                if (remainder == 0)
                    return romanValue;

                return GetRomanValue(remainder, romanValue);
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
