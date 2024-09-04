using SekiburaGames.Arkanoid.Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace StarGames.Digger.System
{
    public class ScriptablObjectController : MonoBehaviour
    {
        private static ScriptablObjectController _instance;

        public static ScriptablObjectController Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                else
                    return FindObjectOfType<ScriptablObjectController>();
            }
            private set { _instance = value; }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance == this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        #region Platforms
        [SerializeField]
        protected PlatformItemsSO _platformsData;
        [SerializeField]
        protected List<BasePlatformItem> _inventoryItems;
        public PlatformItemsSO GetInventoryData()
        {
            return _platformsData;
        }
        public BasePlatformItem GetItemByName(string name)
        {
            BasePlatformItem value = _inventoryItems.Find(x => x.Names.Any(s => s == name));
            if (value != null)
                return value;
            else
            {
                Debug.LogError($"[ScriptablObjectController]: There is no object with name: {name} ");
                return null;
            }
        }
        #endregion
    }
}
