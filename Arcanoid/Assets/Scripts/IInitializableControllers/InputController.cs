using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SekiburaGames.Arkanoid.System
{
    public class InputController : IInitializable
    {
        private InputActions _playerInputActions;

        public Action<float> PlayerMovePerformed;
        public Action PlayerMoveCanceled;

        public Action PlayGamePerformed;
        public Action PlayGameCanceled;


        public void Initialize()
        {
            _playerInputActions = new InputActions();
            _playerInputActions.Enable();
            _playerInputActions.Player.Move.performed += (e => PlayerMovePerformed?.Invoke(e.ReadValue<float>()));
            _playerInputActions.Player.Move.canceled += (CheckMove) => PlayerMoveCanceled?.Invoke();

            _playerInputActions.Player.StartGame.performed += (e) => PlayGamePerformed?.Invoke();
            _playerInputActions.Player.StartGame.canceled += (e) => PlayGameCanceled?.Invoke();
            
        }

        public void Dispose()
        {
            _playerInputActions.Disable();
        }
    }
}
