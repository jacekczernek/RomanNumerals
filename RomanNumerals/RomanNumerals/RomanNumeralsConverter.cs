using System;
using System.Collections.Concurrent;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace RomanNumerals
{
    public class RomanNumeralsConverter : IRomanNumeralsConverter
    {
        private const string regexPattern = @"(\d+(\s\d+)+)|(\d+)";
        private const string outOfRangeExceptionMessage = "Number should be between 1 and 3999";
        private ThreadLocal<int> replacementCounter = new ThreadLocal<int>();
        private static ConcurrentDictionary<short, string> cache = new ConcurrentDictionary<short, string>();

        public string Convert(short number)
        {
            if (number < 1 || number > 3999)
                throw new ArgumentOutOfRangeException(outOfRangeExceptionMessage);

            string cachedValue;
            if (cache.TryGetValue(number, out cachedValue))
                return cachedValue;
            
            var numberAsCharArray = number.ToString();
            var resultBuilder = new StringBuilder(numberAsCharArray.Length);

            for (int i = 0; i < numberAsCharArray.Length; i++)
            {
                var digit = short.Parse(numberAsCharArray[i].ToString());
                var digitBase = (int)Math.Pow(10, (numberAsCharArray.Length - i - 1));

                var romanDigit = GetRomanDigitForRange(digit, digitBase);

                if (!String.IsNullOrWhiteSpace(romanDigit))
                    resultBuilder.AppendFormat("{0} ", romanDigit);
            }

            var result = resultBuilder.ToString().TrimEnd(' ');

            cache.TryAdd(number, result);
            return result;
        }
        
        public string Convert(string text, out int numberOfReplacements)
        {
            replacementCounter.Value = 0;
            
            var result = Regex.Replace(text, regexPattern, RegexEvaluator);
            numberOfReplacements = replacementCounter.Value;

            return result;
        }

        private string RegexEvaluator(Match input)
        {
            if (input.Value.Contains(" "))
            {
                var separateNumbers = input.Value.Split(' ');
                var results = new StringBuilder(separateNumbers.Length);

                foreach (var item in separateNumbers)
                {
                    results.AppendFormat("{0}, ", ConvertSingleTextNumber(item));
                }

                return results.ToString().TrimEnd(' ',',');
            }
            else
                return ConvertSingleTextNumber(input.Value);
        }

        private string ConvertSingleTextNumber(string input)
        {
            short number;
            if (short.TryParse(input, out number))
            {
                replacementCounter.Value++;
                return Convert(number);
            }
            else
                throw new ArgumentOutOfRangeException(outOfRangeExceptionMessage);
        }

        private string GetRomanDigitForRange(short digit, int digitBase)
        {
            switch (digitBase)
            {
                case 1:
                    return ConstructRomanDigit(digit, 'I', 'V', 'X');
                case 10:
                    return ConstructRomanDigit(digit, 'X', 'L', 'C');
                case 100:
                    return ConstructRomanDigit(digit, 'C', 'D', 'M');
                case 1000:
                    if (digit > 3)
                        throw new NotImplementedException();

                    return ConstructRomanDigit(digit, 'M', ' ', ' ');
                default:
                    throw new ArgumentOutOfRangeException("Range base should be 10th base power up to 1000");
            }
        }

        private string ConstructRomanDigit(short digit, char startSymbol, char middleSymbol, char endSymbol)
        {
            StringBuilder result = new StringBuilder();
            switch (digit)
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
