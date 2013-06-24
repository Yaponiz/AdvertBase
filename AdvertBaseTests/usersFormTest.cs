using AdvertBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdvertBaseTests
{
    
    
    /// <summary>
    ///This is a test class for usersFormTest and is intended
    ///to contain all usersFormTest Unit Tests
    ///</summary>
    [TestClass()]
    public class usersFormTest
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
        ///A test for usersForm Constructor
        ///</summary>
        [TestMethod()]
        public void usersFormConstructorTest()
        {
            usersForm target = new usersForm();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void DisposeTest()
        {
            usersForm_Accessor target = new usersForm_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void InitializeComponentTest()
        {
            usersForm_Accessor target = new usersForm_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton1_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton1_ClickTest()
        {
            usersForm_Accessor target = new usersForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton1_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton2_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton2_ClickTest()
        {
            usersForm_Accessor target = new usersForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton2_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for usersForm_Load
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void usersForm_LoadTest()
        {
            usersForm_Accessor target = new usersForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.usersForm_Load(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
