using SekiburaGames.Arkanoid.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SekiburaGames.Arkanoid.System.StaticData;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public class ScoreController : IInitializable
    { 

        #region props
        private int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            private set
            {
                _score = value;
                ScoreUpdatedEvent?.Invoke(Score);
            }
        }
        #endregion

        public event Action<int> ScoreUpdatedEvent;
        public void Initialize()
        {
            ResetScoreValue();
            GameStatesManager.Instance.GameStateChanged.AddListener(GameStateUpdated);
        }

        public bool UpdateScore(int delta)
        {
            if (Score + delta < 0)
                return false;

            Score = Score + delta > 0 ? Score + delta : 0;

            if (delta != 0)
                ScoreUpdatedEvent?.Invoke(Score);

            return true;
        }

        private void GameStateUpdated()
        {
            switch (GameStatesManager.gameState)
            {
                case AvailableGameStates.Menu:
                    break;
                case AvailableGameStates.Starting:
                    ResetScoreValue();
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

        private void ResetScoreValue()
        {
            Debug.Log($"[ScoreController]: ResetScoreValue!");
            Score = 0;
        }

        public void Dispose()
        {
            //SaveProgress();
        }
    }
}
