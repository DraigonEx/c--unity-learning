using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface ITile
{
    int X { get; }
    int Y { get; }
    int Z { get; }
    float Elevation { get; set; }
    float Moisture { get; set; }
    Biomes TerrainType { get; set; }
    Locations Feature { get; set; }
    bool IsPlayable { get; set; }
    bool IsHighlighted { get; set; }
    string OwnedBy { get; set; }

    Vector3 getCoordinates();
    float TileHeight();
    float TileWidth();
    float HexVerticalSpacing();
    float HexHorizontalSpacing();
}

