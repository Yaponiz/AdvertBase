using AdvertBaseServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using System.ComponentModel;
using MySql.Data.MySqlClient;

namespace AdvertBaseServerTests
{
    
    
    /// <summary>
    ///This is a test class for ExportFormTest and is intended
    ///to contain all ExportFormTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExportFormTest
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
        ///A test for ExportForm Constructor
        ///</summary>
        [TestMethod()]
        public void ExportFormConstructorTest()
        {
            ExportForm target = new ExportForm();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void DisposeTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ExportForm_Load
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void ExportForm_LoadTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.ExportForm_Load(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void InitializeComponentTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PrintAds
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void PrintAdsTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            int[] catalogNum = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdApp = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdAppExpected = null; // TODO: Initialize to an appropriate value
            target.PrintAds(catalogNum, ref wdApp);
            Assert.AreEqual(wdAppExpected, wdApp);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PrintAds8
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void PrintAds8Test()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            int[] catalogNum = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdApp = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdAppExpected = null; // TODO: Initialize to an appropriate value
            target.PrintAds8(catalogNum, ref wdApp);
            Assert.AreEqual(wdAppExpected, wdApp);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PrintAds9
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void PrintAds9Test()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            int[] catalogNum = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdApp = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdAppExpected = null; // TODO: Initialize to an appropriate value
            target.PrintAds9(catalogNum, ref wdApp);
            Assert.AreEqual(wdAppExpected, wdApp);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PrintAdsOB
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void PrintAdsOBTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            int[] catalogNum = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdApp = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdAppExpected = null; // TODO: Initialize to an appropriate value
            target.PrintAdsOB(catalogNum, ref wdApp);
            Assert.AreEqual(wdAppExpected, wdApp);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PrintAdsRIO
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void PrintAdsRIOTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            int[] catalogNum = null; // TODO: Initialize to an appropriate value
            target.PrintAdsRIO(catalogNum);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PrintAdsRSU
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void PrintAdsRSUTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            int first = 0; // TODO: Initialize to an appropriate value
            int second = 0; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdApp = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdAppExpected = null; // TODO: Initialize to an appropriate value
            target.PrintAdsRSU(first, second, ref wdApp);
            Assert.AreEqual(wdAppExpected, wdApp);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PrintAuto
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void PrintAutoTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            int[] catalogNum = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdApp = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdAppExpected = null; // TODO: Initialize to an appropriate value
            target.PrintAuto(catalogNum, ref wdApp);
            Assert.AreEqual(wdAppExpected, wdApp);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ReplaceTextWord
        ///</summary>
        [TestMethod()]
        public void ReplaceTextWordTest()
        {
            ExportForm target = new ExportForm(); // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdApp = null; // TODO: Initialize to an appropriate value
            Microsoft.Office.Interop.Word.Application wdAppExpected = null; // TODO: Initialize to an appropriate value
            string replaceText = string.Empty; // TODO: Initialize to an appropriate value
            string text = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ReplaceTextWord(ref wdApp, replaceText, text);
            Assert.AreEqual(wdAppExpected, wdApp);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateTable
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void UpdateTableTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
            int index = 0; // TODO: Initialize to an appropriate value
            int kol = 0; // TODO: Initialize to an appropriate value
            target.UpdateTable(date, index, kol);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for addCard_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void addCard_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.addCard_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button10_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button10_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button10_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button11_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button11_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button11_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button12_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button12_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button12_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button14_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button14_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button14_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button15_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button15_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button15_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button1_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button1_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button1_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button2_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button2_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button2_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button2_Click_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button2_Click_1Test()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button2_Click_1(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button3_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button3_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button3_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button3_Click_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button3_Click_1Test()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button3_Click_1(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button4_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button4_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button4_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button4_Click_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button4_Click_1Test()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button4_Click_1(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button5_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button5_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button5_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button5_Click_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button5_Click_1Test()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button5_Click_1(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button6_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button6_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button6_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button6_Click_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button6_Click_1Test()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button6_Click_1(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button6_Click_2
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button6_Click_2Test()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button6_Click_2(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button7_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button7_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button7_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button7_Click_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button7_Click_1Test()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button7_Click_1(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button8_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button8_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button8_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button9_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void button9_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button9_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for catalogSelector_SelectedIndexChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void catalogSelector_SelectedIndexChangedTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.catalogSelector_SelectedIndexChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for checkBox1_CheckedChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void checkBox1_CheckedChangedTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.checkBox1_CheckedChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for checkBox2_CheckedChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void checkBox2_CheckedChangedTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.checkBox2_CheckedChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for checkBox3_CheckedChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void checkBox3_CheckedChangedTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.checkBox3_CheckedChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for checkCount
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void checkCountTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            int[] catalogNum = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.checkCount(catalogNum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for comboBox1_SelectedIndexChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void comboBox1_SelectedIndexChangedTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.comboBox1_SelectedIndexChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for dataGridView1_CellContentClick
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void dataGridView1_CellContentClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            DataGridViewCellEventArgs e = null; // TODO: Initialize to an appropriate value
            target.dataGridView1_CellContentClick(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for dataGridView1_CellContentClick_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void dataGridView1_CellContentClick_1Test()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            DataGridViewCellEventArgs e = null; // TODO: Initialize to an appropriate value
            target.dataGridView1_CellContentClick_1(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for getCount
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void getCountTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            target.getCount();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for label1_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void label1_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.label1_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for listBox1_SelectedIndexChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void listBox1_SelectedIndexChangedTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.listBox1_SelectedIndexChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for loadCatalogs
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void loadCatalogsTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            target.loadCatalogs();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for monthCalendar1_DateChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void monthCalendar1_DateChangedTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            DateRangeEventArgs e = null; // TODO: Initialize to an appropriate value
            target.monthCalendar1_DateChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for monthCalendar1_DateSelected
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void monthCalendar1_DateSelectedTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            DateRangeEventArgs e = null; // TODO: Initialize to an appropriate value
            target.monthCalendar1_DateSelected(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for saveFileDialog1_FileOk
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void saveFileDialog1_FileOkTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            CancelEventArgs e = null; // TODO: Initialize to an appropriate value
            target.saveFileDialog1_FileOk(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for saveMainCatalog
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void saveMainCatalogTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            target.saveMainCatalog();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for saveMainCatalogItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void saveMainCatalogItemTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            target.saveMainCatalogItem();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for tabControl1_SelectedIndexChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void tabControl1_SelectedIndexChangedTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.tabControl1_SelectedIndexChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for tabPage1_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void tabPage1_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.tabPage1_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for tabPage2_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void tabPage2_ClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.tabPage2_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for tabPage2_DoubleClick
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void tabPage2_DoubleClickTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.tabPage2_DoubleClick(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for updateCount
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void updateCountTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            int i = 0; // TODO: Initialize to an appropriate value
            target.updateCount(i);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for updateDateTable
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void updateDateTableTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            target.updateDateTable();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for updateQuerry
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void updateQuerryTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            MySqlCommand myCommand = null; // TODO: Initialize to an appropriate value
            string setItems = string.Empty; // TODO: Initialize to an appropriate value
            target.updateQuerry(myCommand, setItems);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for updateStatistics
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBaseServer.exe")]
        public void updateStatisticsTest()
        {
            ExportForm_Accessor target = new ExportForm_Accessor(); // TODO: Initialize to an appropriate value
            target.updateStatistics();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
