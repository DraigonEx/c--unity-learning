using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class City : ILocation
{
    private static string locationName = "City";
    private static string locationDescription = "All settlements built adjacent to this tile will receive 1 gold.";
    private static int numberOfTiles = 1;
    private static readonly Locations getLocationType = Locations.CITY;
    private static readonly GameObject locationPrefab = (GameObject)Resources.Load("Prefabs/CityPrefab", typeof(GameObject));

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