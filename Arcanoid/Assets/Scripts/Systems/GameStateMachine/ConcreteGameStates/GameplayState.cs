using SekiburaGames.Arkanoid.Gameplay;
using SekiburaGames.Arkanoid.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SekiburaGames.Arkanoid.System
{
    public class GameplayState : GameState
    {
        private BallController _ballController;
        private GameStateMachine _gameStateMachine;
        public GameplayState(GameStateMachine stateMachine) : base(stateMachine)
        {
            SystemManager.Get(out _gameStateMachine);
            _ballController = MonobehReferencesManager.Instance.FindByType<BallController>();
        }

        public override void Enter()
        {
            //Debug.Log("Entered Gameplay State");
            ViewManager.Show<GameplayView>();
            if (_gameStateMachine.IsLastState<PauseState>())
                _ballController.ContinueMovement();
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            //Debug.Log("Exiting Gameplay State");
            // Код для остановки игрового процесса
        }
    }
}
