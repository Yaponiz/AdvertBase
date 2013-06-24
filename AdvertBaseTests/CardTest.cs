using AdvertBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdvertBaseTests
{
    
    
    /// <summary>
    ///This is a test class for CardTest and is intended
    ///to contain all CardTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CardTest
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
        ///A test for ClientPhone
        ///</summary>
        [TestMethod()]
        public void ClientPhoneTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.ClientPhone = expected;
            actual = target.ClientPhone;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ClientAddress
        ///</summary>
        [TestMethod()]
        public void ClientAddressTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.ClientAddress = expected;
            actual = target.ClientAddress;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CardUser
        ///</summary>
        [TestMethod()]
        public void CardUserTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CardUser = expected;
            actual = target.CardUser;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CardTime
        ///</summary>
        [TestMethod()]
        public void CardTimeTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CardTime = expected;
            actual = target.CardTime;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CardText
        ///</summary>
        [TestMethod()]
        public void CardTextTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CardText = expected;
            actual = target.CardText;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CardPhone
        ///</summary>
        [TestMethod()]
        public void CardPhoneTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CardPhone = expected;
            actual = target.CardPhone;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CardName
        ///</summary>
        [TestMethod()]
        public void CardNameTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CardName = expected;
            actual = target.CardName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CardGroup
        ///</summary>
        [TestMethod()]
        public void CardGroupTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CardGroup = expected;
            actual = target.CardGroup;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CardCost
        ///</summary>
        [TestMethod()]
        public void CardCostTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CardCost = expected;
            actual = target.CardCost;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CardComment
        ///</summary>
        [TestMethod()]
        public void CardCommentTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CardComment = expected;
            actual = target.CardComment;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CardAddress
        ///</summary>
        [TestMethod()]
        public void CardAddressTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CardAddress = expected;
            actual = target.CardAddress;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for setViewsCount
        ///</summary>
        [TestMethod()]
        public void setViewsCountTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.setViewsCount();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getCard
        ///</summary>
        [TestMethod()]
        public void getCardTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.getCard();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for exportCard
        ///</summary>
        [TestMethod()]
        public void exportCardTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.exportCard();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for editCard
        ///</summary>
        [TestMethod()]
        public void editCardTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.editCard();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for checkCard
        ///</summary>
        [TestMethod()]
        public void checkCardTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.checkCard();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for addToBase
        ///</summary>
        [TestMethod()]
        public void addToBaseTest()
        {
            Card target = new Card(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.addToBase();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Card Constructor
        ///</summary>
        [TestMethod()]
        public void CardConstructorTest()
        {
            Card target = new Card();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
