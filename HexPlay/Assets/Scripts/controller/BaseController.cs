using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.controller.GameBoard;
using thelab.mvc;

public class BaseController : Controller<TileApp> {

    public CameraKeyboardController camKeyCon { get { return m_camKeyCon = Assert<CameraKeyboardController>(m_camKeyCon); } }
    private CameraKeyboardController m_camKeyCon;

    public TileMapController hexMapCon { get { return m_hexMapCon = Assert<TileMapController>(m_hexMapCon); } }
    private TileMapController m_hexMapCon;

    public HexMathHelper hexMath { get { return m_hexMath = Assert<HexMathHelper>(m_hexMath); } }
    private HexMathHelper m_hexMath;

    public MouseController MouseCon { get { return m_MouseCon = Assert<MouseController>(m_MouseCon); } }
    private MouseController m_MouseCon;

    public TileController tileCon { get { return m_tileCon = Assert<TileController>(m_tileCon); } }
    private TileController m_tileCon;

    public TurnController turnCon { get { return m_turnCon = Assert<TurnController>(m_turnCon); } }
    private TurnController m_turnCon;

}
