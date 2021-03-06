﻿using System;
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
        private int numberOfUncountedLines = 0;

        //Constructor - Requires the .asm file to parse anything.
        public Parser(string[] newAsmFile)
        {
            this.asmFile = newAsmFile;
            this.currentInstruction = this.asmFile[0];
        }

        //getters and setters
        public string[] GetAsmFile()
        {
            return this.asmFile;
        }

        //Getter that retrieves symbol table
        public SymbolTable GetSymbolTable()
        {
            return this.symbolTable;
        }
        
        //Getter that retrieves all warnings found while parsing
        public List<string> GetWarnings()
        {
            return warnings;
        }

        //Checks if there exists another command
        public bool HasMoreCommands(int currentIndex)
        {
            //return true if there exists more commands within the asmFile... Might not need this if iterating through a finite sized array of strings.
            return (currentIndex + 1 >= this.asmFile.Length) ? false : true;
        }

        //Places the next command into question
        public void Advance(int romIndex)
        {
            //Reads the next input of the given file. Only do this if hasMoreCommands returns true. There does not exist a current instruction at first.
            this.currentInstruction = this.asmFile[romIndex + 1];
        }

        //Returns the type of the command
        public string CommmandType()
        {
            if (string.IsNullOrEmpty(this.currentInstruction))
            {
                return "empty line";
            }
            //If First character in current string is "@", then that implies the instruction is A
            else if (this.currentInstruction[0] == '@')
            {
                return "A";
            }
            else if(this.currentInstruction.Contains(".EQU"))
            {
                return "EQU";
            }
            //Since the Label Instruction is created via the Assembly Conductor Class, the only other possible instruction is C type.
            else if (this.currentInstruction.Contains(";") || this.currentInstruction.Contains("="))
            {
                return "C";
            }
            else if (this.currentInstruction[0] == '(')
            {
                return "L";
            }
            else if (this.currentInstruction[0] == '/' && this.currentInstruction[1] == '/')
            {
                return "Comment";
            }
            else //The current command is none of the above allowed types, therefore throw an error.
            {
                throw new InvalidCommandType(this.currentInstruction, Array.IndexOf(this.asmFile, this.currentInstruction));
            }
        }
        
        //Returns in Binary the Address Space of the Symbol Requested.
        public string Symbol()
        {
            string symbol = "";
            string warning = "";
            string binary = "";
            string label = this.currentInstruction.Substring(this.currentInstruction.IndexOf("@") + 1);

            //If Label is empty, throw an exception
            if (label == "")
            {
                throw new IllegalATypeValueException(label, Array.IndexOf(this.asmFile, this.currentInstruction) + 1);
            }
            //If the symbol is already in the table, fetch th value
            else if (this.symbolTable.contains(label)) 
            {
                binary = Convert.ToString(this.symbolTable.getAddress(label), 2);
                for (int i = 0; i < 16 - binary.Length; i++)
                {
                    symbol += "0";
                }
                symbol += binary;
            }
            //If the symbol is an integer, convert it to binary
            else if (int.TryParse(label, out int labelNum))
            {
                if((label.Contains("0") || label.Contains("1")) && (!label.Contains("2") && !label.Contains("3") && !label.Contains("4")
                    && !label.Contains("5")  && !label.Contains("6") && !label.Contains("7") && !label.Contains("8") 
                    && !label.Contains("9")))
                {
                    warning = "\nLine["+ Array.IndexOf(this.asmFile, this.currentInstruction) +"]: WARNING Binary Number is considered an Int unless specified in the form: @0bXXX";
                }
                binary = Convert.ToString(labelNum, 2);
                if (labelNum < 0 || binary.Length > 15)
                {
                    //If the label is negative, or larger than 15 bits, then, send an error.
                    throw new IllegalATypeValueException(label, Array.IndexOf(this.asmFile, this.currentInstruction) + 1);
                }
                for (int i = 0; i < 16 - binary.Length; i++)
                {
                    symbol += "0";
                }
                symbol += binary;
            }
            //If the symbol has a length > 2 => it could be a binary value, or a Hex value ( 0x_ or 0b_ )
            else if (label.Length > 2)
            {
                /*
                if(label.Substring(2).Length == 0)
                {
                    throw new IllegalATypeValueException(label, Array.IndexOf(this.asmFile, this.currentInstruction) + 1);
                }*/
                //hex
                if (label.Substring(0, 2).ToLower() == "0x" && int.TryParse(label.Substring(2), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out int labelHexNum))
                {
                    binary = Convert.ToString(labelHexNum, 2);
                    if (labelNum < 0 || binary.Length > 15)
                    {
                        //If the label is negative, or larger than 15 bits, then, send an error.
                        throw new IllegalATypeValueException(label, Array.IndexOf(this.asmFile, this.currentInstruction) + 1);
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
                        throw new IllegalATypeValueException(label, Array.IndexOf(this.asmFile, this.currentInstruction) + 1);
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
        public string Dest()
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

        //Return the binary of the given comp
        public string Comp()
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

        //Return the binary of the given jump
        public string Jump()
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

        //Builds the Symbol Table
        public void ConstructSymbolTable()
        {
            for(int i = 0; i < this.asmFile.Length; i++)
            {
                //Removes all comments, if they exist, that are in-line
                if (this.asmFile[i].Contains("//"))
                {
                    this.asmFile[i] = this.asmFile[i].Substring(0, this.asmFile[i].IndexOf("/"));
                }

                if (this.asmFile[i].Contains(".EQU"))
                {
                    //Command is EQU, check if format is correct.
                    try
                    {
                        string tempString = this.asmFile[i].Trim();
                        string[] equ = tempString.Split(' ');
                        int value = 0;

                        //If the value has more than 2 characters, could be hex or bin.
                        if(equ[2].Length > 2)
                        {
                            //EQU is a hex value
                            if (equ[2].Substring(0, 2).ToLower() == "0x")
                            {
                                if (!int.TryParse(equ[2].Substring(2), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out value))
                                {
                                    throw new Exception(equ[2] + " is not an appropriate hex value");
                                }
                                this.symbolTable.addEntry(equ[1], value);
                                this.numberOfUncountedLines++;
                                continue;
                            }
                            //EQU is a binary value
                            else if (equ[2].Substring(0, 2).ToLower() == "0b")
                            {
                                ///binary = equ[2].Substring(2);
                                if (equ[2].Substring(2).Length < 0 || equ[2].Substring(2).Length > 15)
                                {
                                    //If the label is negative, or larger than 15 bits, then, send an error.
                                    throw new Exception(String.Format(" \"{0}\" is an illegal value. Label values must be less than 16 bits, and non-negative.",equ[2]));
                                }
                                else if(equ[2].Substring(2).Contains("2") || equ[2].Substring(2).Contains("3") || equ[2].Substring(2).Contains("4") || equ[2].Substring(2).Contains("5") || equ[2].Substring(2).Contains("6") || equ[2].Substring(2).Contains("7") || equ[2].Substring(2).Contains("8") || equ[2].Substring(2).Contains("9"))
                                {
                                    throw new Exception(equ[2] + " is not an appropriate binary value");
                                }
                                value = BinaryConvertToInt(equ[2].Substring(2));
                                this.symbolTable.addEntry(equ[1], value);
                                this.numberOfUncountedLines++;
                                continue;
                            }
                        }

                        //Value is not an integer
                        if(!int.TryParse(equ[2], out value))
                        {
                            this.numberOfUncountedLines++;
                            throw new Exception("\"" + equ[2] + "\"" + " is not an appropriate integer value");
                        }

                        value = int.Parse(equ[2]);

                        //If value is negative or zero, throw an exception.
                        if (value > 32767 || value < 0)
                        {
                            throw new Exception(equ[2] + " has to be a non-negative value and less than 32767.");
                        }
                        this.symbolTable.addEntry(equ[1], value);
                        this.numberOfUncountedLines++;
                        continue;
                    }
                    catch(Exception e)
                    {
                        this.warnings.Add("Line[" + i + "]: ERROR -- " + e.Message);
                        this.numberOfUncountedLines++;
                        continue;
                    }
                }

                //Removes all whitespace... This is done after 
                this.asmFile[i] = this.asmFile[i].Replace(" ", "");

                //If line begins with "(" it is a label.
                if (this.asmFile[i].IndexOf("(") == 0) 
                {
                    string tempStr = this.asmFile[i].Substring(1, this.asmFile[i].IndexOf(")") - 1);
                    try
                    {
                        //If there exists any characters after the label, then send a warning. No comments should exist at this point.
                        string comment = this.asmFile[i].Substring(this.asmFile[i].IndexOf(")") + 1);
                        if (comment.Length > 0)
                        {
                            //if non-commented text exists, throw this error
                            throw new IllegalLabelException(this.asmFile[i], Array.IndexOf(this.asmFile, this.currentInstruction) + 1);
                        }

                        //if label is legal, don't throw a warning, and add it to table. i+1 because ROM starts at 1, not 0.
                        this.symbolTable.addEntry(tempStr, i - this.numberOfUncountedLines);
                        this.numberOfUncountedLines++;
                    }
                    catch (IllegalLabelException e)
                    {
                        //if illegal label, throw a warning and add to table. i+1 because ROM starts at 1, not 0.
                        this.warnings.Add("Line[" + i + "]: WARNING -- " + e.Message);
                        this.symbolTable.addEntry(tempStr, i - this.numberOfUncountedLines);
                        this.numberOfUncountedLines++;
                    }
                    catch (IllegalLabelRedefinitionException e)
                    {
                        this.warnings.Add("Line[" + i + "]: WARNING -- " + e.Message);
                    }
                }
                //Check if 
                else if(String.IsNullOrEmpty(this.asmFile[i]) || (this.asmFile[i][0] == '/' && this.asmFile[i][1] == '/'))
                {
                    this.numberOfUncountedLines++;
                }
            }
        }

        //Simplifies the current instruction by stripping all in-line comments, and white space.
        public void CleanCurrentLine()
        {
            this.currentInstruction = this.currentInstruction.Replace(" ", "");
            if (this.currentInstruction.Contains('/'))
            {
                this.currentInstruction = this.currentInstruction.Substring(0, this.currentInstruction.IndexOf("/"));
            }
        }

        //Converts a Binary number into a Decimal Value
        private int BinaryConvertToInt(string binaryNum)
        {
            double returnValue = 0;
            for(int i = 0; i < binaryNum.Length; i++)
            {
                returnValue += int.Parse(binaryNum[(binaryNum.Length-1)- i].ToString())*Math.Pow(2,i);
            }
            return Convert.ToInt32(returnValue);
        }

        //END OF FILE


        /// <DEPRECATED CODE GOES BELOW>  ================================================================================
        /// The Following Code was for old implementations. Left here for documentation purposes, as well as reference.
        /// </DEPRECATED CODE GOES BELOW> ================================================================================
        
        //Deprecated Code -- Old Construct Symbol Table Function
        [Obsolete]
        public void ConstructSymbolTable1()
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
                        string comment = str.Substring(str.IndexOf(")") + 1);
                        if (comment.Length > 0)
                        {
                            //throw new IllegalLabelException(str);
                        }
                        this.symbolTable.addEntry(tempStr, index);
                    }
                    catch (IllegalLabelException e)
                    {
                        this.warnings.Add("Line[" + index + "]: WARNING -- " + e.Message);
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

            if (this.asmFile.Length != 0)
            {
                this.currentInstruction = this.asmFile[0];
            }
        }
    }
}
