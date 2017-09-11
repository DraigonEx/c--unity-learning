using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

class Barn : ILocation
{
    private static readonly string locationName = "Barn";
    private static readonly string locationDescription = "The barn allows a player to move an existing settlement to a tile matching the current playable terrain.";
    private static readonly int numberOfTiles = 2;
    private static readonly Locations getLocationType = Locations.BARN;
    private static readonly GameObject locationPrefab = (GameObject) Resources.Load("Prefabs/BarnPrefab", typeof(GameObject));

    public Locations GetLocationType { get { return getLocationType; } }
    public string LocationName { get { return locationName; } }
    public string LocationDescription { get { return locationDescription; } }
    public GameObject LocationPrefab { get { return locationPrefab; } }
    public int NumberOfTiles { get { return numberOfTiles; } }

    // Map Generation rules

    public bool IsValidTileLocation(Tile tile, Tile[] neighbors)
    {
        return true;
    }

    // Ability information

    public void PerformAction()
    {

    }
}