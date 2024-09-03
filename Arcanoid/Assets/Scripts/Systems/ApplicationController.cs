using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
//using UnityEngine.InputSystem;

namespace SekiburaGames.Arkanoid.System
{
    public class ApplicationController : MonoBehaviour
    {
        private static bool _isLoaded = false;

        void Awake()
        {
            if (_isLoaded)
                return;

            _isLoaded = true;
            SystemManager.Register(this);
            RegisterSystems();
            GetSystems();
            SetApplicationSettings();
            //GameStateManager.Instance.UpdateGameState(GameStateManager.GameState.InGame);
        }

        private void RegisterSystems()
        {
            SystemManager.Register<InputController>();
        }

        private void GetSystems()
        {
            SystemManager.Get<InputController>();
        }

        private void SetApplicationSettings()
        {

            Application.targetFrameRate = 60;

#if UNITY_WEBGL
            SetWebSettings();
#endif

#if UNITY_ANDROID || UNITY_IOS
            SetMobileSettings();
#endif
        }

        private void SetWebSettings()
        {

        }

        private void SetMobileSettings()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void OnDestroy()
        {
            SystemManager.Dispose();
            _isLoaded = false;
        }
    }
}