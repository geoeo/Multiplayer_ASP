using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Utils.Interfaces;

namespace Application.Utils
{
    public class GameMapper : IGameMappable
    {

        private readonly IDictionary<string, string> _opponentOfPlayer1 = new Dictionary<string,string>();

        private readonly IDictionary<string, string> _opponentOfPlayer2 = new Dictionary<string, string>(); 

        public void Add(string player1, string player2)
        {
            _opponentOfPlayer1.Add(player1,player2);
            _opponentOfPlayer2.Add(player2,player1);
        }

        /* @arg - key for OpponentOfPlayer1
         */
        public void Remove(string player1)
        {
            string player2 = null;

            if (!_opponentOfPlayer1.TryGetValue(player1, out player2))
            {
                throw new KeyNotFoundException(string.Format("Key: {0} does not exsist",player1));
            }

            _opponentOfPlayer1.Remove(player1);
            _opponentOfPlayer2.Remove(player2);

        }


    }
}