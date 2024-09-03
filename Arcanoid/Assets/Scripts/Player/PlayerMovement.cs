using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerGO;
        private InputController _inputController;
        private float _inputValue;
        private void Start()
        {
            SystemManager.Get(out _inputController);
            _inputController.PlayerMovePerformed += (value) => _inputValue = value;
            _inputController.PlayerMoveCanceled += () => _inputValue = 0;
        }

        private void Update()
        {
            if (GameStatesManager.gameState != StaticData.AvailableGameStates.Playing)
                return;

            //Debug.Log($"ProceedInput {_inputValue}");

            if (Mathf.Abs(_inputValue) > 0.1f)
            {
                gameObject.transform.Translate(new Vector3(_inputValue, 0, 0) * PlayerStats.PlayerSpeed * Time.deltaTime);
            }
        }
    }
}

