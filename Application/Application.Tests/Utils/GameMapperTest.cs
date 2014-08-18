using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using Application.Utils;
using NUnit.Framework;

namespace Application.Tests.Utils
{
    /// <summary>
    /// Summary description for GameMapperTest
    /// </summary>
    [TestFixture]
    public class GameMapperTest
    {

        private IDictionary<string, string> DictionaryForPlayer1 { get; set; }
        private IDictionary<string, string> DictionaryForPlayer2 { get; set; }
        private GameMapper GameMapper { get; set; }

        public GameMapperTest()
        {
            GameMapper = new GameMapper();
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;


            FieldInfo fieldForPlayer1 = typeof(GameMapper).GetField("_opponentOfPlayer1", flags);
            FieldInfo fieldForPlayer2 = typeof(GameMapper).GetField("_opponentOfPlayer2", flags);

            Assert.IsNotNull(fieldForPlayer1);
            Assert.IsNotNull(fieldForPlayer2);

            DictionaryForPlayer1 = fieldForPlayer1.GetValue(GameMapper) as Dictionary<string, string>;
            DictionaryForPlayer2 = fieldForPlayer2.GetValue(GameMapper) as Dictionary<string, string>;

            Assert.IsNotNull(DictionaryForPlayer1);
            Assert.IsNotNull(DictionaryForPlayer2);
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

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>

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
        
        [SetUp]
        public void MyTestInitialize()
        {
            DictionaryForPlayer1.Clear();
            DictionaryForPlayer2.Clear();

            Assert.AreEqual(0, DictionaryForPlayer1.Count);
            Assert.AreEqual(0, DictionaryForPlayer2.Count);
        }
        
        [Test]
        public void AddingTwoPlayers()
        {
            
            const string player1 = "player1";
            const string player2 = "player2";

            string value;

            GameMapper.Add(player1, player2);

            Assert.AreEqual(1, DictionaryForPlayer1.Count);
            Assert.AreEqual(1, DictionaryForPlayer2.Count);

            Assert.IsTrue(DictionaryForPlayer1.ContainsKey(player1));
            Assert.IsTrue(DictionaryForPlayer2.ContainsKey(player2));

            Assert.IsTrue(DictionaryForPlayer1.TryGetValue(player1, out value));
            Assert.AreEqual(player2, value);            
            
            Assert.IsTrue(DictionaryForPlayer2.TryGetValue(player2, out value));
            Assert.AreEqual(player1, value);
        }

        [Test]
        public void RemoveAPlayer()
        {
            const string player1 = "player1";

            AddingTwoPlayers();

            Assert.AreEqual(1, DictionaryForPlayer1.Count);
            Assert.AreEqual(1, DictionaryForPlayer2.Count);

            GameMapper.Remove(player1);

            Assert.AreEqual(0, DictionaryForPlayer1.Count);
            Assert.AreEqual(0, DictionaryForPlayer2.Count);


        }

        [Test,ExpectedException(typeof(KeyNotFoundException))]
        public void RemoveAPlayerFromEmpty()
        {
            GameMapper.Remove("invalid");
        }

    }
}
