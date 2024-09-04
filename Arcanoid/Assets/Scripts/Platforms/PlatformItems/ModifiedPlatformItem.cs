using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public class ModifiedPlatformItem : BasePlatformItem, IItemAction
    {
        [SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();
        public string ActionName => "Consume";

        public bool PerformAction(List<ItemParameter> itemState = null)
        {
            foreach (ModifierData data in modifiersData)
            {
                data.Modifier.AffectModifier(data.value);
            }
            return true;
        }
    }
    public interface IItemAction
    {
        public string ActionName { get; }
        bool PerformAction(List<ItemParameter> itemState);
    }

    [Serializable]
    public class ModifierData
    {
        public ModifierSO Modifier;
        public float value;
    }
}
