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


    }
}