using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    [Serializable]
    class IllegalATypeValueException : Exception
    {
        public IllegalATypeValueException()
        {

        }

        public IllegalATypeValueException(string command, int line)
        : base(String.Format("Line[{1}]: ERROR -- Illegal A-Type Command: @{0}. Value must be non-negative, not null, and/or less than 16 bits long (in binary).", command, line))
        {

        }
    }
}
