using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface ILocation
{
    Locations GetLocationType { get; }
    string LocationName { get; }
    string LocationDescription { get; }
    GameObject LocationPrefab { get; }

    // Map Generation rules

    int NumberOfTiles { get; }
    bool IsValidTileLocation(Tile tile, Tile[] neighbors);

    // Ability information

    void PerformAction();
}
