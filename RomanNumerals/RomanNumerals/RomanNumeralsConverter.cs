using System;
using System.Text;

namespace RomanNumerals
{
    public class RomanNumeralsConverter : IRomanNumeralsConverter
    {
        public string Convert(short number)
        {
            if (number < 1 || number > 3999)
                throw new ArgumentOutOfRangeException("Number should be between 1 and 3999");

            var result = new StringBuilder();
            var numberAsCharArray = number.ToString();
            
            for (int i = 0; i < numberAsCharArray.Length; i++)
            {
                var digit = short.Parse(numberAsCharArray[i].ToString());
                var rangeBase = (int)Math.Pow(10, (numberAsCharArray.Length - i - 1));

                var romanDigit = GetRomanDigitForRange(digit, rangeBase);

                if (!String.IsNullOrWhiteSpace(romanDigit))
                    result.AppendFormat("{0} ", romanDigit);
            }

            return result.ToString().TrimEnd(' ');
        }

        public string Convert(string text, out int numberOfReplacement)
        {
            var result = "";
            numberOfReplacement = 0;



            return result;
        }

        private string GetRomanDigitForRange(short value, int rangeBase)
        {
            switch (rangeBase)
            {
                case 1:
                    return ConstructRomanDigit(value, 'I', 'V', 'X');
                case 10:
                    return ConstructRomanDigit(value, 'X', 'L', 'C');
                case 100:
                    return ConstructRomanDigit(value, 'C', 'D', 'M');
                case 1000:
                    if (value > 3)
                        throw new NotImplementedException();

                    return ConstructRomanDigit(value, 'M', ' ', ' ');
                default:
                    throw new ArgumentOutOfRangeException("Range base should be 10th base power up to 1000");
            }
        }

        private string ConstructRomanDigit(short value, char startSymbol, char middleSymbol, char endSymbol)
        {
            StringBuilder result = new StringBuilder();
            switch (value)
            {
                case 1:
                    result.Append(startSymbol);
                    break;
                case 2:
                    result.AppendFormat("{0}{1}",startSymbol,startSymbol);
                    break;
                case 3:
                    result.AppendFormat("{0}{1}{2}", startSymbol, startSymbol, startSymbol);
                    break;
                case 4:
                    result.AppendFormat("{0}{1}", startSymbol, middleSymbol);
                    break;
                case 5:
                    result.Append(middleSymbol);
                    break;
                case 6:
                    result.AppendFormat("{0}{1}", middleSymbol, startSymbol);
                    break;
                case 7:
                    result.AppendFormat("{0}{1}{2}", middleSymbol, startSymbol, startSymbol);
                    break;
                case 8:
                    result.AppendFormat("{0}{1}{2}{3}", middleSymbol, startSymbol, startSymbol, startSymbol);
                    break;
                case 9:
                    result.AppendFormat("{0}{1}", startSymbol, endSymbol);
                    break;
            }

            return result.ToString();
        }
    }
}
