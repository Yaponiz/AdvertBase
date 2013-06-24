using AdvertBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharedItemsTest
{
    
    
    /// <summary>
    ///This is a test class for propertiesTest and is intended
    ///to contain all propertiesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class propertiesTest
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
        ///A test for properties Constructor
        ///</summary>
        [TestMethod()]
        public void propertiesConstructorTest()
        {
            properties target = new properties();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SharedItems.dll")]
        public void DisposeTest()
        {
            properties_Accessor target = new properties_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SharedItems.dll")]
        public void InitializeComponentTest()
        {
            properties_Accessor target = new properties_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button1_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SharedItems.dll")]
        public void button1_ClickTest()
        {
            properties_Accessor target = new properties_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button1_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for textBox2_TextChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SharedItems.dll")]
        public void textBox2_TextChangedTest()
        {
            properties_Accessor target = new properties_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.textBox2_TextChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
