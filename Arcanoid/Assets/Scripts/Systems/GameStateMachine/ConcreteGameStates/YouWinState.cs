using SekiburaGames.Arkanoid.Gameplay;
using SekiburaGames.Arkanoid.System;
using SekiburaGames.Arkanoid.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.System
{
    //Состояние когда все платформы уничтожены
    public class YouWinState : GameState
    {
        private BallController _ballController;
        public YouWinState(GameStateMachine stateMachine) : base(stateMachine)
        {
            _ballController = MonobehReferencesManager.Instance.FindByType<BallController>();
        }

        public override void Enter()
        {
            base.Enter();
            _ballController.StopMovement();
            ViewManager.Show<GameWinView>();

        }
    }
}
