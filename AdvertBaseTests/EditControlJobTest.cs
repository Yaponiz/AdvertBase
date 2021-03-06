﻿using AdvertBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdvertBaseTests
{
    
    
    /// <summary>
    ///This is a test class for EditControlJobTest and is intended
    ///to contain all EditControlJobTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EditControlJobTest
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
        ///A test for label3_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void label3_ClickTest()
        {
            EditControlJob_Accessor target = new EditControlJob_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.label3_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for cardPanel_Leave
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardPanel_LeaveTest()
        {
            EditControlJob_Accessor target = new EditControlJob_Accessor(); // TODO: Initialize to an appropriate value
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
            EditControlJob_Accessor target = new EditControlJob_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardPanel_Enter(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void InitializeComponentTest()
        {
            EditControlJob_Accessor target = new EditControlJob_Accessor(); // TODO: Initialize to an appropriate value
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
            EditControlJob_Accessor target = new EditControlJob_Accessor(); // TODO: Initialize to an appropriate value
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
            EditControlJob_Accessor target = new EditControlJob_Accessor(); // TODO: Initialize to an appropriate value
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
            EditControlJob_Accessor target = new EditControlJob_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for EditControlJob Constructor
        ///</summary>
        [TestMethod()]
        public void EditControlJobConstructorTest()
        {
            string id = string.Empty; // TODO: Initialize to an appropriate value
            EditControlJob target = new EditControlJob(id);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for EditControlJob Constructor
        ///</summary>
        [TestMethod()]
        public void EditControlJobConstructorTest1()
        {
            EditControlJob target = new EditControlJob();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
