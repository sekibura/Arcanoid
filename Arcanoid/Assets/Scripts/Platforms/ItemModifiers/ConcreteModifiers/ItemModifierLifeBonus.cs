using SekiburaGames.Arkanoid.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SekiburaGames.Arkanoid.Gameplay
{
    [CreateAssetMenu(fileName = "ModifierLifeBonus", menuName = "Modifiers/Life Bonus")]
    public class ItemModifierLifeBonus : ModifierSO
    {
        private LifesController _lifesController;
        public override void AffectModifier(float val)
        {
            if (_lifesController == null)
                SystemManager.Get(out _lifesController);

            _lifesController.UpdateLifes(Convert.ToInt32(val));
        }
    }
}
