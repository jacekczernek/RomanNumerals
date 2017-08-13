using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomanNumerals;

namespace RomanNumeralsTest
{
    [TestClass]
    public class RomanNumeralConverterTest
    {
        private IRomanNumeralsConverter converter;

        [TestInitialize]
        public void Initialize()
        {
            converter = new RomanNumeralsConverter();
        }

        [TestMethod]
        public void Convert_CorrectSingleDigitInput_Success()
        {
            //Arrange
            short input = 5;

            //Act 
            var result = converter.Convert(input);

            //Assert
            Assert.AreEqual("V", result);
        }

        [TestMethod]
        public void Convert_CorrectMultipleDigitInput_Success()
        {
            short input = 999;

            var result = converter.Convert(input);

            Assert.AreEqual("CM XC IX", result);
        }

        [TestMethod]
        public void Convert_CorrectMinimumNumber_Success()
        {
            short input = 1;

            var result = converter.Convert(input);

            Assert.AreEqual("I", result);
        }

        [TestMethod]
        public void Convert_CorrectMaximugNumber_Success()
        {
            short input = 3999;

            var result = converter.Convert(input);

            Assert.AreEqual("MMM CM XC IX", result);
        }

        [TestMethod]
        public void Convert_CorrectContainingZero_Success()
        {
            short input = 1904;

            var result = converter.Convert(input);

            Assert.AreEqual("M CM IV", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Convert_OutOfRangeLow_ThrowException()
        {
            short input = 0;

            var result = converter.Convert(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Convert_OutOfRangeHight_ThrowException()
        {
            short input = 4000;

            var result = converter.Convert(input);
        }
    }
}
