using SekiburaGames.Arkanoid.Gameplay;
using SekiburaGames.Arkanoid.Gameplay.SekiburaGames.Arkanoid.Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace SekiburaGames.Arkanoid.System
{
    public class ScriptablObjectController : MonoBehaviour
    {
        private static ScriptablObjectController _instance;

        public static ScriptablObjectController Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                else
                    return FindObjectOfType<ScriptablObjectController>();
            }
            private set { _instance = value; }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance == this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        #region Platforms
        [SerializeField]
        protected LevelsSO _levels;
        public LevelsSO GetLevelsData()
        {
            return _levels;
        }
        #endregion
    }
}
