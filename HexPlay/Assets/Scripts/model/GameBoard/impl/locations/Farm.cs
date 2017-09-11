using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

class Farm : ILocation
{
    private static readonly string locationName = "Farm";
    private static readonly string locationDescription = "The farm allows a player to place an additional settlement on a plains tile.";
    private static readonly int numberOfTiles = 2;
    private static readonly Locations getLocationType = Locations.FARM;
    private static readonly GameObject locationPrefab = (GameObject)Resources.Load("Prefabs/FarmPrefab", typeof(GameObject));

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