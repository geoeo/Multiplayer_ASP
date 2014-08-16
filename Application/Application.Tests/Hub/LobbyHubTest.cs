using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using Application.Hub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Tests.Hub
{
    /// <summary>
    /// Summary description for LobbyHubTest
    /// </summary>s
    [TestClass]
    public class LobbyHubTest
    {
        private const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Static;
        private Stack<string> WaitingPlayerStackRef { get; set; } 

        public LobbyHubTest()
        {

            FieldInfo stackFieldInfo = typeof(LobbyHub).GetField("WaitingPlayerStack", Flags);

            Assert.IsNotNull(stackFieldInfo);

            WaitingPlayerStackRef = stackFieldInfo.GetValue(null) as Stack<string>;

            Assert.IsNotNull(WaitingPlayerStackRef);


        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestInitialize]
        public void MyTestInitialize()
        {
            WaitingPlayerStackRef.Clear();
            Assert.AreEqual(0,WaitingPlayerStackRef.Count);
        }

        private MethodInfo GetPrivateMethodInfoFor(string methodName)
        {
            MethodInfo dynMethod = typeof (LobbyHub).GetMethod(methodName, Flags);
            Assert.IsNotNull(dynMethod);
            return dynMethod;
        }

        [TestMethod]
        public void AddingOnePlayerToWaitingList()
        {
            var accessStackWithNew = GetPrivateMethodInfoFor("AccessStackWithNew");
            const string player1 = "player1";
            accessStackWithNew.Invoke(null, new Object[] {player1});

            Assert.AreEqual(1,WaitingPlayerStackRef.Count);
            Assert.AreEqual(player1,WaitingPlayerStackRef.Peek());
        }

        [TestMethod]
        public void AddingTwoPlayersToWaitingList()
        {
            var accessStackWithNew = GetPrivateMethodInfoFor("AccessStackWithNew");
            const string player1 = "player1";
            const string player2 = "player2";
            accessStackWithNew.Invoke(null, new Object[] { player1 });
            accessStackWithNew.Invoke(null, new Object[] { player2 });

            Assert.AreEqual(0, WaitingPlayerStackRef.Count);
        }

    }

}
