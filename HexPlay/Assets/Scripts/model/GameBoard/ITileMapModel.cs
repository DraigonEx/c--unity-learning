using UnityEngine;

public interface ITileMapModel
{
    Tile GetRandomTile();
    Tile GetTileAt(int x, int y);
    int GetTileCount();
    Tile GetTileFromGameObject(GameObject tileGO);
    GameObject GetTileGO(Tile tile);
    Vector3 GetTilePosition(int q, int r);
    Vector3 GetTilePosition(Tile tile);
    bool InMap(int x, int y);
    bool InMap(Tile tile);
}