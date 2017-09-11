using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

class Oracle : ILocation
{
    private static readonly string locationName = "Oracle";
    private static readonly string locationDescription = "The oracle allows a player to place an additional settlement on the current playable terrain.";
    private static readonly int numberOfTiles = 1;
    private static readonly Locations getLocationType = Locations.ORACLE;
    private static readonly GameObject locationPrefab = (GameObject)Resources.Load("Prefabs/OraclePrefab", typeof(GameObject));

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