using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static SekiburaGames.Arkanoid.System.StaticData;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerGO;

        [SerializeField]
        private Renderer _playerRender;
        [SerializeField]
        private Renderer _leftBound;
        [SerializeField]
        private Renderer _rightBound;

        private InputController _inputController;
        private float _inputValue;

        [SerializeField]
        private Vector3 _defaultPlayerPosition;

        private GameStateMachine _gameStateMachine;

        private void Start()
        {
            SystemManager.Get(out _inputController);
            
            _inputController.PlayerMovePerformed += (value) => _inputValue = value;
            _inputController.PlayerMoveCanceled += () => _inputValue = 0;
            //GameStatesManager.Instance.GameStateChanged.AddListener(GameStateUpdated);
            SystemManager.Get(out _gameStateMachine);
        }

        private void Update()
        {
            if (!(_gameStateMachine.IsCurrentState<GameplayState>()) && !(_gameStateMachine.IsCurrentState<ResetState>()))
                return;
            if (((_playerGO.transform.position.x + _playerRender.bounds.size.x/2) >= (_rightBound.transform.position.x - _rightBound.bounds.size.x/2)) && _inputValue > 0)
                return;
            else if (((_playerGO.transform.position.x - _playerRender.bounds.size.x / 2) <= (_leftBound.transform.position.x + _leftBound.bounds.size.x / 2)) && _inputValue < 0)
                return;

            if (Mathf.Abs(_inputValue) > 0.1f)
            {
                gameObject.transform.Translate(new Vector3(_inputValue, 0, 0) * PlayerStats.PlayerSpeed * Time.deltaTime);
            }
        }

        public void ResetPlayerPosition()
        {
            Debug.Log($"[PlayerMovement]: ResetPlayerPosition!");
            _playerGO.transform.position = _defaultPlayerPosition;
        }
    }
}

