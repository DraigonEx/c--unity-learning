using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class TileMapView : View<TileApp>
{

    public GameObject GenerateTile(Vector3 pos)
    {
        GameObject tileGO = (GameObject)Instantiate(
            app.model.hexMap.TilePrefab,
            pos,
            Quaternion.identity,
            this.transform);

        tileGO.AddComponent<Assets.Scripts.controller.GameBoard.TileController>();

        return tileGO;
    }

    public void UpdateTileVisuals()
    {

        foreach (string key in app.model.hexMap.tileKeys)
        {
            Tile tile = app.model.hexMap.tiles[key];
            GameObject hexGO = app.model.hexMap.GetTileGO(tile);

            MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
            MeshFilter mf = hexGO.GetComponentInChildren<MeshFilter>();

            //Make height related assignments
            if (tile.Elevation >= app.model.hexMap.HeightMountain)
            {
                SwapTilePrefab(hexGO, tile, app.model.hexMap.MountainPrefab);
                hexGO = app.model.hexMap.GetTileGO(tile);
                mr = hexGO.GetComponentInChildren<MeshRenderer>();
                mr.material = app.model.hexMap.MatMountain;
            }
            else if (tile.Elevation >= app.model.hexMap.HeightHill)
            {
                SwapTilePrefab(hexGO, tile, app.model.hexMap.HillPrefab);
                hexGO = app.model.hexMap.GetTileGO(tile);
                mr = hexGO.GetComponentInChildren<MeshRenderer>();
                mr.material = app.model.hexMap.MatCanyon;
            }
            else if (tile.Elevation >= app.model.hexMap.HeightFlat)
            {
                mf.mesh = app.model.hexMap.MeshFlat;
            }
            else
            {
                mr.material = app.model.hexMap.MatOcean;
                mf.mesh = app.model.hexMap.MeshWater;
            }

            // Make moisture related assignments
            if (tile.Elevation >= 0 && tile.Elevation < app.model.hexMap.HeightMountain)
            {
                if (tile.Moisture >= app.model.hexMap.MoistureJungle)
                {
                    mr.material = app.model.hexMap.MatJungle;
                    //if (!amILocation)
                    if (tile.Feature == Locations.NONE)
                    {
                        // SPAWN Jungles
                        Vector3 p = hexGO.transform.position;
                        if (tile.Elevation >= app.model.hexMap.HeightHill)
                        {
                            p.y += 0.25f;
                        }
                        GameObject.Instantiate(app.model.hexMap.ForestPrefab, p, Quaternion.identity, hexGO.transform);
                    }
                }
                else if (tile.Moisture >= app.model.hexMap.MoistureGrassland)
                {
                    mr.material = app.model.hexMap.MatGrassland;
                }
                else if (tile.Moisture >= app.model.hexMap.MoisturePlain)
                {
                    mr.material = app.model.hexMap.MatPlain;
                }
                else if (tile.Moisture >= app.model.hexMap.MoistureCanyon)
                {
                    mr.material = app.model.hexMap.MatCanyon;
                }
                else
                {
                    mr.material = app.model.hexMap.MatDesert;
                }
            }

            // Make Location assignments
            if (tile.Feature != Locations.NONE)
            {
                SwapTilePrefab(hexGO, tile, (LocationFactory.getLocation(tile.Feature)).LocationPrefab);

                if (tile.Feature == Locations.HARBOR)
                {
                    //TODO: Figure out rotating the harbor to face a water tile without rotating text
                    for (int i = 0; i < 6; i++)
                    {
                        Tile checkTileCoords = HexMathHelper.HexNeighbor(tile, i);
                        Tile checkTile = app.model.hexMap.GetTileAt(checkTileCoords.X, checkTileCoords.Y);

                        // Debug.Log(checkTile.Elevation);

                        if(checkTile != null && checkTile.TerrainType == Biomes.SEA)
                        {
                            // Turn to face Water
                            GameObject harbor = app.model.hexMap.GetTileGO(tile);
                            Vector3 spin = harbor.transform.rotation.eulerAngles;
                            spin.y += (60*i);
                            // Debug.Log("Rotating by " + spin.y + "degrees.");
                            harbor.transform.rotation = Quaternion.Euler(spin);
                            break;
                        }

                    }
                }
            }
        }
    }

    public void SwapTilePrefab(GameObject hexGO, Tile tile, GameObject prefab)
    {
        Vector3 p = hexGO.transform.position;
        GameObject newGO = (GameObject)Instantiate(prefab, p, Quaternion.identity, this.transform);
        app.model.hexMap.gameObjectToTileMap.Remove(hexGO);
        app.model.hexMap.gameObjectToTileMap[newGO] = tile;
        Destroy(hexGO);
        app.model.hexMap.tileToGameObjectMap[tile] = newGO;

        newGO.name = "Hex: " + tile.X + "," + tile.Y;

        newGO.AddComponent<Assets.Scripts.controller.GameBoard.TileController>();
    }

    public void HighlightTile(Tile tile)
    {
        GameObject tileGO = app.model.hexMap.GetTileGO(tile);

        MeshRenderer mr = tileGO.GetComponentInChildren<MeshRenderer>();

        // Highlights a tile with a lighter color
        mr.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");

        AddOutline(tileGO);

    }

    public void AddOutline(GameObject tileGO)
    {
        GameObject outlineGO = tileGO.transform.Find("OutlinePrefab").gameObject;
        outlineGO.SetActive(true);
    }

    public void RemoveHighlight(Tile tile)
    {
        GameObject tileGO = app.model.hexMap.GetTileGO(tile);

        MeshRenderer mr = tileGO.GetComponentInChildren<MeshRenderer>();

        mr.material.shader = Shader.Find("Standard");

        RemoveOutline(tileGO);

    }

    public void RemoveOutline(GameObject tileGO)
    {
        GameObject outlineGO = tileGO.transform.Find("OutlinePrefab").gameObject;
        outlineGO.SetActive(false);
    }

    public void PlaceSettlement(Tile tile, string player)
    {
        // I expect to end up using the the SwapTilePrefab function eventually, but for now we'll actually place a house

        GameObject hexGO = app.model.hexMap.GetTileGO(tile);

        Vector3 p = hexGO.transform.position;
        if (tile.Elevation >= app.model.hexMap.HeightHill)
        {
            p.y += 0.25f;
        }
        GameObject.Instantiate(app.model.hexMap.UnitHouseBasePrefab, p, Quaternion.Euler(0, Random.Range(-70, 70), 0), hexGO.transform);

        //TODO: change house roof to player color

    }
}
