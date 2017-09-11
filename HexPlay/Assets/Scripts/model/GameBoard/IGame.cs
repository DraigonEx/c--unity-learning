using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.model.GameBoard
{
    interface IGame
    {
        List<ICard> Deck { get; }
        List<IPlayer> Players { get; }
        int CurrentTurn { get; set; } //player list index after sort

        void nextTurn();
        void setupGame(IPlayer[] players);
        void restartGame();
    }
}
