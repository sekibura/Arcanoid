using SekiburaGames.Arkanoid.Gameplay;
using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace SekiburaGames.Arkanoid.UI
{
    public class GameWinView : View
    {
        [SerializeField]
        private Button _restartBtn;
        [SerializeField]
        private Button _nextLvlBtn;
        private GameStateMachine _gameStateMachine;

        public override void Initialize()
        {
            base.Initialize();
            SystemManager.Get(out _gameStateMachine);
            _restartBtn.onClick.AddListener(() =>_gameStateMachine.ChangeState(new ResetState(_gameStateMachine)));
            _nextLvlBtn.onClick.AddListener(() => _gameStateMachine.ChangeState(new ResetState(_gameStateMachine)));
        }
    }
}
