using SekiburaGames.Arkanoid.System;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SekiburaGames.Arkanoid.Gameplay
{
    [CreateAssetMenu(fileName = "ModifierScoreBonus", menuName = "Modifiers/Score Bonus")]
    public class ItemModifierScoreBonus : ModifierSO
    {
        private ScoreController _scoreController;
        public override void AffectModifier(float val)
        {
            if (_scoreController == null)
                SystemManager.Get(out _scoreController);

            _scoreController.UpdateScore(Convert.ToInt32(val));
        }
    }
}