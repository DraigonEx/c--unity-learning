using System;
using UnityEngine;
using UnityEngine.EventSystems;
using thelab.mvc;


namespace Assets.Scripts.controller.GameBoard
{
    public class TileController : Controller<TileApp>, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Tile clickedTile = app.model.hexMap.GetTileFromGameObject(eventData.pointerPress);

            Debug.Log("Tile Controller: " + clickedTile.getCoordinates().ToString());

            // TODO: We'll want to get the current selected play and test click conditions using specific conditions
            //        This assignment is temporary and only useful for testing
            string currentPlay = "Default";

            if (currentPlay == "Default")
            {
                if (clickedTile.IsHighlighted && clickedTile.OwnedBy == null)
                {
                    Debug.Log("Tile Controller: Valid Play");

                    string currentPlayer = "ForTesting";

                    app.view.hexMap.PlaceSettlement(clickedTile, currentPlayer);
                    clickedTile.OwnedBy = currentPlayer;

                    app.view.hexMap.RemoveHighlight(clickedTile);
                    clickedTile.IsHighlighted = false;

                    app.controller.turnCon.playerSettlements.Add(clickedTile);
                    app.controller.turnCon.availableDefaultPlays--;
                    app.controller.turnCon.RefreshBoard();

                }
                else if(clickedTile.OwnedBy != null)
                {
                    Debug.Log("Tile Controller: Owned by " + clickedTile.OwnedBy);
                }
            }
        }
    }
}
