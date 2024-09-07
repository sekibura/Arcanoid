using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SekiburaGames.Arkanoid.System
{
    public class MonobehReferencesManager : MonoBehaviour
    {

        #region SingletonPattern
        public static MonobehReferencesManager Instance { get; private set; }

        void Awake()
        {
            //Makes this script a singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);

        }
        #endregion SingletonPattern

        public T FindByType<T>() where T : MonoBehaviour
        {
            return FindObjectOfType<T>();
        }

    }
}