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
            // Destination Dictionary Created
            this.destDictionary.Add("null", "000");
            this.destDictionary.Add("M", "001");
            this.destDictionary.Add("D", "010");
            this.destDictionary.Add("MD", "011");
            this.destDictionary.Add("A", "100");
            this.destDictionary.Add("AM", "101");
            this.destDictionary.Add("AD", "110");
            this.destDictionary.Add("AMD", "111");

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
            this.compDictionary.Add("0", "101010");
            this.compDictionary.Add("1", "111111");
            this.compDictionary.Add("-1", "111010");
            this.compDictionary.Add("D", "001100");
            this.compDictionary.Add("A", "110000");
            this.compDictionary.Add("M", "110000"); //a=1
            this.compDictionary.Add("!D", "001101");
            this.compDictionary.Add("!A", "110001");
            this.compDictionary.Add("!M", "110001"); //a=1
            this.compDictionary.Add("-D", "001111");
            this.compDictionary.Add("-A", "110011");
            this.compDictionary.Add("-M", "110011"); //a=1
            this.compDictionary.Add("D+1", "011111");
            this.compDictionary.Add("1+D", "011111");
            this.compDictionary.Add("A+1", "110111");
            this.compDictionary.Add("1+A", "110111");
            this.compDictionary.Add("M+1", "110111"); //a=1
            this.compDictionary.Add("1+M", "110111"); //a=1
            this.compDictionary.Add("D-1", "001110");
            this.compDictionary.Add("A-1", "110010");
            this.compDictionary.Add("M-1", "110010"); //a=1
            this.compDictionary.Add("D+A", "000010");
            this.compDictionary.Add("D+M", "000010"); //a=1
            this.compDictionary.Add("D-A", "010011");
            this.compDictionary.Add("D-M", "010011"); //a=1
            this.compDictionary.Add("A-D", "000111");
            this.compDictionary.Add("M-D", "000111"); //a=1
            this.compDictionary.Add("D&A", "000000");
            this.compDictionary.Add("A&D", "000000");
            this.compDictionary.Add("D&M", "000000"); //a=1
            this.compDictionary.Add("M&D", "000000"); //a=1
            this.compDictionary.Add("D|A", "010101");
            this.compDictionary.Add("A|D", "010101");
            this.compDictionary.Add("D|M", "010101"); //a=1
            this.compDictionary.Add("M|D", "010101"); //a=1
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
