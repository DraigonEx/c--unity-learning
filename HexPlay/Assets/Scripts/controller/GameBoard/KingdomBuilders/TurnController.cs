using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class TurnController : Controller<TileApp>
{
    void Start()
    {
        playerSettlements = new List<Tile>();
        currentPlay = "Default";
    }

    public string currentPlay;
    public int availableDefaultPlays;
    Biomes currentBiome;

    // Test variables
    public List<Tile> playerSettlements;

    public void RefreshBoard()
    {
        // This is meant to Highlight appropriate tiles per the currentPlay

        app.controller.hexMapCon.ClearHighlightedTiles();

        Debug.Log("Current Play: " + currentPlay);
        Debug.Log("Available plays: " + availableDefaultPlays);

        if (currentPlay == "Default")
        {
            // Check for adjacent valid plays
            // else highlight the current biome
            // if no valid plays draw a new biome card and try again
            if(availableDefaultPlays != 0)
            {
                // Setup for default play
                app.controller.hexMapCon.HighLightPlayableTiles(currentBiome);
            }
            else
            {
                // Enable End Turn button in UI and disable Default play button
            }

        }
    }

    public void NewTurn()
    {
        // Set appropriate values for a player's new turn

        //playerHouses should be pulled from a player model for the correct player
        int playerHouses = 40;

        if(playerHouses < 3)
        {
            availableDefaultPlays = playerHouses;
        }
        else
        {
            availableDefaultPlays = 3;
        }

        Biomes[] playableBiomes = { Biomes.DESERT, Biomes.FIELD, Biomes.FLOWERS, Biomes.FOREST, Biomes.HILL };
        int index = Random.Range(0, playableBiomes.Length);
        currentBiome = playableBiomes[index];

        RefreshBoard();
    }
}

