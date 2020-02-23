using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    public class Memory
    {
        private bool isReadOnly;
        private int memorySize;
        private List<string> allocatedMemory;

        public Memory(bool newIsReadOnly, int memorySize)
        {
            this.isReadOnly = newIsReadOnly;
            this.memorySize = memorySize;

            //Instantiates the memory with blank spaces.
            for(int i = 0; i < this.memorySize; i++)
            {
                this.allocatedMemory[i] = " ";
            }
        }

        //If use wants to see how the memory addresses are being populated, use this to produce the memory hierarchy.
        public override string ToString()
        {
            string returnString = "";
            string[] memoryArray = allocatedMemory.ToArray();

            for(int i = 0; i < memoryArray.Length; i++ )
            {
                //All necessary memory will be in the string, rather than the empty slots.
                if(memoryArray[i] != " ")
                {
                    returnString += i + "," + memoryArray[i] + "\n";
                }
            }
            return returnString; 
        }
    }
}
