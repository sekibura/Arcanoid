using SekiburaGames.Arkanoid.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static SekiburaGames.Arkanoid.System.StaticData;


namespace SekiburaGames.Arkanoid.Gameplay
{
    public class LifesController : IInitializable
    {
        [SerializeField]
        private int _defaultLifesValue;

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
        }

        public bool UpdateLifes(int delta)
        {
            if (Lifes + delta < 0)
                return false;

            Lifes = Lifes + delta > 0 ? Lifes + delta : 0;

            if (delta != 0)
                LifesUpdatedEvent?.Invoke(Lifes);

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
    }
}
