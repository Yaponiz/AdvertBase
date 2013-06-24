using AdvertBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdvertBaseTests
{
    
    
    /// <summary>
    ///This is a test class for SearcResultsTest and is intended
    ///to contain all SearcResultsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SearcResultsTest
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
        ///A test for cardList_DoubleClick
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardList_DoubleClickTest()
        {
            SearcResults_Accessor target = new SearcResults_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardList_DoubleClick(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void InitializeComponentTest()
        {
            SearcResults_Accessor target = new SearcResults_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void DisposeTest()
        {
            SearcResults_Accessor target = new SearcResults_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SearcResults Constructor
        ///</summary>
        [TestMethod()]
        public void SearcResultsConstructorTest()
        {
            SearcResults target = new SearcResults();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
