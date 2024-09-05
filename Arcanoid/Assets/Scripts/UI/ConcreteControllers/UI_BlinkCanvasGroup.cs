using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SekiburaGames.Arkanoid.System.TimersController;

namespace SekiburaGames.Arkanoid.UI
{
    public class UI_BlinkCanvasGroup : MonoBehaviour
    {
        [SerializeField]
        private float _blinkStep;
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private bool _blinkOnStart;

        private TimerData _timerData;

        private void Start()
        {
            if (!_blinkOnStart)
                return;

            StartBlink();
        }

        public void StartBlink()
        {
            _timerData = TimersController.Instance.StartTimer(() => CanvasBlinking(), _blinkStep, true);
        }

        public void StopBlink()
        {
            TimersController.Instance.StopTimer(_timerData);
        }

        private void CanvasBlinking()
        {
            _canvasGroup.alpha = _canvasGroup.alpha == 1 ? 0 : 1;
        }
    }
}
