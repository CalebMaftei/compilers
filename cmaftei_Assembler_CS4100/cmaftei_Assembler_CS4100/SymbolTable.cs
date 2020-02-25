using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100
{
    //This class creates an instance that collects symbol for the first pass-through of the data.
    public class SymbolTable
    {
        //Data Object that will hold the table.
        private Dictionary<string, int> symbolTable = new Dictionary<string, int>();
        private int nextAvailableRamLocation = 16;

        public SymbolTable()
        {
            //0-param constructor -- Creates a Cache of Main Memory... these values either point to line numbers, or point to variable homes in M.M.
            initializeTable();
        }

        //Adds a new entry into the system table
        public void addEntry(string symbol, int address)
        {
            symbolTable.Add(symbol, address);
        }
        
        //Overrided operation to allow only 1 parameter... this is used for variables in the .asm code.
        public void addEntry(string symbol)
        {
            symbolTable.Add(symbol, this.nextAvailableRamLocation);
            this.nextAvailableRamLocation++;
        }

        public bool contains(string address)
        {
            return symbolTable.ContainsKey(address);
        }

        public int getAddress(string symbol)
        {
            return this.symbolTable[symbol];
        }

        private void initializeTable()
        {
            //Sets up the table by creating intial values
            addEntry("SP", 0);
            addEntry("LCL", 1);
            addEntry("ARG", 2);
            addEntry("THIS", 3);
            addEntry("THAT", 4);
            for(int i = 0;  i < 16; i++)
            {
                addEntry("R" + i, i);
            }
            addEntry("SCREEN", 16384);
            addEntry("KBD", 24576);
        }

        //In case the user wants to see the Symbol Table
        public override string ToString()
        {
            string returnString = "SYMBOL TABLE:\n\n";
            foreach(KeyValuePair<string, int> entry in symbolTable)
            {
                returnString += entry.Key + ", " + entry.Value + "\n";
            }
            return returnString; //Change this to print the symbol table instead
        }

        
    }
}
