using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SekiburaGames.Arcanoid.Audio
{
    public class ButtonClickSound : MonoBehaviour
    {
        private Button _btn;
        [SerializeField]
        private SoundManager.Sound _sound = SoundManager.Sound.Click;

        private void Start()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(() => SoundManager.instance.PlaySound(_sound));
        }

        private void OnDestroy()
        {
            //_btn.onClick.RemoveListener(() => SoundManager.instance.PlaySound(_sound));
        }
    }
}
