using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class HexModel : Model<TileApp> {

    // Searches the Hierarchy for Models
    public TileModel hex { get { return m_hex = Assert<TileModel>(m_hex); } }
    private TileModel m_hex;

    public TileMapModel hexMap { get { return m_hexMap = Assert<TileMapModel>(m_hexMap); } }
    private TileMapModel m_hexMap;

}
