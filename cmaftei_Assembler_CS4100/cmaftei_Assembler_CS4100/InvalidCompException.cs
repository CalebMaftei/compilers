using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    [Serializable]
    class InvalidCompException : Exception
    {
        public InvalidCompException()
        {

        }

        public InvalidCompException(string name)
        : base(String.Format("Invalid COMP Command Name: {0}.", name))
        {

        }
    }
}
