using SekiburaGames.Arkanoid.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SekiburaGames.Arkanoid.System
{
    public class GameplayState : GameState
    {
        private BallController _ballController;
        public GameplayState(GameStateMachine stateMachine) : base(stateMachine)
        {
            _ballController = MonobehReferencesManager.Instance.FindByType<BallController>();
        }

        public override void Enter()
        {
            //Debug.Log("Entered Gameplay State");
            //_ballController.PushBall();
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
