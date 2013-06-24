using AdvertBaseServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdvertBaseServerTests
{
    
    
    /// <summary>
    ///This is a test class for StatisticsTest and is intended
    ///to contain all StatisticsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StatisticsTest
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
        ///A test for Statistics Constructor
        ///</summary>
        [TestMethod()]
        public void StatisticsConstructorTest()
        {
            DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
            Statistics target = new Statistics(date);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Statistics Constructor
        ///</summary>
        [TestMethod()]
        public void StatisticsConstructorTest1()
        {
            Statistics target = new Statistics();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void DisposeTest()
        {
            Statistics_Accessor target = new Statistics_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void InitializeComponentTest()
        {
            Statistics_Accessor target = new Statistics_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Statistics_Load
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void Statistics_LoadTest()
        {
            Statistics_Accessor target = new Statistics_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.Statistics_Load(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for countByDate1_Load
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void countByDate1_LoadTest()
        {
            Statistics_Accessor target = new Statistics_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.countByDate1_Load(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
