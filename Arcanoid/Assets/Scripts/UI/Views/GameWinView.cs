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
        [SerializeField]
        private Button _mainMenuBtn;
        private GameStateMachine _gameStateMachine;
        private LevelManager _levelManager;

        public override void Initialize()
        {
            base.Initialize();
            SystemManager.Get(out _gameStateMachine);
            _levelManager = FindObjectOfType<LevelManager>();
            _restartBtn.onClick.AddListener(() =>_gameStateMachine.ChangeState(new ResetState(_gameStateMachine)));
            _nextLvlBtn.onClick.AddListener(() => 
            {
                _levelManager.LoadNextLvl();
                _gameStateMachine.ChangeState(new ResetState(_gameStateMachine));
            }
            );
            _mainMenuBtn.onClick.AddListener(() => _gameStateMachine.ChangeState(new MainMenuState(_gameStateMachine)));
        }

        public override void Show(object parameter = null)
        {
            base.Show(parameter);
            //_nextLvlBtn.gameObject.SetActive(_levelManager.IsNextLevelExist());
        }
    }
}
