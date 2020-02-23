using Xunit;
using cmaftei_Assembler_CS4100;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Assembler_CS4100.Tests
{
    public class AssemblyConductorTests
    {
        [Fact()]
        public void assembleTest()
        {
            //bool hypothesisValue;
            string[] fileWithWhiteSpace = new string[5] {"a b c d e// stuff and what not","aaaaa bbb cccc d","","//This is a comment","mail box" };
            string[] fileWithOutWhiteSpace = new string[3] {"abcde", "aaaaabbbccccd", "mailbox"};
            AssemblyConductor assemblyConductor = new AssemblyConductor(fileWithWhiteSpace);

            string[] filteredFile = assemblyConductor.assemble();

            Assert.Equal(3, filteredFile.Length);
            Assert.Equal("abcde", filteredFile[0]);
            Assert.Equal("aaaaabbbccccd", filteredFile[1]);
            Assert.Equal("mailbox", filteredFile[2]);
            Assert.Equal(fileWithOutWhiteSpace, filteredFile);
        }
    }
}