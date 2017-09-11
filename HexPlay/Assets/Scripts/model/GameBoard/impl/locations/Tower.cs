using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

class Tower : ILocation
{
    private static readonly string locationName = "Tower";
    private static readonly string locationDescription = "The tower allows a player to place an additional settlement on the edge of the map (on a playable terrain).";
    private static readonly int numberOfTiles = 2;
    private static readonly Locations getLocationType = Locations.TOWER;
    private static readonly GameObject locationPrefab = (GameObject)Resources.Load("Prefabs/TowerPrefab", typeof(GameObject));

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