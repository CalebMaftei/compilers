using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    [Serializable]
    class InvalidDestinationException : Exception
    {
        public InvalidDestinationException()
        {

        }

        public InvalidDestinationException(string name)
        : base(String.Format("Invalid Destination Name: {0}. Valid Destinations are: D, A, or M.", name))
        {

        }
    }
}
