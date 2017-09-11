using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

class Tavern : ILocation
{
    private static readonly string locationName = "Tavern";
    private static readonly string locationDescription = "Allows a player to place an additional settlement at the end of a row of 3 or more settlements (only on playable terrains).";
    private static readonly int numberOfTiles = 2;
    private static readonly Locations getLocationType = Locations.TAVERN;
    private static readonly GameObject locationPrefab = (GameObject)Resources.Load("Prefabs/TavernPrefab", typeof(GameObject));

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