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

        //Data Properties
        private string[] asmFile;
 
        public AssemblyConductor(string[] newAsmFile)
        {
            this.asmFile = newAsmFile;
        }

        //Getters -- Might not need these.
        public string[] getAsmFile()
        {
            return this.asmFile;
        }

        public SymbolTable getSymbolTable()
        {
            return parser.getSymbolTable();
        }

        //The main Meat of This Code
        public string[] assemble()
        {
            //[CHECK!] := White Space Filter := Go through the Code and remove blank lines, comments, and white space
            string[] cleanAsmFile = WhiteSpaceFilter(asmFile);

            parser = new Parser(cleanAsmFile);

            /*[CHECK!] :=Symbol Table Pass Through := Go through the .asm file and find all Labels (label)
                and place them in symbol table. Remove these lines after processing.*/
            string[] binaryRepresentation = parser.ConstructSymbolTable();

            //[ ] := Second Pass (Use the Symbol Table to go through a second time and jump when appropriate)
            //binaryRepresentation = SecondPass(binaryRepresentation);

            //Return the Binary Representation of the .asm file.
            return binaryRepresentation;
        }

        //Removes all white space, blank lines, and comments from .asm file to produce pure hack assembly.
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

                if(filteredLine.Contains("/"))
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

        //Parses clean code to produce a binary file by parsing and translating via the CODE and PARSER class
        private string[] SecondPass(string[] asmFile)
        {
            for (int i = 0; i < this.parser.getAsmFile().Length; i++) //iterate through each line in parser.
            {

            }
            //temporary placeholder.
            return new string[0];
        }
    }
}
