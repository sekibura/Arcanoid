using SekiburaGames.Arkanoid.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.System
{
    //—осто€ние, когда м€ч попал в зону проигрыша. “ут решаетс€, проигрыш это или минус жизнь.
    public class GameOverZoneState : GameState
    {
        private ScoreController _scoreController;
        private LifesController _lifesController;
        private GameStateMachine _gameStateMachine;
        public GameOverZoneState(GameStateMachine stateMachine) : base(stateMachine)
        {
            SystemManager.Get(out _gameStateMachine);
            SystemManager.Get(out _scoreController);
            SystemManager.Get(out _lifesController);
        }

        public override void Enter()
        {
            base.Enter();
            _lifesController.UpdateLifes(-1);
            if(_lifesController.Lifes <= 0)
            {
                _gameStateMachine.ChangeState(new GameOverState(_gameStateMachine));
            }
            else
            {
                _gameStateMachine.ChangeState(new ResetState(_gameStateMachine));
            }
        }
    }
}
