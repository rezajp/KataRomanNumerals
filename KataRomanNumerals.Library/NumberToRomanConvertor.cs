using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataRomanNumerals.Library
{
    public class NumberToRomanConvertor
    {
        public string GetRomanValue(int number)
        {
            if (number < 1)
                throw new NotSupportedException("This only works for positive numbers");
            if (number <= 3)
                return  new String('I',number);
            throw new NotSupportedException();
        }
    }
}
