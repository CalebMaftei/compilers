using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    //PURPOSE: Translates the Hack Mnemonics into Binary Strings
    class Code
    {
        private Dictionary<string, string> destDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> compDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> jumpDictionary = new Dictionary<string, string>();

        //Creates an object that sifts through code to produce the binary values.
        public Code() //0 param constructor
        {
            //NOTE: THE SPACES BETWEEN THE BINARY VALUES ARE MEANT TO HAVE SPACES. ITS FOR FORMATTING PURPOSES.
            // Destination Dictionary Created
            this.destDictionary.Add("null", "00 0");
            this.destDictionary.Add("M", "00 1");
            this.destDictionary.Add("D", "01 0");
            this.destDictionary.Add("MD", "01 1");
            this.destDictionary.Add("A", "10 0");
            this.destDictionary.Add("AM", "10 1");
            this.destDictionary.Add("AD", "11 0");
            this.destDictionary.Add("AMD", "11 1");

            //Jump Directory Created
            this.jumpDictionary.Add("null", "000");
            this.jumpDictionary.Add("JGT", "001");
            this.jumpDictionary.Add("JEQ", "010");
            this.jumpDictionary.Add("JGE", "011");
            this.jumpDictionary.Add("JLT", "100");
            this.jumpDictionary.Add("JNE", "101");
            this.jumpDictionary.Add("JLE", "110");
            this.jumpDictionary.Add("JMP", "111");

            //Comp Directory Created
            this.compDictionary.Add("0",   "0 1010 10");
            this.compDictionary.Add("1",   "0 1111 11");
            this.compDictionary.Add("-1",  "0 1110 10");
            this.compDictionary.Add("D",   "0 0011 00");
            this.compDictionary.Add("A",   "0 1100 00");
            this.compDictionary.Add("M",   "1 1100 00"); //a=1
            this.compDictionary.Add("!D",  "0 0011 01");
            this.compDictionary.Add("!A",  "0 1100 01");
            this.compDictionary.Add("!M",  "1 1100 01"); //a=1
            this.compDictionary.Add("-D",  "0 0011 11");
            this.compDictionary.Add("-A",  "0 1100 11");
            this.compDictionary.Add("-M",  "1 1100 11"); //a=1
            this.compDictionary.Add("D+1", "0 0111 11");
            this.compDictionary.Add("1+D", "0 0111 11");
            this.compDictionary.Add("A+1", "0 1101 11");
            this.compDictionary.Add("1+A", "0 1101 11");
            this.compDictionary.Add("M+1", "1 1101 11"); //a=1
            this.compDictionary.Add("1+M", "1 1101 11"); //a=1
            this.compDictionary.Add("D-1", "0 0011 10");
            this.compDictionary.Add("A-1", "0 1100 10");
            this.compDictionary.Add("M-1", "1 1100 10"); //a=1
            this.compDictionary.Add("D+A", "0 0000 10");
            this.compDictionary.Add("D+M", "1 0000 10"); //a=1
            this.compDictionary.Add("M+D", "1 0000 10"); //a=1
            this.compDictionary.Add("D-A", "0 0100 11");
            this.compDictionary.Add("D-M", "1 0100 11"); //a=1
            this.compDictionary.Add("A-D", "0 0001 11");
            this.compDictionary.Add("M-D", "1 0001 11"); //a=1
            this.compDictionary.Add("D&A", "0 0000 00");
            this.compDictionary.Add("A&D", "0 0000 00");
            this.compDictionary.Add("D&M", "1 0000 00"); //a=1
            this.compDictionary.Add("M&D", "1 0000 00"); //a=1
            this.compDictionary.Add("D|A", "0 0101 01");
            this.compDictionary.Add("A|D", "0 0101 01");
            this.compDictionary.Add("D|M", "1 0101 01"); //a=1
            this.compDictionary.Add("M|D", "1 0101 01"); //a=1
        }
        
        //based on the mneumonic passed in, this looks through all possible values. If it doesn't exist, it throws an error.
        public string dest(string mneumonic)
        {
            foreach(KeyValuePair<string,string> entry in destDictionary)
            {
                if(entry.Key == mneumonic)
                {
                    return entry.Value;
                }
            }
            throw new InvalidDestinationException(mneumonic);
        }

        //based on the mneumonic passed in, this looks through all possible values. If it doesn't exist, it throws an error.
        public string comp(string mneumonic)
        {
            foreach (KeyValuePair<string, string> entry in compDictionary)
            {
                if (entry.Key == mneumonic)
                {
                    return entry.Value;
                }
            }
            throw new InvalidCompException(mneumonic);
        }

        //based on the mneumonic passed in, this looks through all possible values. If it doesn't exist, it throws an error.
        public string jump(string mneumonic)
        {
            foreach (KeyValuePair<string, string> entry in jumpDictionary)
            {
                if (entry.Key == mneumonic)
                {
                    return entry.Value;
                }
            }
            throw new InvalidJumpException(mneumonic);
        }
    }
}
