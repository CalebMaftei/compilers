using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    public class Parser
    {
        //private properties
        private string[] asmFile;
        private string currentInstruction;
        private SymbolTable symbolTable = new SymbolTable();
        private Code code = new Code();

        //Constructor - Requires the .asm file to parse anything.
        public Parser(string[] newAsmFile)
        {
            this.asmFile = newAsmFile;
        }

        //getters and setters
        public string[] getAsmFile()
        {
            return this.asmFile;
        }

        public SymbolTable getSymbolTable()
        {
            return this.symbolTable;
        }

        //Checks if there exists another command
        [Obsolete]
        public bool hasMoreCommands()
        {
            //return true if there exists more commands within the asmFile... Might not need this if iterating through a finite sized array of strings.
            return true;
        }

        //Places the next command into question
        [Obsolete]
        public void advance()
        {
            //Reads the next input of the given file. Only do this if hasMoreCommands returns true. There does not exist a current instruction at first.
        }

        //Returns the type of the command
        public string CommmandType()
        {
            string command = "";
            //returns the command Type of the current Command.
            //Returns A_Command if the first Char in the string is @
            //Returns C_Command if the instruction takes the form of dest=comp; jump
            //Returns L_Command if instruction is just a symbol like (LOOP)


            return command; //This in turn refers to another method that constructs the correct object according to command's value.
        }

        public string symbol()
        {
            string symbol = "";

            //Returns the symbol or decimal of the current command... Do this only if command is a L_Command or A_Command

            return symbol;
        }

        public string dest()
        {
            string destMneumonic = "";

            //Returns the dest mneumonic of current command. Only works if the command is a C_Command.

            return destMneumonic;
        }

        public string comp()
        {
            string compMneumonic = "";

            //Returns the Comp Mneumonic of current command... Only execute if teh command is a C_Command.

            return compMneumonic;
        }

        public string jump()
        {
            string jumpMneumonic = "";

            //Returns the Jump Mneumonic of current command... Only execute if teh command is a C_Command.

            return jumpMneumonic;
        }

        //Should occur after the .asm File has had its white space, return lines, and comments removed.
        public string[] ConstructSymbolTable()
        {
            //Turn Binary Into List. This will allow for shortening code even more.
            this.asmFile.ToList();
            List<string> noLabelAsmFile = new List<string>();

            //This will keep track of the array placement
            int index = 0;
            foreach (string str in this.asmFile)
            {
                if (str.IndexOf("(") == 0) //implies that the line is a label
                {
                    string tempStr = str.Substring(1, str.IndexOf(")") - 1);
                    this.symbolTable.addEntry(tempStr, index);
                }
                else
                {
                    noLabelAsmFile.Add(str);
                }
                index++;
            }

            return noLabelAsmFile.ToArray();
        }
    }
}
