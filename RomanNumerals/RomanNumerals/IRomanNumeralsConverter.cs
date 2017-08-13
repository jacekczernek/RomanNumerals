using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumerals
{
    public interface IRomanNumeralsConverter
    {
        string Convert(short number);
    }
}
