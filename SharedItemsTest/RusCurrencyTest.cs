using RSDN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharedItemsTest
{
    
    
    /// <summary>
    ///This is a test class for RusCurrencyTest and is intended
    ///to contain all RusCurrencyTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RusCurrencyTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for RusCurrency Constructor
        ///</summary>
        [TestMethod()]
        public void RusCurrencyConstructorTest()
        {
            RusCurrency target = new RusCurrency();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Register
        ///</summary>
        [TestMethod()]
        public void RegisterTest()
        {
            string currency = string.Empty; // TODO: Initialize to an appropriate value
            bool male = false; // TODO: Initialize to an appropriate value
            string seniorOne = string.Empty; // TODO: Initialize to an appropriate value
            string seniorTwo = string.Empty; // TODO: Initialize to an appropriate value
            string seniorFive = string.Empty; // TODO: Initialize to an appropriate value
            string juniorOne = string.Empty; // TODO: Initialize to an appropriate value
            string juniorTwo = string.Empty; // TODO: Initialize to an appropriate value
            string juniorFive = string.Empty; // TODO: Initialize to an appropriate value
            RusCurrency.Register(currency, male, seniorOne, seniorTwo, seniorFive, juniorOne, juniorTwo, juniorFive);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Str
        ///</summary>
        [TestMethod()]
        public void StrTest()
        {
            double val = 0F; // TODO: Initialize to an appropriate value
            string currency = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = RusCurrency.Str(val, currency);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Str
        ///</summary>
        [TestMethod()]
        public void StrTest1()
        {
            double val = 0F; // TODO: Initialize to an appropriate value
            bool male = false; // TODO: Initialize to an appropriate value
            string seniorOne = string.Empty; // TODO: Initialize to an appropriate value
            string seniorTwo = string.Empty; // TODO: Initialize to an appropriate value
            string seniorFive = string.Empty; // TODO: Initialize to an appropriate value
            string juniorOne = string.Empty; // TODO: Initialize to an appropriate value
            string juniorTwo = string.Empty; // TODO: Initialize to an appropriate value
            string juniorFive = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = RusCurrency.Str(val, male, seniorOne, seniorTwo, seniorFive, juniorOne, juniorTwo, juniorFive);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Str
        ///</summary>
        [TestMethod()]
        public void StrTest2()
        {
            double val = 0F; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = RusCurrency.Str(val);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
