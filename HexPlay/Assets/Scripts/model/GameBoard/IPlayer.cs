using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.model.GameBoard.types;

namespace Assets.Scripts.model.GameBoard
{
    interface IPlayer : IComparable // IComparable to allow sorting on TurnOrder
    {
        String Name { get; set; }
        byte TurnOrder { get; set; }

        int Score { get; set; }
        PlayerColors Color { get; set; }
        HashSet<Locations> LocationSet { get; } //not sure how to treat this...
        List<City> Cities { get; }
    }
}
