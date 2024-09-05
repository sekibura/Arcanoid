using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SekiburaGames.Arkanoid.System.StaticData;

namespace SekiburaGames.Arkanoid.UI
{
    public class UI_EnableOnGameState : MonoBehaviour
    {
        [SerializeField]
        private AvailableGameStates _gameState;

        [SerializeField]
        private bool _invert;

        private void Start()
        {
            GameStatesManager.Instance.GameStateChanged.AddListener(GameStateUpdated);
        }

        private void GameStateUpdated()
        {
            if(_invert)
                    gameObject.SetActive(GameStatesManager.gameState != _gameState);
            else
                gameObject.SetActive(GameStatesManager.gameState == _gameState);

            //switch (GameStatesManager.gameState)
            //{
            //    case AvailableGameStates.Menu:
            //        break;
            //    case AvailableGameStates.Starting:
            //        break;
            //    case AvailableGameStates.Playing:
            //        break;
            //    case AvailableGameStates.Tutorial:
            //        break;
            //    case AvailableGameStates.Pausing:
            //        break;
            //    case AvailableGameStates.Ending:
            //        break;
            //    default:
            //        break;
            //}
        }

    }
}
