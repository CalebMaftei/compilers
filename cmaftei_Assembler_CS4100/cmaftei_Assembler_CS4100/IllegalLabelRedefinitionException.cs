using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    [Serializable]
    class IllegalLabelRedefinitionException : Exception
    {
        public IllegalLabelRedefinitionException()
        {

        }

        public IllegalLabelRedefinitionException(string symbol, int line)
        : base (String.Format("IllegalLabel: {0}. It has already been defined on line[{1}]", symbol, line))
        {

        }
    }
}
