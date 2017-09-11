using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

class Oasis : ILocation
{
    private static readonly string locationName = "Oasis";
    private static readonly string locationDescription = "The oasis allows a player to place an additional settlement on a desert tile.";
    private static readonly int numberOfTiles = 2;
    private static readonly Locations getLocationType = Locations.OASIS;
    private static readonly GameObject locationPrefab = (GameObject)Resources.Load("Prefabs/OasisPrefab", typeof(GameObject));

    public Locations GetLocationType { get { return getLocationType; } }
    public string LocationName { get { return locationName; } }
    public string LocationDescription { get { return locationDescription; } }
    public GameObject LocationPrefab { get { return locationPrefab; } }
    public int NumberOfTiles { get { return numberOfTiles; } }

    // Map Generation rules

    public bool IsValidTileLocation(Tile tile, Tile[] neighbors)
    {
        // Needs at least one desert neighbor
        int desertNeighbors = 0;
        foreach (var neighbor in neighbors)
        {
            if (neighbor.TerrainType == Biomes.DESERT)
            {
                desertNeighbors++;
            }
        }

        if (desertNeighbors >= 1)
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