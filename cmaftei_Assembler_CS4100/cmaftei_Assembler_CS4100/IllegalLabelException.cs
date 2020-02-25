using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    [Serializable]
    class IllegalLabelException : Exception
    {
        public IllegalLabelException()
        {

        }

        public IllegalLabelException(string command)
        : base(String.Format("Illegal Label: {0}. There should not exist any non-commented text following a label.", command))
        {

        }
    }
}
