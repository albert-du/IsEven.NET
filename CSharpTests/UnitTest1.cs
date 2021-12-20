using Xunit;
using static IsEven;

namespace CSharpTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1() =>
            // Just check to make sure it's accessible from C#.
            Assert.False(IsEven(1).Even);
    }
}