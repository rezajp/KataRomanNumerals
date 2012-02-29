using System;
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
        NumberToRomanConvertor Convertor;
        [TestInitialize]
        public void Initialize()
        {
            var stepGenerator = new StepsGenerator();
            Convertor = new NumberToRomanConvertor(stepGenerator);
        }

        [TestMethod]
        public void NumberToRomanConvertor_Returns_I_For_1()
        {
            string result = Convertor.GetRomanValue(1);
            Assert.AreEqual("I", result);
        }
        [TestMethod]
        public void NumberToRomanConvertor_Returns_Correct_Value_For_Numbers_Lower_Than_3()
        {
            Assert.AreEqual("II", Convertor.GetRomanValue(2));
            Assert.AreEqual("II", Convertor.GetRomanValue(2));
        }
        [TestMethod]
        public void NumberToRomanConvertor_Handles_Normally_Calculated_Numbers_Up_To_8()
        {
            Assert.AreEqual("V", Convertor.GetRomanValue(5));
            Assert.AreEqual("VI", Convertor.GetRomanValue(6));
            Assert.AreEqual("VIII", Convertor.GetRomanValue(8));
        }
        [TestMethod]
        public void NumberToRomanConvertor_Returns_IV_For_4()
        {
            Assert.AreEqual("IV", Convertor.GetRomanValue(4));
        }
        [TestMethod]
        public void NumberToRomanConvertor_Returns_IX_For_9()
        {
            Assert.AreEqual("IX", Convertor.GetRomanValue(9));
        }
        [TestMethod]
        public void NumberToRomanConvertor_Returns_XXX_For_30()
        {
            Assert.AreEqual("XXX", Convertor.GetRomanValue(30));
        }
        [TestMethod]
        public void NumberToRomanConvertor_Returns_XLIX_For_49()
        {
            Assert.AreEqual("XLIX", Convertor.GetRomanValue(49));
        }
        [TestMethod]
        public void NumberToRomanConvertor_Returns_MCMXCIX_For_1999()
        {
            Assert.AreEqual("MCMXCIX", Convertor.GetRomanValue(1999));
        }
        [TestMethod]
        public void NumberToRomanConvertor_Returns_XCIX_For_99()
        {
            Assert.AreEqual("XCIX", Convertor.GetRomanValue(99));
        }
       
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void NumberToRomanConvertor_ThrowsExeptionFor_4000()
        //{
        //    Convertor.GetRomanValue(4000);
        //}
    }
}
