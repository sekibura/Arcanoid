using Lean.Localization;
using SekiburaGames.Arkanoid.Gameplay;
using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SekiburaGames.Arkanoid.UI
{
    public class UI_ScoreController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _scoreText;
        private ScoreController _scoreController;
        private void Start()
        {
            SystemManager.Get(out _scoreController);
            _scoreController.ScoreUpdatedEvent += OnScoreUpdated;
            OnScoreUpdated(_scoreController.Score);
            LeanLocalization.OnLocalizationChanged += ()=> OnScoreUpdated(_scoreController.Score);
        }

        private void OnScoreUpdated(int value)
        {
            _scoreText.text = $"{LeanLocalization.GetTranslationText("GameplayUI/Score")}: {value.ToString()}";
        }

    }
}
