using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using thelab.mvc;

public class DefaultTileMap : TileMapController
{
    public override void GenerateMap(MapShape mapShape)
    {
        int mapWidth = app.model.hexMap.MapWidth;
        int mapRadius = app.model.hexMap.MapRadius;

        // Generate base map
        base.GenerateMap(mapShape);

        // Make raised area
        int numMountains = Random.Range(mapWidth / 3, mapRadius);
        int numRivers = Random.Range(mapWidth / 3, mapWidth * 3 / 4);

        for (int i = 0; i < numMountains; i++)
        {
            int rangeSize = Random.Range(3, mapWidth / 4);

            Tile lineStart = app.model.hexMap.GetRandomTile();
            if (lineStart != null)
            {
                //Debug.Log("LineStart: " + lineStart.X + ", " + lineStart.Y);

                Tile randomTarget = null;

                while (randomTarget == null)
                {
                    randomTarget = GetRandomHexAtDistance(lineStart, rangeSize);
                }

                Tile[] line = GetLineOfHexes(lineStart, randomTarget);

                foreach (Tile tile in line)
                {
                    ElevateArea(tile.X, tile.Y, 0, 10f);

                    //Debug.Log("Setting elevation for hex at " + tile.X + ", " + tile.Y + ".");
                }
            }
        }

        for (int i = 0; i < numRivers; i++)
        {
            int rangeSize = Random.Range(1, mapWidth / 4);

            Tile lineStart = app.model.hexMap.GetRandomTile();
            if (lineStart != null)
            {
                //Debug.Log("LineStart: " + lineStart.X + ", " + lineStart.Y);

                Tile randomTarget = null;

                while (randomTarget == null)
                {
                    randomTarget = GetRandomHexAtDistance(lineStart, rangeSize);
                }

                Tile[] line = GetLineOfHexes(lineStart, randomTarget);

                foreach (Tile tile in line)
                {
                    SinkArea(tile.X, tile.Y, 0);

                    //Debug.Log("Setting elevation for hex at " + tile.X + ", " + tile.Y + ".");
                }
            }
        }

        if (mapShape == MapShape.SQUARE)
        {
            Tile mapCenter = app.model.hexMap.GetTileAt(mapRadius/2, mapRadius);
        }
        else
        {
            Tile mapCenter = app.model.hexMap.GetTileAt(0, 0);
        }

        // Add lumpiness with some kind of noise generator

        float noiseResolution = 0.2f;
        Vector2 noiseOffset = new Vector2(Random.Range(0f, -1f), Random.Range(0f, -1f));

        float noiseScale = 2f;

        // Desert / Jungle noise

        foreach (KeyValuePair<string, Tile> pair in app.model.hexMap.tiles)
        {
            if (pair.Value.TerrainType != Biomes.MOUNTAIN && pair.Value.TerrainType != Biomes.SEA)
            {
                float n = Mathf.PerlinNoise(
                    ((float)pair.Value.X / mapWidth / noiseResolution) + noiseOffset.x,
                    ((float)pair.Value.Y / mapWidth / noiseResolution) + noiseOffset.y
                    ) - 0.5f;
                float desertJungle = n * noiseScale;

                if (desertJungle > 0.1f)
                {
                    pair.Value.Moisture = 0.3f;
                    pair.Value.TerrainType = Biomes.FOREST;
                }
                else if (desertJungle < -0.2f)
                {
                    pair.Value.Moisture = -0.3f;
                    pair.Value.TerrainType = Biomes.DESERT;
                }
            }
        }

        // Canyon / Grassland noise

        noiseResolution = 0.2f;
        noiseOffset = new Vector2(Random.Range(1f, 2f), Random.Range(1f, 2f));

        noiseScale = 1f;

        foreach (KeyValuePair<string, Tile> pair in app.model.hexMap.tiles)
        {
            if (pair.Value.TerrainType != Biomes.MOUNTAIN && pair.Value.TerrainType != Biomes.SEA)
            {
                float n = Mathf.PerlinNoise(
                ((float)pair.Value.X / mapWidth / noiseResolution) + noiseOffset.x,
                ((float)pair.Value.Y / mapWidth / noiseResolution) + noiseOffset.y
                ) - 0.5f;
                float canyonGrassland = n * noiseScale;

                if (canyonGrassland > 0.1f)
                {
                    pair.Value.Moisture = 0.2f;
                    pair.Value.TerrainType = Biomes.FLOWERS;
                }
                else if (canyonGrassland < -0.2f)
                {
                    pair.Value.Moisture = -0.2f;
                    pair.Value.Elevation = 0.7f;
                    pair.Value.TerrainType = Biomes.HILL;
                }
            }
        }
        
        // Generating Location tiles

        int hexCount = app.model.hexMap.GetTileCount();
        int locationCount = hexCount / 100 * 3;
        int numTypes = hexCount / 100;
        // Debug.Log(locationCount);
        Debug.Log(hexCount);
        // Get random "specials"
        List<ILocation> locationList = new List<ILocation>();
        List<Locations> specials = new List<Locations>();
        ILocation randomLocation;
        while (specials.Count != numTypes)
        {
            randomLocation = LocationFactory.getRandomPlayableLocation();
            if (!(specials.Contains(randomLocation.GetLocationType)))
            {
                specials.Add(randomLocation.GetLocationType);
                locationList.Add(randomLocation);
                if (randomLocation.NumberOfTiles == 2) locationList.Add(LocationFactory.getLocation(randomLocation.GetLocationType));
            }
        }

        while (locationList.Count < locationCount)
        {
            locationList.Add(LocationFactory.getLocation(Locations.CITY));
        }

        // Shuffling the list
        for (int i = 0; i < locationList.Count; i++)
        {
            ILocation temp = locationList[i];
            int randomIndex = Random.Range(i, locationList.Count);
            locationList[i] = locationList[randomIndex];
            locationList[randomIndex] = temp;
        }

        Tile[] locationSet = GenerateLocationList(4, locationCount, 30, locationList);

        // Update Hex visuals

        app.view.hexMap.UpdateTileVisuals();
        app.controller.turnCon.NewTurn();
    }
}