using SekiburaGames.Arkanoid.Gameplay;
using SekiburaGames.Arkanoid.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.System
{
    public class PauseState : GameState
    {
        private BallController _ballController;
        public PauseState(GameStateMachine stateMachine) : base(stateMachine)
        {
            _ballController = MonobehReferencesManager.Instance.FindByType<BallController>();
        }

        public override void Enter()
        {
            base.Enter();
            ViewManager.Show<PauseView>();
            _ballController.StopMovement();
        }
    }
}