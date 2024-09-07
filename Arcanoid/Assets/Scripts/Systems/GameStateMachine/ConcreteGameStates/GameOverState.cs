using SekiburaGames.Arkanoid.System;
using SekiburaGames.Arkanoid.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace SekiburaGames.Arkanoid.System
{
    public class GameOverState : GameState
    {
        private InputController _inputController;
        public GameOverState(GameStateMachine stateMachine) : base(stateMachine)
        {
            SystemManager.Get(out _inputController);
            _inputController.PlayGamePerformed += PlayGameButtonPressed;
        }

        public override void Enter()
        {
            base.Enter();
            ViewManager.Show<GameOverView>();
        }

        private void PlayGameButtonPressed()
        {
            stateMachine.ChangeState(new ResetState(stateMachine));
        }

        public override void Exit()
        {
            base.Exit();
            _inputController.PlayGamePerformed -= PlayGameButtonPressed;
        }
    }
}
