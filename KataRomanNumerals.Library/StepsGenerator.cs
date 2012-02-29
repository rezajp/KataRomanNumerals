using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataRomanNumerals.Library
{
    public class StepsGenerator
    {
        
        private IEnumerable<Step> Steps
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

        public IEnumerable<Step> GetUsableSteps(int startingLevel)
        {
            return startingLevel == 0 ? Steps : Steps.Where(s => s.Level < startingLevel);
        }

        public bool Contains(int remainder)
        {
            return Steps.Select(s => s.Value).Contains(remainder);
        }

        public IEnumerable<Step> GetSubtractablesForLevel(int level)
        {
            return Steps.Where(s => IsSubtractableForLevel(s, level));
        }
        private bool IsSubtractableForLevel(Step step, int level)
        {
            return step.CanSubtract && step.Level < level;
        }

        public string GetNextSmallerSymbol(Step step)
        {
            var nextSmallerStep = Steps.FirstOrDefault(s => s.Value < step.Value);
            if(nextSmallerStep!=null)
                return nextSmallerStep.Symbol.ToString();
            return "";
        }

        public string GetLastLevelSymbol(Step step)
        {
            var lastLevelStep = Steps.LastOrDefault(s => s.Level > step.Level);
            if (lastLevelStep != null)
                return lastLevelStep.Symbol.ToString();
            return "";
            
        }

        internal string GetLastSubtractableLevelSymbol(Step step)
        {
            var lastSubtractableLevelStep=Steps.LastOrDefault(s => s.Level > step.Level && s.CanSubtract);
            if (lastSubtractableLevelStep != null)
                return lastSubtractableLevelStep.Symbol.ToString();
            return "";
        }

        
    }
}
