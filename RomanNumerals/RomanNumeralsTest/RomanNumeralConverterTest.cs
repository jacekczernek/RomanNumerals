﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomanNumerals;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public void Convert_CorrectSingleDigitNumber_Success()
        {
            //Arrange
            short input = 5;

            //Act 
            var result = converter.Convert(input);

            //Assert
            Assert.AreEqual("V", result);
        }

        [TestMethod]
        public void Convert_CorrectMultipleDigitNumber_Success()
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
        public void Convert_NumberContainingZero_Success()
        {
            short input = 1904;

            var result = converter.Convert(input);

            Assert.AreEqual("M CM IV", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Convert_OutOfRangeLowNumber_ThrowException()
        {
            short input = 0;

            var result = converter.Convert(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Convert_OutOfRangeHightNumber_ThrowException()
        {
            short input = 4000;

            var result = converter.Convert(input);
        }

        [TestMethod]
        public void Convert_TextSingleNumber_Success()
        {
            var input = "Lorem ipsum 2 dolor sit amet.";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("Lorem ipsum II dolor sit amet.", result);
            Assert.AreEqual(1, replacements);
        }

        [TestMethod]
        public void Convert_TextMultipleNumbers_Success()
        {
            var input = "Consectetur 5 adipiscing elit 9.";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("Consectetur V adipiscing elit IX.", result);
            Assert.AreEqual(2, replacements);
        }

        [TestMethod]
        public void Convert_TextMultipleDigitNumber_Success()
        {
            var input = "Ut enim quis nostrum 1904 qui.";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("Ut enim quis nostrum M CM IV qui.", result);
            Assert.AreEqual(1, replacements);
        }

        [TestMethod]
        public void Convert_TextMultipleDigitMultipleNumbers_Success()
        {
            var input = "Ut enim quis nostrum 1904 qui. Some extra text with another 3214 number.";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("Ut enim quis nostrum M CM IV qui. Some extra text with another MMM CC X IV number.", result);
            Assert.AreEqual(2, replacements);
        }

        [TestMethod]
        public void Convert_TextWithoutNumbers_Success()
        {
            var input = "Ut enim quis nostrum qui.";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("Ut enim quis nostrum qui.", result);
            Assert.AreEqual(0, replacements);
        }

        [TestMethod]
        public void Convert_TextOnlyWithNumbers_Success()
        {
            //When two numbers are next to each other, thay need to be separate by ',' in the output text
            var input = "746 357 3000";
            int replacements;

            var result = converter.Convert(input, out replacements);
            
            Assert.AreEqual("DCC XL VI, CCC L VII, MMM", result);
            Assert.AreEqual(3, replacements);
        }

        [TestMethod]
        public void Convert_TextWithNumbersNextToEachOther_Success()
        {
            var input = "Lorem ipsum 357 746 dolor sit amet.";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("Lorem ipsum CCC L VII, DCC XL VI dolor sit amet.", result);
            Assert.AreEqual(2, replacements);
        }

        [TestMethod]
        public void Convert_TextWithNumbersNextToEachOtherWithExtraSpace_Success()
        {
            var input = "Lorem ipsum 357  746 dolor sit amet.";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("Lorem ipsum CCC L VII  DCC XL VI dolor sit amet.", result);
            Assert.AreEqual(2, replacements);
        }

        [TestMethod]
        public void Convert_MultipleLineTextWithNumbers_Success()
        {
            var input = @"746 Lorem ipsum
ipsum lorem 
357 lorem lorem 
ipsum ipsum 3000";

            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual(@"DCC XL VI Lorem ipsum
ipsum lorem 
CCC L VII lorem lorem 
ipsum ipsum MMM", result);
            Assert.AreEqual(3, replacements);
        }

        [TestMethod]
        public void Convert_TextWithNumberWithFraction_Success()
        {
            //Assuming that there are no fractions, in such a scenario, we are treating those as separate numbers
            var input = "Lorem ipsum 2,45 dolor 4.56 sit amet.";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("Lorem ipsum II,XL V dolor IV.L VI sit amet.", result);
            Assert.AreEqual(4, replacements);
        }

        [TestMethod]
        public void Convert_TextWithEmptyString_Success()
        {
            var input = "";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("", result);
            Assert.AreEqual(0, replacements);
        }

        [TestMethod]
        public void Convert_TextWithTwiceTheSameNumber_Success()
        {
            var input = "Lorem 357 ipsum 357 dolor sit amet.";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("Lorem CCC L VII ipsum CCC L VII dolor sit amet.", result);
            Assert.AreEqual(2, replacements);
        }

        [TestMethod]
        public void Convert_TextWithNegativeNumber_Success()
        {
            var input = "Some negative number -345 example";
            int replacements;

            var result = converter.Convert(input, out replacements);

            Assert.AreEqual("Some negative number -CCC XL V example", result);
            Assert.AreEqual(1, replacements);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Convert_TextWithOutOfTheRangeNumber_ThrowException()
        {
            var input = "Some huge 4634 number";
            int replacements;

            var result = converter.Convert(input, out replacements);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Convert_TextWithOutOfTheDataTypeRangeNumber_ThrowException()
        {
            var input = "Some huge 463634355 number";
            int replacements;

            var result = converter.Convert(input, out replacements);
        }

        [TestMethod]
        public void Convert_MultipleInputsRunInParallel_Success()
        {
            var inputCollection = new string[10];
            inputCollection[0] = "Some negative number -345 example";
            inputCollection[1] = "Some negative number -345 example 345";
            inputCollection[2] = "Some negative number -345 example 345 345";
            inputCollection[3] = "Some negative number -345 example 345 345 345";
            inputCollection[4] = "Some negative number -345 example 345 345 345 345";
            inputCollection[5] = "Some negative number -345 example 345 345 345 345 345";
            inputCollection[6] = "Some negative number -345 example 345 345 345 345 345 345";
            inputCollection[7] = "Some negative number -345 example 345 345 345 345 345 345 345";
            inputCollection[8] = "Some negative number -345 example 345 345 345 345 345 345 345 345";
            inputCollection[9] = "Some negative number -345 example 345 345 345 345 345 345 345 345 345";

            var resultCollection = new Dictionary<string, int>(10);
            Parallel.ForEach(inputCollection, input => 
            {
                int replacements;
                var result = converter.Convert(input, out replacements);

                resultCollection.Add(input, replacements);
            });

            Assert.AreEqual(1, resultCollection[inputCollection[0]]);
            Assert.AreEqual(2, resultCollection[inputCollection[1]]);
            Assert.AreEqual(3, resultCollection[inputCollection[2]]);
            Assert.AreEqual(4, resultCollection[inputCollection[3]]);
            Assert.AreEqual(5, resultCollection[inputCollection[4]]);
            Assert.AreEqual(6, resultCollection[inputCollection[5]]);
            Assert.AreEqual(7, resultCollection[inputCollection[6]]);
            Assert.AreEqual(8, resultCollection[inputCollection[7]]);
            Assert.AreEqual(9, resultCollection[inputCollection[8]]);
            Assert.AreEqual(10, resultCollection[inputCollection[9]]);
        }

        [TestMethod]
        public void Convert_HeavyLoadTest_Success()
        {
            short input = 254;
            string result = "";

            for (int i = 1; i < 100000000; i++)
            {
                result = converter.Convert(input);
            }

            Assert.AreEqual("CC L IV", result);
        }
    }
}
