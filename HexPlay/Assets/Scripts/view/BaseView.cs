using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class BaseView : View<TileApp> {

    // Searches the Hierarchy for HexView
    public TileView hex { get { return m_hex = Assert<TileView>(m_hex); } }
    private TileView m_hex;

    public TileMapView hexMap { get { return m_hexMap = Assert<TileMapView>(m_hexMap); } }
    private TileMapView m_hexMap;
}
