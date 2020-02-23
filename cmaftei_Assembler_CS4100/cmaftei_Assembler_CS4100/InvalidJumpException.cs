using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    [Serializable]
    class InvalidJumpException : Exception
    {
        public InvalidJumpException()
        {

        }

        public InvalidJumpException(string name)
        : base(String.Format("Invalid Jump Command Name: {0}.", name))
        {

        }
    }
}
