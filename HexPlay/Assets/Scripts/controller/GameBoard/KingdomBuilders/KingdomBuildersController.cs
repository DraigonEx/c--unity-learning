using System;
using UnityEngine;
using Assets.Scripts.model.GameBoard;
using Assets.Scripts.model.GameBoard.impl;

namespace Assets.Scripts.controller.GameBoard.KingdomBuilders
{
    public class KingdomBuildersController : MonoBehaviour
    {
        private IGame currentGame;

        void Start()
        {
            setupGame();
        }

        private void setupGame()
        {
            currentGame = new KingdomBuildersGameModel();
            currentGame.setupGame( new IPlayer[] { null } );
        }
    }
}
