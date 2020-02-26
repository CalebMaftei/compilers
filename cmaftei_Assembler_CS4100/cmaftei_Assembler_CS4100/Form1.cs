using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cmaftei_Assembler_CS4100
{
    public partial class Form_Cmaftei_Assembler : Form
    {

        private string[] asmFile;
        private AssemblyConductor assemblyConductor;

        public Form_Cmaftei_Assembler()
        {
            InitializeComponent();
        }

        //Once button is clicked, the function runs in the background to allow the fixed binary to be seen.
        private void btn_assemble_Click(object sender, EventArgs e)
        {
            if (richTxt_asmFileContents.Text == "")
            {
                MessageBox.Show("ERROR: No .asm file to assemble.\nPlease Place In Contents of .asm file to Assemble.");
            }
            else
            {
                asmFile = richTxt_asmFileContents.Lines;
                assemblyConductor = new AssemblyConductor(asmFile);

                richTxt_Binary.Lines = assemblyConductor.Assemble();

                btn_showSymbolTable.Enabled = true;
            }
        }

        private void btn_showSymbolTable_Click(object sender, EventArgs e)
        {
            MessageBox.Show(assemblyConductor.GetSymbolTable().ToString());
        }
    }
}
