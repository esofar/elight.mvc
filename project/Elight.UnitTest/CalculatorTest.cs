using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Elight.UnitTest
{
    [TestFixture]
    public class CalculatorTest
    {
        /// <summary>
        /// 测试DEMO。
        /// </summary>
        [Test]
        public void TestAdd()
        {
            Calculator cal = new Calculator();
            int expected = 5;
            int actual = cal.Add(2, 3);
            Assert.AreEqual(expected, actual);
        }
    }  
}
