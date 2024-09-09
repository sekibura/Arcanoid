using SekiburaGames.Arkanoid.Gameplay;
using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SekiburaGames.Arkanoid.UI
{
    public class MainMenuView : View
    {

        [SerializeField]
        private Button _levelsBtn;
        [SerializeField]
        private GameObject _levelsContainer;
        [SerializeField]
        private Button _settingsBtn;
        [SerializeField]
        private GameObject _settingsContainer;

        [SerializeField]
        private Button _exitBtn;

        [Header("Levels Container")]
        [SerializeField]
        private Button _homeBtnLvls;

        [Header("SettingsnnContainer")]
        [SerializeField]
        private Button _settingsHomeBtnLvls;

        public override void Initialize()
        {
            base.Initialize();
            ResetContainers();
            _levelsBtn.onClick.AddListener(() =>
            {
                ResetContainers();
                _levelsContainer.SetActive(true);
            });

            _settingsBtn.onClick.AddListener(() =>
            {
                _settingsContainer.SetActive(true);
            });

            _exitBtn.onClick.AddListener(() =>
            {
                Application.Quit();
            });

            _homeBtnLvls.onClick.AddListener(() => { ResetContainers(); });
            _settingsHomeBtnLvls.onClick.AddListener(() => { ResetContainers(); });
            InitLevelsContainer();
            InitSettingsContent();
        }
        private void ResetContainers()
        {
            _levelsContainer.SetActive(false);
            _settingsContainer.SetActive(false);
        }


        #region lvls

        [SerializeField]
        private Button[] _lvlsButtons;
        private LevelManager _levelManager;
        private GameStateMachine _gameStateMachine;

        private void InitLevelsContainer()
        {
            SystemManager.Get(out _gameStateMachine);
            _levelManager = FindObjectOfType<LevelManager>();
            InitButtons();
        }
        private void InitButtons()
        {
            int lvlvsCount = ScriptablObjectController.Instance.GetLevelsData().Levels.Length;

            for (int i = 0; i < _lvlsButtons.Length; i++)
            {                
                if(i < lvlvsCount)
                {
                    int lvlNumber = i;
                    _lvlsButtons[i].onClick.AddListener(() => LoadLevel(lvlNumber));
                    _lvlsButtons[i].gameObject.SetActive(true);
                    _lvlsButtons[i].transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
                }
                else
                    _lvlsButtons[i].gameObject.SetActive(false);
            }
        }

        private void LoadLevel(int number)
        {
            Debug.Log($"[MainMenuView]: Load {number}lvl");
            _levelManager.LoadLevel(number);
            _gameStateMachine.ChangeState(new ResetState(_gameStateMachine));
        }
        #endregion

        public void OpenMyGithub()
        {
            Application.OpenURL("https://github.com/sekibura");
        }
        public override void Show(object parameter = null)
        {
            base.Show(parameter);
            ResetContainers();
            SettingsShow();
        }

        #region Settings
        [SerializeField]
        private Slider _sliderMusic;
        [SerializeField]
        private Slider _sliderEffects;
        [SerializeField]
        private UI_LanguageChooseController _languageChooseController;
        [SerializeField]
        private AudioMixerGroup _audioMixer;
        private bool _isSetSliderValues = false;


        private void InitSettingsContent()
        {
            _sliderEffects.onValueChanged.AddListener((value) => OnSoundEffectsSliderChange(value));
            _sliderMusic.onValueChanged.AddListener((value) => OnMusicSliderChange(value));

        }

        private void SettingsShow()
        {
            _languageChooseController.UpdateButtonStates();
        }
        private void OnSoundEffectsSliderChange(float value)
        {
            //if (_isSetSliderValues)
            //    return;
            Debug.Log($"OnSoundEffectsSliderChange {value}");
            _audioMixer.audioMixer.SetFloat("Effects", Mathf.Log10(value) * 20);
        }

        private void OnMusicSliderChange(float value)
        {
            //if (_isSetSliderValues)
            //    return;
            _audioMixer.audioMixer.SetFloat("BackgroundMusic", Mathf.Log10(value) * 20);
        }
        #endregion
    }
}
