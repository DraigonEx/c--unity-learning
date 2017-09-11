using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using thelab.mvc;
using Assets.Scripts.model.GameBoard;
using Assets.Scripts.model.GameBoard.types;

public class Player : IPlayer
{
    public String Name { get; set; }
    public byte TurnOrder { get; set; }

    public int Score { get; set; }
    public PlayerColors Color { get; set; }
    public HashSet<Locations> LocationSet { get; set; } //not sure how to treat this...
    public List<City> Cities { get; set; }

    public int CompareTo(object player)
    {
        return player == null ? 0 : this.TurnOrder - ((Player)player).TurnOrder;
    }
}
