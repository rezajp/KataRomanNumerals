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
                var stepsArray = new[]
                {
                    new Step() { Value = 1, Symbol = 'I',Level=1, CanSubtract = true },
                    new Step() { Value = 5,Level=2, Symbol = 'V'},
                    new Step(){Value = 10,Level=3,Symbol='X',CanSubtract=true},
                    new Step(){Value=50,Level=4,Symbol='L'},
                    new Step(){Value = 100,Level=5,Symbol='C',CanSubtract=true},
                    new Step(){Value=500,Level=6,Symbol='D'},
                    new Step(){Value = 1000,Level=7,Symbol='M',CanSubtract=true},
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
                if (IsInvalidStep(step, remainder))
                    continue;
                string romanValue = accumulatedString;
                //if the number is less than the step value but it is not a step itself
                if (quotient == 0 && !Steps.Select(s => s.Value).Contains(remainder))
                {
                    var subtractables = Steps.Where(s => IsSubtractableForLevel(s, step.Level));
                    if (subtractables.Any())
                    {
                        var firstSubtractor = subtractables.First();
                        if ((firstSubtractor.Value + remainder) == step.Value)
                            return accumulatedString + firstSubtractor.Symbol.ToString() + step.Symbol.ToString();
                        else if ((step.Value - firstSubtractor.Value) < number)
                        {

                            return GetRomanValue(number - (step.Value - firstSubtractor.Value), step.Level, firstSubtractor.Symbol.ToString() + step.Symbol.ToString());
                        }
                    }
                }
                romanValue = GetRomanValueForBiggerNumbers(romanValue,quotient,step,accumulatedString);

                if (remainder == 0)
                    return romanValue;

                return GetRomanValue(remainder, step.Level, romanValue);
            }

            throw new NotSupportedException();
        }

        private string GetRomanValueForBiggerNumbers(string romanValue,int quotient,Step step,string accumulatedString)
        {
            if (quotient <= 3)
                romanValue += new string(step.Symbol, quotient);
            else if (!step.CanSubtract)
                romanValue = Steps.FirstOrDefault(s => s.Value < step.Value).Symbol.ToString() + romanValue;
            else
            {
                if (quotient == 4)
                    romanValue = accumulatedString + step.Symbol.ToString() + Steps.LastOrDefault(s => s.Level > step.Level).Symbol;
                else
                    romanValue = accumulatedString + step.Symbol.ToString() + Steps.LastOrDefault(s => s.Level > step.Level && s.CanSubtract).Symbol;
            }
            return romanValue;
        }

        private bool IsInvalidStep(Step step, int remainder)
        {
            return !step.CanSubtract && remainder > 3;
        }

        private bool IsSubtractableForLevel(Step step, int level)
        {
            return step.CanSubtract && step.Level < level;
        }

        private void CheckForNegativeValues(int number)
        {
            if (number < 1)
                throw new NotSupportedException("This only works for positive numbers");

        }
    }
}
