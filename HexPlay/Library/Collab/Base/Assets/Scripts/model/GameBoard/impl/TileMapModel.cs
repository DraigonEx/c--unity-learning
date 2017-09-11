using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class TileMapModel : Model<TileApp> {

    // Use this for initialization
    void Start()
    {
        MapRadius = MapWidth / 2;
        NumRows = MapWidth;
        NumColumns = MapWidth;
    }

    public GameObject TilePrefab;

    public Mesh MeshWater;
    public Mesh MeshFlat;
    public Mesh MeshHill;
    public Mesh MeshMountain;
    public Mesh MeshLocation;

    public GameObject ForestPrefab;
    public GameObject JunglePrefab;
    public GameObject FancyLocationPrefab;

    public Material MatOcean;
    public Material MatMountain;
    public Material MatGrassland;
    public Material MatCanyon;
    public Material MatDesert;
    public Material MatPlain;
    public Material MatJungle;
    public Material MatLocation;

    public GameObject UnitHouseBasePrefab;

    [System.NonSerialized] public float StartingElevation = 0f;
    [System.NonSerialized] public float HeightMountain = 0.9f;
    [System.NonSerialized] public float HeightHill = 0.6f;
    [System.NonSerialized] public float HeightFlat = 0.0f;

    [System.NonSerialized] public float MoistureJungle = 0.275f;
    [System.NonSerialized] public float MoistureGrassland = 0.1f;
    [System.NonSerialized] public float MoisturePlain = -0.1f;
    [System.NonSerialized] public float MoistureCanyon = -0.275f;

    //[SerializeField]
    //private int mapWidth;
    // 13 -> 127 Tilees, 3 Locations
    // 17 -> 217 Tilees, 6 Locations
    // 21 -> 331 Tilees, 9 Locations
    // 25 -> 469 Tilees, 12 Locations (a bit bigger than a normal game size in the board game)
    [System.NonSerialized] public int MapWidth = 25;
    [System.NonSerialized] public int NumRows;
    [System.NonSerialized] public int NumColumns;
    [System.NonSerialized] public int MapRadius;

    // Set up Dictionaries and Lists for tilees
    public Dictionary<string, Tile> tiles;
    public List<string> tileKeys;
    public Dictionary<Tile, GameObject> tileToGameObjectMap;
    public Dictionary<GameObject, Tile> gameObjectToTileMap;

    public int GetTileCount()
    {
        return tiles.Count;
    }

    public Tile GetTileAt(int x, int y)
    {
        if (tiles == null)
        {
            Debug.LogError("Tiles dictionary not yet instantiated!");
            return null;
        }

        if (tiles.ContainsKey(x + ", " + y))
            return tiles[x + ", " + y];
        else
            return null;
    }

    public Tile GetRandomTile()
    {
        int index = Random.Range(0, (tileKeys.Count));
        string myKey = tileKeys[index];
        return tiles[myKey];
    }

    public Tile GetTileFromGameObject(GameObject tileGO)
    {
        if (gameObjectToTileMap.ContainsKey(tileGO))
        {
            return gameObjectToTileMap[tileGO];
        }

        return null;
    }

    public GameObject GetTileGO(Tile tile)
    {
        if (tileToGameObjectMap.ContainsKey(tile))
        {
            return tileToGameObjectMap[tile];
        }

        return null;
    }

    public Vector3 GetTilePosition(int q, int r)
    {
        Tile tile = GetTileAt(q, r);

        return GetTilePosition(tile);
    }

    public Vector3 GetTilePosition(Tile tile)
    {
        return tile.Position();
    }

    public bool InMap(Tile tile)
    {
        int x = tile.X;
        int y = tile.Y;

        if (tiles.ContainsKey(x + ", " + y))
            return true;

        return false;
    }

    public bool InMap(int x, int y)
    {
        if (tiles.ContainsKey(x + ", " + y))
            return true;

        return false;
    }

}
