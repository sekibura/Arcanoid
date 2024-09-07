using SekiburaGames.Arkanoid.Gameplay;
using SekiburaGames.Arkanoid.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.System
{
    public class ResetState : GameState
    {
        private InputController _inputController;
        private ScoreController _scoreController;
        private LifesController _lifesController;
        private PlayerMovement _playerMovement;
        private BallController _ballController;
        public ResetState(GameStateMachine stateMachine) : base(stateMachine)
        {
            SystemManager.Get(out _inputController);
            SystemManager.Get(out _scoreController);
            SystemManager.Get(out _lifesController);
            _ballController = MonobehReferencesManager.Instance.FindByType<BallController>();
            _playerMovement = MonobehReferencesManager.Instance.FindByType<PlayerMovement>();
        }

        public override void Enter()
        {
            base.Enter();
            ViewManager.Show<GameplayView>();
            _inputController.PlayGamePerformed += PlayGameButtonPressed;


            if(_lifesController.Lifes <= 0)
            {
                _scoreController.ResetScoreValue();
                _lifesController.ResetLifesValue();
                //TODO
                //_levelConntroller.ResetPlatforms;
            }
                

            if(_ballController!=null)
                _ballController.ResetBallState();
            
            if(_playerMovement!=null)
                _playerMovement.ResetPlayerPosition();
        }

        public override void Exit()
        {
            base.Exit();
            _inputController.PlayGamePerformed -= PlayGameButtonPressed;
        }

        private void PlayGameButtonPressed()
        {
            stateMachine.ChangeState(new GameplayState(stateMachine));
        }
    }
}