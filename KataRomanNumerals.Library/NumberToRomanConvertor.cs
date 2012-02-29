using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataRomanNumerals.Library
{
    public class NumberToRomanConvertor
    {
        private StepsGenerator _stepsGenerator;
        public NumberToRomanConvertor(StepsGenerator stepsGenerator)
        {
            _stepsGenerator = stepsGenerator;
        }
        public string GetRomanValue(int number, int startingLevel = 0, string accumulatedString = "")
        {
            CheckForNegativeValues(number);
            CheckForBigValues(number);
            var usableSteps = _stepsGenerator.GetUsableSteps(startingLevel);
            foreach (var step in usableSteps)
            {

                var remainder = number % step.Value;
                int quotient = number / step.Value;
                if (IsInvalidStep(step, remainder))
                    continue;
                string romanValue = accumulatedString;
                //if the number is less than the step value but it is not a step itself
                if (quotient == 0 && !_stepsGenerator.Contains(remainder))
                {
                    var value= GetRomanValueForSmallerNumbers(number, accumulatedString, step, remainder);
                    if (!String.IsNullOrEmpty(value))
                        return value;
                }
                romanValue = GetRomanValueForBiggerNumbers(romanValue,quotient,step,accumulatedString);

                if (remainder == 0)
                    return romanValue;

                return GetRomanValue(remainder, step.Level, romanValue);
            }

            throw new NotSupportedException();
        }

        private void CheckForBigValues(int number)
        {
            //if (_stepsGenerator.IsNumberToLarge(number))
            //    throw new ArgumentOutOfRangeException("Number",number,"");
        }

        private string GetRomanValueForSmallerNumbers(int number, string accumulatedString, Step step, int remainder)
        {
            var subtractables = _stepsGenerator.GetSubtractablesForLevel(step.Level);
            if (subtractables != null && subtractables.Any())
            {
                var firstSubtractor = subtractables.First();
                if ((firstSubtractor.Value + remainder) == step.Value)
                    return accumulatedString + firstSubtractor.Symbol.ToString() + step.Symbol.ToString();
                if ((step.Value - firstSubtractor.Value) < number)
                {

                    return GetRomanValue(number - (step.Value - firstSubtractor.Value), step.Level, firstSubtractor.Symbol.ToString() + step.Symbol.ToString());
                }
            }
            return "";
        }

        private string GetRomanValueForBiggerNumbers(string romanValue,int quotient,Step step,string accumulatedString)
        {
            if (quotient <= 3)
                romanValue += new string(step.Symbol, quotient);
            else if (!step.CanSubtract)
                romanValue = _stepsGenerator.GetNextSmallerSymbol(step)  + romanValue;
            else
            {
                if (quotient == 4)
                    romanValue = accumulatedString + step.Symbol.ToString() + _stepsGenerator.GetLastLevelSymbol(step) ;
                else
                    romanValue = accumulatedString + step.Symbol.ToString() +_stepsGenerator.GetLastSubtractableLevelSymbol(step) ;
            }
            return romanValue;
        }

        private bool IsInvalidStep(Step step, int remainder)
        {
            return !step.CanSubtract && remainder > 3;
        }

        

        private void CheckForNegativeValues(int number)
        {
            if (number < 1)
                throw new NotSupportedException("This only works for positive numbers");

        }
    }
}
