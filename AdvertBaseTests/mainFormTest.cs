using AdvertBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;

namespace AdvertBaseTests
{
    
    
    /// <summary>
    ///This is a test class for mainFormTest and is intended
    ///to contain all mainFormTest Unit Tests
    ///</summary>
    [TestClass()]
    public class mainFormTest
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
        ///A test for mainForm Constructor
        ///</summary>
        [TestMethod()]
        public void mainFormConstructorTest()
        {
            mainForm target = new mainForm();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AddNode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void AddNodeTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            TreeNode node = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AddNode(node);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddSearchResult
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void AddSearchResultTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            string CommandText = string.Empty; // TODO: Initialize to an appropriate value
            string Connect = string.Empty; // TODO: Initialize to an appropriate value
            target.AddSearchResult(CommandText, Connect);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddSlashes
        ///</summary>
        [TestMethod()]
        public void AddSlashesTest()
        {
            mainForm target = new mainForm(); // TODO: Initialize to an appropriate value
            string InputTxt = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.AddSlashes(InputTxt);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void DisposeTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
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
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for addAtributeToXml
        ///</summary>
        [TestMethod()]
        public void addAtributeToXmlTest()
        {
            mainForm target = new mainForm(); // TODO: Initialize to an appropriate value
            XmlTextWriter t = null; // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            string text = string.Empty; // TODO: Initialize to an appropriate value
            target.addAtributeToXml(t, name, text);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for addCard_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void addCard_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.addCard_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button1_Click_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void button1_Click_1Test()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button1_Click_1(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button2_Click_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void button2_Click_1Test()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button2_Click_1(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button3_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void button3_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button3_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button4_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void button4_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button4_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button5_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void button5_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button5_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for button6_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void button6_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.button6_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for cardComment_TextChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardComment_TextChangedTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardComment_TextChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for cardList_CellClick
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardList_CellClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            DataGridViewCellEventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardList_CellClick(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for cardList_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardList_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardList_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for cardList_DoubleClick
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardList_DoubleClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardList_DoubleClick(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for cardName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardNameTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardName(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for cardsView_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardsView_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardsView_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for cardsView_ItemActivate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void cardsView_ItemActivateTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.cardsView_ItemActivate(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for catalogList_CellClick
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void catalogList_CellClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            DataGridViewCellEventArgs e = null; // TODO: Initialize to an appropriate value
            target.catalogList_CellClick(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for catalogList_CellContentClick
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void catalogList_CellContentClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            DataGridViewCellEventArgs e = null; // TODO: Initialize to an appropriate value
            target.catalogList_CellContentClick(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for fillCard
        ///</summary>
        [TestMethod()]
        public void fillCardTest()
        {
            mainForm target = new mainForm(); // TODO: Initialize to an appropriate value
            string id = string.Empty; // TODO: Initialize to an appropriate value
            target.fillCard(id);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for groupBox3_Enter
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void groupBox3_EnterTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.groupBox3_Enter(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for listOfCards_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void listOfCards_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.listOfCards_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for mainCatalogTree_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void mainCatalogTree_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.mainCatalogTree_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for mainForm_FormClosed
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void mainForm_FormClosedTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            FormClosedEventArgs e = null; // TODO: Initialize to an appropriate value
            target.mainForm_FormClosed(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for mainForm_Load
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void mainForm_LoadTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.mainForm_Load(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for mainForm_Shown
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void mainForm_ShownTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.mainForm_Shown(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for panel1_Paint
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void panel1_PaintTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            PaintEventArgs e = null; // TODO: Initialize to an appropriate value
            target.panel1_Paint(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for tabPage2_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void tabPage2_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.tabPage2_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton10_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton10_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton10_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton11_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton11_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton11_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton12_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton12_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton12_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton12_Click_1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton12_Click_1Test()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton12_Click_1(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton2_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton2_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton2_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton3_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton3_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton3_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton4_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton4_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton4_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton6_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton6_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton6_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton7_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton7_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton7_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton8_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton8_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton8_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for toolStripButton9_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void toolStripButton9_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.toolStripButton9_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for updateQuerry
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void updateQuerryTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            MySqlCommand myCommand = null; // TODO: Initialize to an appropriate value
            string setItems = string.Empty; // TODO: Initialize to an appropriate value
            target.updateQuerry(myCommand, setItems);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for usersButton_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("AdvertBase.exe")]
        public void usersButton_ClickTest()
        {
            mainForm_Accessor target = new mainForm_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.usersButton_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
