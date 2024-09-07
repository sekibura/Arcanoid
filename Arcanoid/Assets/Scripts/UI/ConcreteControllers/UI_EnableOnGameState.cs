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
        private GameStates _gameState;

        [SerializeField]
        private bool _invert;

        private GameStateMachine _gameStateMachine;

        private void Start()
        {
            SystemManager.Get(out _gameStateMachine);
            _gameStateMachine.GameStateChangedEvent += GameStateUpdated;
        }

        private void GameStateUpdated(GameState gameState)
        {
            switch (_gameState)
            {
                case GameStates.ResetState:
                    {
                        if (_invert)
                            gameObject.SetActive(!_gameStateMachine.IsCurrentState<ResetState>());
                        else
                            gameObject.SetActive(_gameStateMachine.IsCurrentState<ResetState>());
                    }
                    break;
                case GameStates.GameplayState:
                    {
                        if (_invert)
                            gameObject.SetActive(!_gameStateMachine.IsCurrentState<GameplayState>());
                        else
                            gameObject.SetActive(_gameStateMachine.IsCurrentState<GameplayState>());
                    }
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }

        private enum GameStates
        {
            ResetState,
            GameplayState
        }
    }
}
