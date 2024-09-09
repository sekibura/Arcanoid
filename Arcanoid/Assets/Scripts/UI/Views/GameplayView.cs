using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SekiburaGames.Arkanoid.UI
{
    public class GameplayView : View
    {
        [SerializeField]
        private Button _pauseBtn;
        private GameStateMachine _gameStateMachine;

        public override void Initialize()
        {
            base.Initialize();
            SystemManager.Get(out _gameStateMachine);
            _pauseBtn.onClick.AddListener(() => OnPauseButtonPressed());
        }

        private void OnPauseButtonPressed()
        {
//            ViewManager.Show<PauseView>();
            _gameStateMachine.ChangeState(new PauseState(_gameStateMachine));
        }

    }
}
