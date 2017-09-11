using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using thelab.mvc;
using System;

public class MouseController : Controller<TileApp>, IPointerClickHandler
{

    // Use this for initialization
    void Start()
    {
        Update_CurrentFunc = Update_DetectModeStart;

    }

    // Generic bookkeeping variables
    Vector3 lastMousePosition;  // From Input.mousePosition

    // Camera Dragging bookkeeping variables
    int mouseDragThreshold = 1; // Threshold of mouse movement to start a drag
    Vector3 lastMouseGroundPlanePosition;
    Vector3 cameraTargetOffset;

    delegate void UpdateFunc();
    UpdateFunc Update_CurrentFunc;

    public LayerMask LayerIDForHexTiles;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SelectedUnit = null;
            CancelUpdateFunc();
        }

        ///NEED TO REPLACE CLICK LOGIC HERE...
        Update_CurrentFunc();

        // Always do camera zooms (check for being over a scroll UI later)
        Update_ScrollZoom();

        lastMousePosition = Input.mousePosition;
    }

    void CancelUpdateFunc()
    {
        Update_CurrentFunc = Update_DetectModeStart;
    }

    void Update_DetectModeStart()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // Left mouse button just went down.
            // This doesn't do anything by itself, really.
            // Debug.Log("MOUSE DOWN");
        }
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    Tile selectTile = MouseToTile();
        //    //if (selectTile.IsPlayable && !selectTile.IsHighlighted)
        //    //{
        //    //    app.view.hexMap.HighlightTile(selectTile);
        //    //    selectTile.IsHighlighted = true;
        //    //}
        //    //else if (selectTile.IsHighlighted)
        //    //{
        //    //    app.view.hexMap.RemoveHighlight(selectTile);
        //    //    selectTile.IsHighlighted = false;
        //    //}
        //}
        else if (Input.GetMouseButtonUp(0) && app.model.currentTurn.CurrentPlay == Locations.NONE)
        {
            // This is a default turn

            //Tile selectTile = MouseToTile();
            //if (selectTile.IsPlayable)
            //{
            //    Debug.Log("Playable");
            //}
            //if (selectTile.IsHighlighted)
            //{
            //    Debug.Log("Highlighted");
            //}
            //if (selectTile.IsPlayable && selectTile.IsHighlighted)
            //{
            //    string currentPlayer = "ForTesting";

            //    app.view.hexMap.PlaceSettlement(selectTile, currentPlayer);
            //    selectTile.OwnedBy = currentPlayer;

            //    app.view.hexMap.RemoveHighlight(selectTile);
            //    selectTile.IsHighlighted = false;

            //    // send event notification to TurnController "Settlement Placed"
            //}
        }
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    Tile selectTile = MouseToTile();
        //    //if (selectTile.IsPlayable && !selectTile.IsHighlighted)
        //    //{
        //    //    app.view.hexMap.HighlightTile(selectTile);
        //    //    selectTile.IsHighlighted = true;
        //    //}
        //    //else if (selectTile.IsHighlighted)
        //    //{
        //    //    app.view.hexMap.RemoveHighlight(selectTile);
        //    //    selectTile.IsHighlighted = false;
        //    //}
        //}
        else if (Input.GetMouseButton(0) &&
            Vector3.Distance(Input.mousePosition, lastMousePosition) > mouseDragThreshold)
        {
            // Left button is being held down AND the mouse moved? That's a camera drag!
            Update_CurrentFunc = Update_CameraDrag;
            lastMouseGroundPlanePosition = MouseToGroundPlane(Input.mousePosition);
            Update_CurrentFunc();
        }
    }

    Tile MouseToTile()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        int layerMask = LayerIDForHexTiles.value;

        if (Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, layerMask))
        {
            // Something got hit

            // The collider is a child of the "correct" game object that we want.
            GameObject hexGO = hitInfo.rigidbody.gameObject;

            Tile tile = app.model.hexMap.GetTileFromGameObject(hexGO);

            Debug.Log( string.Format("{0} {1}, {2}",hitInfo.collider.name,tile.X,tile.Y));

            return tile;
        }

        Debug.Log("Found nothing.");
        return null;
    }

    Vector3 MouseToGroundPlane(Vector3 mousePos)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        // What is the point at which the mouse ray intersects Y=0
        if (mouseRay.direction.y >= 0)
        {
            //Debug.LogError("Why is mouse pointing up?");
            return Vector3.zero;
        }
        float rayLength = (mouseRay.origin.y / mouseRay.direction.y);
        return mouseRay.origin - (mouseRay.direction * rayLength);
    }

    void Update_CameraDrag()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // Debug.Log("Cancelling camera drag.");
            CancelUpdateFunc();
            return;
        }

        // Right now, all we need are camera controls

        Vector3 hitPos = MouseToGroundPlane(Input.mousePosition);

        Vector3 diff = lastMouseGroundPlanePosition - hitPos;
        Camera.main.transform.Translate(diff, Space.World);

        lastMouseGroundPlanePosition = hitPos = MouseToGroundPlane(Input.mousePosition);



    }

    void Update_ScrollZoom()
    {
        // Zoom to scrollwheel
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
        float minHeight = 2;
        float maxHeight = 30;
        // Move camera towards hitPos
        Vector3 hitPos = MouseToGroundPlane(Input.mousePosition);
        Vector3 dir = hitPos - Camera.main.transform.position;

        Vector3 p = Camera.main.transform.position;

        // Stop zooming out at a certain distance.
        // TODO: Maybe you should still slide around at 20 zoom?
        if (scrollAmount > 0 || p.y < (maxHeight - 0.1f))
        {
            cameraTargetOffset += dir * scrollAmount;
        }
        Vector3 lastCameraPosition = Camera.main.transform.position;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.position + cameraTargetOffset, Time.deltaTime * 5f);
        cameraTargetOffset -= Camera.main.transform.position - lastCameraPosition;


        p = Camera.main.transform.position;
        if (p.y < minHeight)
        {
            p.y = minHeight;
        }
        if (p.y > maxHeight)
        {
            p.y = maxHeight;
        }
        Camera.main.transform.position = p;

        // Change camera angle
        Camera.main.transform.rotation = Quaternion.Euler(
            Mathf.Lerp(30, 75, Camera.main.transform.position.y / maxHeight),
            Camera.main.transform.rotation.eulerAngles.y,
            Camera.main.transform.rotation.eulerAngles.z
        );


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // This is a default turn
        Debug.Log("clicked");

        Tile selectTile = MouseToTile();
        if (selectTile.IsPlayable)
        {
            Debug.Log("Playable");
        }
        if (selectTile.IsHighlighted)
        {
            Debug.Log("Highlighted");
        }
        if (selectTile.IsPlayable && selectTile.IsHighlighted)
        {
            string currentPlayer = "ForTesting";

            app.view.hexMap.PlaceSettlement(selectTile, currentPlayer);
            selectTile.OwnedBy = currentPlayer;

            app.view.hexMap.RemoveHighlight(selectTile);
            selectTile.IsHighlighted = false;
        }
    }
}