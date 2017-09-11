using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

class Harbor : ILocation
{
    private static readonly string locationName = "Harbor";
    private static readonly string locationDescription = "The harbor allows a player to move an existing settlement to a water tile.";
    private static readonly int numberOfTiles = 1;
    private static readonly Locations getLocationType = Locations.HARBOR;
    private static readonly GameObject locationPrefab = (GameObject)Resources.Load("Prefabs/HarborPrefab", typeof(GameObject));

    public Locations GetLocationType { get { return getLocationType; } }
    public string LocationName { get { return locationName; } }
    public string LocationDescription { get { return locationDescription; } }
    public GameObject LocationPrefab { get { return locationPrefab; } }
    public int NumberOfTiles { get { return numberOfTiles; } }

    // Map Generation rules

    public bool IsValidTileLocation(Tile tile, Tile[] neighbors)
    {
        // Harbors must be placed near at least one water tile
        int waterNeighbors = 0;
        foreach (var neighbor in neighbors)
        {
            if (neighbor.Elevation < 0)
            {
                waterNeighbors++;
            }
        }

        if(waterNeighbors >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Ability information

    public void PerformAction()
    {

    }
}