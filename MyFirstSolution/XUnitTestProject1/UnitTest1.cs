using MyFirstClassLibrary;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TrueIsTrue()
        {
            Assert.True(true);
        }

        [Fact]
        public void GetWordsWorks()
        {
            Assert.Equal(2, Helper.GetWords("Hello world!").Count);
        }

        [Fact]
        public void GetWordsThrowsForNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => Helper.GetWords(null) );
        }
    }
}
