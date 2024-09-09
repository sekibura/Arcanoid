using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SekiburaGames.Arkanoid.UI
{
    public class PauseView : View
    {
        [SerializeField]
        private Slider _sliderMusic;
        [SerializeField]
        private Slider _sliderEffects;
        [SerializeField]
        private UI_LanguageChooseController _languageChooseController;
        [SerializeField]
        private AudioMixerGroup _audioMixer;
        private bool _isSetSliderValues = false;

        [SerializeField]
        private Button _helpBtn;
        [SerializeField]
        private GameObject _helpView;

        [SerializeField]
        private Button _mainMenuScene;

        [SerializeField]
        private Button _resumeBtn;

        private GameStateMachine _gameStateMachine;

        public override void Initialize()
        {
            base.Initialize();
            SystemManager.Get(out _gameStateMachine);
            _sliderEffects.onValueChanged.AddListener((value) => OnSoundEffectsSliderChange(value));
            _sliderMusic.onValueChanged.AddListener((value) => OnMusicSliderChange(value)); 
            _helpBtn.onClick.AddListener(() => _helpView.SetActive(true));
            _mainMenuScene.onClick.AddListener(() => _gameStateMachine.ChangeState(new MainMenuState(_gameStateMachine)));
            _resumeBtn.onClick.AddListener(() => OnResumeButtonPressed());
        }

        public override void Show(object parameter = null)
        {
            base.Show(parameter);
            _languageChooseController.UpdateButtonStates();
            SlidersUpdateValue();
        }

        private void SlidersUpdateValue()
        {
            _isSetSliderValues = true;
            //_sliderDialogs.value = saveData.SettingsDialogVolume;
            //_sliderMusic.value = saveData.SettingsMusicVolume;
            _isSetSliderValues = false;
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

        private void OnResumeButtonPressed()
        {
//            ViewManager.Show<GameplayView>();
            //_gameStateMachine.ChangeState(new GameplayState(_gameStateMachine));
            //var lastState = _gameStateMachine.LastState;
            _gameStateMachine.ChangeState(_gameStateMachine.LastState);

        }
    }
}
