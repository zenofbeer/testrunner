using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using net.PaulChristensen.TestHarnessLib.Util;

namespace TestHarnessLibTests.Util
{
    public class StringHelperTestscs
    {
        [Fact]
        public void IsVariableReturnsTrueTest()
        {
            string value = "${ImAVariable}";

            bool actual = StringHelper.IsVariable(value);

            Assert.True(actual);
        }

        [Fact]
        public void IsVariableReturnsFalseTest()
        {
            string value = "${ImNotAVariable";

            bool actual = StringHelper.IsVariable(value);

            Assert.False(actual);
        }

        [Fact]
        public void StripVariableDelimitersTest()
        {
            var actual = "${ImAVariable}";
            var expected = "ImAVariable";
            StringHelper.StripVariableDelimiters(ref actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StripVariableDelimitersEmptyVariable()
        {
            var actual = "${}";
            StringHelper.StripVariableDelimiters(ref actual);
            Assert.Equal(string.Empty, actual);
        }

        [Fact]
        public void StripVariableDelimitersStringNotAVariableTest()
        {
            var actual = "ImNotAVariable";
            Assert.Throws(typeof (ApplicationException), () => StringHelper.StripVariableDelimiters(ref actual));
        }
    }
}
