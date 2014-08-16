using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Utils
{
    public static class GameMapper
    {

        private static readonly IDictionary<string, string> OpponentOfPlayer1 = new Dictionary<string,string>();

        private static readonly IDictionary<string, string> OpponentOfPlayer2 = new Dictionary<string, string>(); 

        public static void Add(string player1, string player2)
        {
            OpponentOfPlayer1.Add(player1,player2);
            OpponentOfPlayer2.Add(player2, player1);
        }

        /* @arg - key for OpponentOfPlayer1
         */
        public static void Remove(string player1)
        {
            string player2 = null;

            if (!OpponentOfPlayer1.TryGetValue(player1, out player2))
            {
                throw new KeyNotFoundException(string.Format("Key: {0} does not exsist",player1));
            }

            OpponentOfPlayer1.Remove(player1);
            OpponentOfPlayer2.Remove(player2);

        }


    }
}