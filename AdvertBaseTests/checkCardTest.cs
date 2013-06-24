using AdvertBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace AdvertBaseTests
{
    
    
    /// <summary>
    ///This is a test class for checkCardTest and is intended
    ///to contain all checkCardTest Unit Tests
    ///</summary>
    [TestClass()]
    public class checkCardTest
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
        ///A test for label1_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void label1_ClickTest()
        {
            checkCard_Accessor target = new checkCard_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.label1_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for checkCard_Load
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void checkCard_LoadTest()
        {
            checkCard_Accessor target = new checkCard_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.checkCard_Load(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for checkCard_KeyUp
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void checkCard_KeyUpTest()
        {
            checkCard_Accessor target = new checkCard_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            KeyEventArgs e = null; // TODO: Initialize to an appropriate value
            target.checkCard_KeyUp(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void InitializeComponentTest()
        {
            checkCard_Accessor target = new checkCard_Accessor(); // TODO: Initialize to an appropriate value
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
            checkCard_Accessor target = new checkCard_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for checkCard Constructor
        ///</summary>
        [TestMethod()]
        public void checkCardConstructorTest()
        {
            checkCard target = new checkCard();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for checkCard Constructor
        ///</summary>
        [TestMethod()]
        public void checkCardConstructorTest1()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            int index = 0; // TODO: Initialize to an appropriate value
            checkCard target = new checkCard(text, index);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
