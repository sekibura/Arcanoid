using SekiburaGames.Arkanoid.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public class ScoreController : IInitializable
    {

        #region props
        private double _score;
        public double Score
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

        public event Action<double> ScoreUpdatedEvent;
        public void Initialize()
        {
            
        }

        public bool UpdateScore(double delta)
        {
            if (Score + delta < 0)
                return false;

            Score = Score + delta > 0 ? Score + delta : 0;

            if (delta != 0)
                ScoreUpdatedEvent?.Invoke(Score);

            return true;
        }

        public void Dispose()
        {
            //SaveProgress();
        }
    }
}
