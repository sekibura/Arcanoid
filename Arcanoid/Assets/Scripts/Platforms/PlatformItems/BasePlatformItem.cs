using SekiburaGames.Arkanoid.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public abstract class BasePlatformItem : MonoBehaviour
    {
        [SerializeField]
        private int _health = 1;
        [SerializeField]
        private int _score = 1;

        public List<string> Names { get; set; }
        public List<ItemParameter> DefaultParametersList { get; set; }

        public Action<int> PlatformHitsUpdated;
        public Action<int> PlatformGetDamage;
        public Action<BasePlatformItem> PlatformDestroyedEvent;

        private ScoreController _scoreController;
        public virtual void ApplyDamage(int damage) 
        {
            SystemManager.Get(out _scoreController);
            _health = _health - damage < 0 ? 0 : _health - damage;
            if (_health <= 0)
                DestroyPlatform();
            PlatformGetDamage?.Invoke(damage);
            PlatformHitsUpdated?.Invoke(_health);
        }

        protected virtual void DestroyPlatform()
        {
            Debug.Log($"[Platform {gameObject.name}] - Was destroyed!");
            _scoreController.UpdateScore(_score);
            PlatformDestroyedEvent?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}
