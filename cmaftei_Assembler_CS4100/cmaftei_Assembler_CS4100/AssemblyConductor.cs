using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    //Conducts the Assembly Process Starting with an initial pass through of the .asm file, to construct 
    public class AssemblyConductor
    {
        //Objects
        private Parser parser;
 
        //Constructor
        public AssemblyConductor(string[] newAsmFile)
        {
            this.parser = new Parser(newAsmFile);
        }

        //Retrieves Symbol Table
        public SymbolTable GetSymbolTable()
        {
            return this.parser.GetSymbolTable();
        }

        //Takes the asmFile contents and converts them into the contents of a .hack file.
        public string[] Assemble()
        {
            //Creates the Symbol Table -- Equivalent to the first pass
            parser.ConstructSymbolTable();

            //Returns the converted Binary -- For spacing, and readability, this is a method.
            return ConvertToBindary();
        }

        //Parses clean code to produce a binary file by parsing and translating via the CODE and PARSER class
        private string[] ConvertToBindary()
        {
            List<string> binaryConversion = new List<string>();
            for (int i = 0; i < this.parser.GetAsmFile().Length; i++) //iterate through each line in parser's asmFile.
            {
                //Removes white space and in-line comments for current line.
                this.parser.CleanCurrentLine();

                //Dependent on the command type of the instruction, convert to binary, or continue to next instruction
                try
                {
                    string commandType = this.parser.CommmandType();
                    if (commandType == "A")
                    {
                        try
                        {
                            binaryConversion.Add(this.parser.Symbol());
                        }
                        catch(IllegalATypeValueException e)
                        {
                            binaryConversion.Add(String.Format("Line[{1}]: ERROR -- {0}", e.Message, i+1));
                        }
                    }
                    else if (commandType == "C")
                    {
                        try
                        {
                            binaryConversion.Add("111" + this.parser.Comp() + this.parser.Dest() + this.parser.Jump());
                        }
                        catch (InvalidDestinationException e)
                        {
                            binaryConversion.Add(String.Format("Line[{1}]: ERROR -- {0}", e.Message, i + 1));
                        }
                        catch (InvalidCompException e)
                        {
                            binaryConversion.Add(String.Format("Line[{1}]: ERROR -- {0}", e.Message, i + 1));
                        }
                        catch (InvalidJumpException e)
                        {
                            binaryConversion.Add(String.Format("Line[{1}]: ERROR -- {0}", e.Message, i + 1));
                        }
                    }
                    else // commandType == "L" or "Comment" or "Empty Line"
                    {
                        /* Do nothing ... This will remove the label or comment from the binary
                         * The Label is already processed by the this.parser.ConstructSymbolTable(), 
                         * so leave the code be so that errors and warnings make sense.*/
                    }
                }
                //If command type is none of the above, it is an error.
                catch (InvalidCommandType e) 
                {
                    binaryConversion.Add(e.Message);
                }

                //Checks if there are more commands. If there are not, then break out of the loop.
                if (this.parser.HasMoreCommands(i))
                {
                    this.parser.Advance(i);
                }
                else
                {
                    break;
                }
            }
            return this.parser.GetWarnings().Concat(binaryConversion).ToArray();
            //return binaryConversion.ToArray();
        }

        //END OF FILE

        /// <DEPRECATED CODE GOES BELOW>  ================================================================================
        /// The Following Code was for old implementations. Left here for documentation purposes, as well as reference.
        /// </DEPRECATED CODE GOES BELOW> ================================================================================

        /* Deprecated Code. Used to remove all white space before assembling. However, this  
         * caused warnings to not line up with appropriate code lines in .asm file.*/
        [Obsolete]
        private string[] WhiteSpaceFilter(string[] asmFileWhite)
        {
            int i = 0;

            //By making this a list, it allows for the removal of dead lines.
            List<string> filteredFile = new List<string>();

            //iterate through each value in the file.
            for (i = 0; i < asmFileWhite.Length; i++)
            {
                //This acts as our dummy variable.
                string filteredLine = asmFileWhite[i];

                //Remove all unecessary white space
                filteredLine = filteredLine.Replace(" ", "");

                //Check if line is empty line, if it is, go to next line. Else, replace all internal whitespace.
                if (true == string.IsNullOrEmpty(filteredLine) || filteredLine.IndexOf("/") == 0)
                {
                    continue;
                }

                if (filteredLine.Contains("/"))
                {
                    //CITE: This quick way of removing comments was found at:
                    //https://www.codeproject.com/Questions/412064/trim-string-in-csharp-after-specific-character
                    filteredLine = filteredLine.Substring(0, filteredLine.IndexOf("/"));
                }

                //Final filtered line is added to list
                filteredFile.Add(filteredLine.ToString());
            }

            //List is returned as a new array... The index will act as the lines of the cleared.
            return filteredFile.ToArray();
        }
    }
}
