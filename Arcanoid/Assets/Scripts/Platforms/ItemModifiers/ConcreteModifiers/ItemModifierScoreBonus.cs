using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.Gameplay
{
    [CreateAssetMenu(fileName = "ModifierScoreBonus", menuName = "Modifiers/Score Bonus")]
    public class ItemModifierScoreBonus : ModifierSO
    {
        public override void AffectModifier(float val)
        {
            //Bonus score add
        }
    }
}