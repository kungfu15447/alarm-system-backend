using System;
using Xunit;

namespace Test
{
    public class Class1
    {
        [Fact]
        public void PassingTest() {
            Assert.Equal(4, 4);
        }
    }
}
