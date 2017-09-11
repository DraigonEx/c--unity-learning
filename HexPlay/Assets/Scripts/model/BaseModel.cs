using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class BaseModel : Model<TileApp> {

    // Searches the Hierarchy for Models
    public TileMapModel hexMap { get { return m_hexMap = Assert<TileMapModel>(m_hexMap); } }
    private TileMapModel m_hexMap;

    public Turn currentTurn { get { return m_turn = Assert<Turn>(m_turn); } }
    private Turn m_turn;
}
