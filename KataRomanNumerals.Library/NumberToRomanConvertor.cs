using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataRomanNumerals.Library
{
    public class NumberToRomanConvertor
    {
        public IEnumerable<Step> Steps
        {
            get
            {
                yield return new Step() { Value = 1, Symbol = 'I', CanSubtract = true };
                yield return new Step() { Value = 5, Symbol = 'V', CanSubtract = false };
            }
        }

        public string GetRomanValue(int number,string accumulatedString="")
        {
            CheckForNegativeValues(number);
            
            foreach (var step in Steps.Where(s=> number>=s.Value).OrderByDescending(s=>s.Value))
            {
                var remainder = number % step.Value;
                var romanValue=accumulatedString + new string(step.Symbol, number / step.Value);
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
