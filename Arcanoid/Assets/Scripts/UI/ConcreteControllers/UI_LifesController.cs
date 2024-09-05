using SekiburaGames.Arkanoid.Gameplay;
using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace SekiburaGames.Arkanoid.UI
{
    public class UI_LifesController : MonoBehaviour
    {
        private LifesController _lifesController;
        [SerializeField]
        private CanvasGroup[] _canvasGroupLifes;
        void Start()
        {
            SystemManager.Get(out _lifesController);
            _lifesController.LifesUpdatedEvent += LifesUpdated;
            LifesUpdated(_lifesController.Lifes);
        }

        private void LifesUpdated(int value)
        {
            for (int i = 0; i < _canvasGroupLifes.Length; i++)
            {
                _canvasGroupLifes[i].alpha = i < value ? 1 : 0;
            }
        }
    }
}
