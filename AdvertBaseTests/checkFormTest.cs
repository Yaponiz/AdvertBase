using AdvertBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdvertBaseTests
{
    
    
    /// <summary>
    ///This is a test class for checkFormTest and is intended
    ///to contain all checkFormTest Unit Tests
    ///</summary>
    [TestClass()]
    public class checkFormTest
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
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void InitializeComponentTest()
        {
            checkForm_Accessor target = new checkForm_Accessor(); // TODO: Initialize to an appropriate value
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
            checkForm_Accessor target = new checkForm_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for checkForm Constructor
        ///</summary>
        [TestMethod()]
        public void checkFormConstructorTest()
        {
            checkForm target = new checkForm();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for checkForm_Load
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void checkForm_LoadTest()
        {
            checkForm_Accessor target = new checkForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.checkForm_Load(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for проверитьВсеToolStripMenuItem_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void проверитьВсеToolStripMenuItem_ClickTest()
        {
            checkForm_Accessor target = new checkForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.проверитьВсеToolStripMenuItem_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
