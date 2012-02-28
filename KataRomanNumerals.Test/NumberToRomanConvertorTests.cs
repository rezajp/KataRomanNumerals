﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataRomanNumerals.Library;

namespace KataRomanNumerals.Test
{
    [TestClass]
    public class NumberToRomanConvertorTests
    {
        [TestMethod]
        public void NumberToRomanConvertor_Returns_I_For_1()
        {
            var convertor = new NumberToRomanConvertor();
            string result = convertor.GetRomanValue(1);
            Assert.AreEqual("I", result);
        }
        [TestMethod]
        public void NumberToRomanConvertor_Returns_Correct_Value_For_Numbers_Lower_Than_3()
        {
            var convertor = new NumberToRomanConvertor();
            Assert.AreEqual("II", convertor.GetRomanValue(2));
            Assert.AreEqual("II", convertor.GetRomanValue(2));
        }
    }
}
