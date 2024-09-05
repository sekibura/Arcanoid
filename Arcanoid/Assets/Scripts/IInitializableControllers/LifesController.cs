using SekiburaGames.Arkanoid.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static SekiburaGames.Arkanoid.System.StaticData;
using static SekiburaGames.Arkanoid.System.TimersController;


namespace SekiburaGames.Arkanoid.Gameplay
{
    /// <summary>
    /// Максимум 3 жизни
    /// </summary>
    public class LifesController : System.IInitializable
    {
        private int _defaultLifesValue = 3;

        #region props
        private int _lifes;
        public int Lifes
        {
            get
            {
                return _lifes;
            }
            private set
            {
                _lifes = value;
                LifesUpdatedEvent?.Invoke(Lifes);
            }
        }
        #endregion

        public event Action<int> LifesUpdatedEvent;
        public void Initialize()
        {
            ResetLifesValue();
            GameStatesManager.Instance.GameStateChanged.AddListener(GameStateUpdated);
            //TestValue();
        }

        public bool UpdateLifes(int delta)
        {
            if (Lifes + delta < 0)
                return false;

            Lifes = Lifes + delta > 0 ? Lifes + delta : 0;

            return true;
        }

        private void GameStateUpdated()
        {
            switch (GameStatesManager.gameState)
            {
                case AvailableGameStates.Menu:
                    break;
                case AvailableGameStates.Starting:
                    ResetLifesValue();
                    break;
                case AvailableGameStates.Playing:
                    break;
                case AvailableGameStates.Tutorial:
                    break;
                case AvailableGameStates.Pausing:
                    break;
                case AvailableGameStates.Ending:
                    break;
                default:
                    break;
            }
        }

        private void ResetLifesValue()
        {
            Debug.Log($"[LifesController]: ResetLifesValue!");
            Lifes = _defaultLifesValue;
        }

        public void Dispose()
        {
            //SaveProgress();
        }

        private void TestValue()
        {
            TimerData timer = TimersController.Instance.StartTimer(() => TestUpdateLifes(), 1, true);
        }

        private void TestUpdateLifes()
        {            
            int newValue = Lifes - 1 < 0 ? 3 : Lifes - 1;
            Debug.Log("Tick " + newValue);
            Lifes = newValue;
        }
    }
}
