using SekiburaGames.Arkanoid.Gameplay;
using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

            _homeBtnLvls.onClick.AddListener(() => { ResetContainers(); });
            _settingsHomeBtnLvls.onClick.AddListener(() => { ResetContainers(); });
            InitLevelsContainer();
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
        }
    }
}
