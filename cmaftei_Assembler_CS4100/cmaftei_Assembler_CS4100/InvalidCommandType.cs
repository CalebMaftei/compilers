using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    [Serializable]
    class InvalidCommandType : Exception
    {
        public InvalidCommandType()
        {

        }

        public InvalidCommandType(string command, int index)
        : base(String.Format("Line[{1}]: ERROR -- Invalid Command: {0}. Review C-Type, A-Type, and L-Type Semantics.", command, index ))
        {

        }
    }
}
