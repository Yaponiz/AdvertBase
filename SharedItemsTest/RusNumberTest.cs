using RSDN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharedItemsTest
{
    
    
    /// <summary>
    ///This is a test class for RusNumberTest and is intended
    ///to contain all RusNumberTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RusNumberTest
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
        ///A test for RusNumber Constructor
        ///</summary>
        [TestMethod()]
        public void RusNumberConstructorTest()
        {
            RusNumber target = new RusNumber();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Case
        ///</summary>
        [TestMethod()]
        public void CaseTest()
        {
            int val = 0; // TODO: Initialize to an appropriate value
            string one = string.Empty; // TODO: Initialize to an appropriate value
            string two = string.Empty; // TODO: Initialize to an appropriate value
            string five = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = RusNumber.Case(val, one, two, five);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Str
        ///</summary>
        [TestMethod()]
        public void StrTest()
        {
            int val = 0; // TODO: Initialize to an appropriate value
            bool male = false; // TODO: Initialize to an appropriate value
            string one = string.Empty; // TODO: Initialize to an appropriate value
            string two = string.Empty; // TODO: Initialize to an appropriate value
            string five = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = RusNumber.Str(val, male, one, two, five);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
