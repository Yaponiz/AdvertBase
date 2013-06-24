using AdvertBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using System.Xml;

namespace AdvertBaseTests
{
    
    
    /// <summary>
    ///This is a test class for authuserTest and is intended
    ///to contain all authuserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class authuserTest
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
        ///A test for pass_KeyUp
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void pass_KeyUpTest()
        {
            authuser_Accessor target = new authuser_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            KeyEventArgs e = null; // TODO: Initialize to an appropriate value
            target.pass_KeyUp(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for label2_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void label2_ClickTest()
        {
            authuser_Accessor target = new authuser_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.label2_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button2_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void button2_ClickTest()
        {
            authuser_Accessor target = new authuser_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button2_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button1_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void button1_ClickTest()
        {
            authuser_Accessor target = new authuser_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button1_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for addAtributeToXml
        ///</summary>
        [TestMethod()]
        public void addAtributeToXmlTest()
        {
            authuser target = new authuser(); // TODO: Initialize to an appropriate value
            XmlTextWriter t = null; // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            string text = string.Empty; // TODO: Initialize to an appropriate value
            target.addAtributeToXml(t, name, text);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void InitializeComponentTest()
        {
            authuser_Accessor target = new authuser_Accessor(); // TODO: Initialize to an appropriate value
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
            authuser_Accessor target = new authuser_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for authuser Constructor
        ///</summary>
        [TestMethod()]
        public void authuserConstructorTest()
        {
            authuser target = new authuser();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
