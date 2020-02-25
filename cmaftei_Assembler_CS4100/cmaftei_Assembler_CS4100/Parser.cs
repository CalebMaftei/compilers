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
        private List<string> warnings = new List<string>();

        //Constructor - Requires the .asm file to parse anything.
        public Parser(string[] newAsmFile)
        {
            this.asmFile = newAsmFile;
            this.currentInstruction = this.asmFile[0];
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

        public List<string> getWarnings()
        {
            return warnings;
        }

        //Checks if there exists another command
        public bool hasMoreCommands(int currentIndex)
        {
            //return true if there exists more commands within the asmFile... Might not need this if iterating through a finite sized array of strings.
            return (currentIndex + 1 >= this.asmFile.Length) ? false : true;
        }

        //Places the next command into question
        public void advance(int romIndex)
        {
            //Reads the next input of the given file. Only do this if hasMoreCommands returns true. There does not exist a current instruction at first.
            this.currentInstruction = this.asmFile[romIndex + 1];
        }

        //Returns the type of the command
        public string CommmandType()
        {
            //If First character in current string is "@", then that implies the instruction is A
            if(this.currentInstruction[0] == '@')
            {
                return "A";
            }
            //Since the Label Instruction is created via the Assembly Conductor Class, the only other possible instruction is C type.
            else if(this.currentInstruction.Contains(";") || this.currentInstruction.Contains("="))
            {
                return "C";
            }
            else
            {
                throw new InvalidCommandType(this.currentInstruction, Array.IndexOf(this.asmFile, this.currentInstruction));
            }
        }
        
        //Returns in Binary the Address Space of the Symbol Requested.
        public string symbol()
        {
            string symbol = "";
            string warning = "";
            string binary = "";
            string label = this.currentInstruction.Substring(this.currentInstruction.IndexOf("@") + 1);

            if (label == "")
            {
                throw new IllegalATypeValueException(label);
            }
            else if (this.symbolTable.contains(label))
            {
                binary = Convert.ToString(this.symbolTable.getAddress(label), 2);
                for (int i = 0; i < 16 - binary.Length; i++)
                {
                    symbol += "0";
                }
                symbol += binary;
            }
            else if (int.TryParse(label, out int labelNum))
            {
                if(label.Contains("0") || label.Contains("1") && !label.Contains("2") && !label.Contains("3") && !label.Contains("4") && !label.Contains("5")
                    && !label.Contains("6") && !label.Contains("7") && !label.Contains("8") && !label.Contains("9"))
                {
                    warning = "\nLine["+ Array.IndexOf(this.asmFile, this.currentInstruction) +"]: WARNING Binary Number is considered an Int unless specified in the form: @0bXXX";
                }
                binary = Convert.ToString(labelNum, 2);
                if (labelNum < 0 || binary.Length > 15)
                {
                    //If the label is negative, or larger than 15 bits, then, send an error.
                    throw new IllegalATypeValueException(label);
                }
                for (int i = 0; i < 16 - binary.Length; i++)
                {
                    symbol += "0";
                }
                symbol += binary;
            }
            else if (label.Length > 2) //implying that there has to exist at least 3 character => 0x or 0b (if the label is 0x____) this will bomb out.
            {
                if(label.Substring(2).Length == 0)
                {
                    throw new IllegalATypeValueException(label); //As no value following the hex or bin identifier is a blank value.
                }
                //hex
                if (label.Substring(0, 2).ToLower() == "0x" && int.TryParse(label.Substring(2), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out int labelHexNum))
                {
                    binary = Convert.ToString(labelHexNum, 2);
                    if (labelNum < 0 || binary.Length > 15)
                    {
                        //If the label is negative, or larger than 15 bits, then, send an error.
                        throw new IllegalATypeValueException(label);
                    }
                    for (int i = 0; i < 16 - binary.Length; i++)
                    {
                        symbol += "0";
                    }
                    symbol += binary;
                }
                //binary
                else if (label.Substring(0, 2).ToLower() == "0b")
                {
                    binary = label.Substring(2);
                    if (labelNum < 0 || binary.Length > 15)
                    {
                        //If the label is negative, or larger than 15 bits, then, send an error.
                        throw new IllegalATypeValueException(label);
                    }
                    for (int i = 0; i < 16 - binary.Length; i++)
                    {
                        symbol += "0";
                    }
                    symbol += binary;
                }
                else //If symbol doesn't exist, then add a new entry, and return the new address in binary.
                {
                    this.symbolTable.addEntry(label);
                    binary = Convert.ToString(this.symbolTable.getAddress(label), 2);
                    for (int i = 0; i < 16 - binary.Length; i++)
                    {
                        symbol += "0";
                    }
                    symbol += binary;
                }
            }
            else 
            {
                //already taken care of... need this here for syntax.
                this.symbolTable.addEntry(label);
                binary = Convert.ToString(this.symbolTable.getAddress(label), 2);
                for (int i = 0; i < 16 - binary.Length; i++)
                {
                    symbol += "0";
                }
                symbol += binary;
            }

            //Placing Spaces in for formatting purposes.
            symbol = symbol.Insert(4, " ");
            symbol = symbol.Insert(9, " ");
            symbol = symbol.Insert(14, " ");
            return symbol + warning;
        }

        //Return the binary or an error message about the destination of the .asm file.
        public string dest()
        {
            //Issue is that not all dest are from Dest=Comp, sometimes its Dest;Jump commands as well. 
            string destMneumonic = "";
            if (this.currentInstruction.Contains("="))
            {
                destMneumonic = this.currentInstruction.Substring(0, this.currentInstruction.IndexOf("="));
            }
            else //To get to this point, it must be the case that there exists a = or a ; in the current instruction.... any other invalid expressions will be caught in code.
            {
                //if there does not exist a =, then Dest is omitted.
                destMneumonic = "null";
            }
            return code.dest(destMneumonic);
        }

        public string comp()
        {
            string compMneumonic = "";
            if (this.currentInstruction.Contains("="))
            {
                compMneumonic = this.currentInstruction.Substring(this.currentInstruction.IndexOf("=")+1);
            }
            else //To get to this point, it must be the case that there exists a = or a ; in the current instruction.... any other invalid expressions will be caught in code.
            {
                //if there does not exist a =, then Dest is omitted.
                compMneumonic = this.currentInstruction.Substring(0,this.currentInstruction.IndexOf(";"));
            }
            return code.comp(compMneumonic);
        }

        public string jump()
        {
            string jumpMneumonic = "";
            if (this.currentInstruction.Contains("="))
            {
                //If the command contains an "=" then it means that no jump will occur.
                jumpMneumonic = "null";
            }
            else //To get to this point, it must be the case that there exists a = or a ; in the current instruction.... any other invalid expressions will be caught in code.
            {
                jumpMneumonic = this.currentInstruction.Substring(this.currentInstruction.IndexOf(";") + 1);
            }
            return code.jump(jumpMneumonic);
        }

        //Should occur after the .asm File has had its white space, return lines, and comments removed.
        public void ConstructSymbolTable()
        {
            this.asmFile.ToList();
            List<string> noLabelAsmFile = new List<string>();

            int index = 0;
            foreach (string str in this.asmFile)
            {
                if (str.IndexOf("(") == 0) //implies that the line is a label
                {
                    string tempStr = str.Substring(1, str.IndexOf(")") - 1);
                    try
                    {
                        if (str.Substring(str.IndexOf(")")).Length > 0)
                        {
                            throw new IllegalLabelException(str);
                        }
                        this.symbolTable.addEntry(tempStr, index);
                    }
                    catch (IllegalLabelException e)
                    {
                        this.warnings.Add("Line[" + index + "]: WARNING -" + e.Message);
                        this.symbolTable.addEntry(tempStr, index);
                    }                    
                }
                else
                {
                    noLabelAsmFile.Add(str);
                    index++;
                }                
            }
            this.asmFile = noLabelAsmFile.ToArray();

            if(this.asmFile.Length != 0)
            {
                this.currentInstruction = this.asmFile[0];
            }            
        }
    }
}
