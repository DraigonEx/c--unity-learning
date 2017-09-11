using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.model.GameBoard.impl
{
    class KingdomBuildersGameModel : IGame
    {
        private List<ICard> deck;
        public List<ICard> Deck
        {
            get
            {
                return deck;
            }
        }
        private List<IPlayer> players;
        public List<IPlayer> Players
        {
            get
            {
                return players;
            }
        }
        public int CurrentTurn { get; set; }

        public void nextTurn()
        {
            CurrentTurn = ++CurrentTurn % players.Count;
        }

        public void setupGame(IPlayer[] players)
        {
            this.players = players.ToList<IPlayer>();

            throw new NotImplementedException();
        }

        public void restartGame()
        {
            throw new NotImplementedException();
        }
    }
}
