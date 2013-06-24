using AdvertBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdvertBaseTests
{
    
    
    /// <summary>
    ///This is a test class for EditControlTest and is intended
    ///to contain all EditControlTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EditControlTest
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
        ///A test for townList_TextChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void townList_TextChangedTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.townList_TextChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for townList_SelectedIndexChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void townList_SelectedIndexChangedTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.townList_SelectedIndexChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for secPhone_TextChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void secPhone_TextChangedTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.secPhone_TextChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for loadCard
        ///</summary>
        [TestMethod()]
        public void loadCardTest()
        {
            EditControl target = new EditControl(); // TODO: Initialize to an appropriate value
            string id = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.loadCard(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for kod_r_ValueChanged
        ///</summary>
        [TestMethod()]
        public void kod_r_ValueChangedTest()
        {
            EditControl target = new EditControl(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.kod_r_ValueChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for kod_r_TextChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void kod_r_TextChangedTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.kod_r_TextChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for kod_pppr_ValueChanged
        ///</summary>
        [TestMethod()]
        public void kod_pppr_ValueChangedTest()
        {
            EditControl target = new EditControl(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.kod_pppr_ValueChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for k_word_TextChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void k_word_TextChangedTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.k_word_TextChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for cardPanel_Leave
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardPanel_LeaveTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardPanel_Leave(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for cardPanel_Enter
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardPanel_EnterTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardPanel_Enter(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button1_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void button1_ClickTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button1_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for automobilesList_SelectedIndexChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void automobilesList_SelectedIndexChangedTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.automobilesList_SelectedIndexChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UnSetSelected
        ///</summary>
        [TestMethod()]
        public void UnSetSelectedTest()
        {
            EditControl target = new EditControl(); // TODO: Initialize to an appropriate value
            target.UnSetSelected();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SetSelected
        ///</summary>
        [TestMethod()]
        public void SetSelectedTest()
        {
            EditControl target = new EditControl(); // TODO: Initialize to an appropriate value
            target.SetSelected();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SaveAndClose
        ///</summary>
        [TestMethod()]
        public void SaveAndCloseTest()
        {
            EditControl target = new EditControl(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.SaveAndClose();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void InitializeComponentTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for EditControl_Leave
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void EditControl_LeaveTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.EditControl_Leave(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for EditControl_Enter
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void EditControl_EnterTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.EditControl_Enter(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void DisposeTest()
        {
            EditControl_Accessor target = new EditControl_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for EditControl Constructor
        ///</summary>
        [TestMethod()]
        public void EditControlConstructorTest()
        {
            EditControl target = new EditControl();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
