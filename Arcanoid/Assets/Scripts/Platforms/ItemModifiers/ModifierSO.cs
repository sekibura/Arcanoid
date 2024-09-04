using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public abstract class ModifierSO : ScriptableObject
    {
        public abstract void AffectModifier(float val);
    }
}
