using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _platformPrefabs; // ������ �������� ��������
        private List<BasePlatformItem> _platformItems = new List<BasePlatformItem>();
        private GameStateMachine _gameStateMachine;

        private int[,] levelMatrix = new int[,]
        {
        { 1, 1, 0, 1 },
        { 0, 1, 1, 0 },
        { 1, 0, 1, 1 }
        };

        private int rows;
        private int cols;
        [SerializeField]
        private float _spacing = 0.1f; // ���������� ����� �����������

        void Start()
        {
            SystemManager.Get(out _gameStateMachine);
            rows = levelMatrix.GetLength(0);
            cols = levelMatrix.GetLength(1);
            BuildLevel();
        }

        public void BuildLevel()
        {
            // ��������� ������ ���������, ����� ��� ���������� � �����
            float screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
            float screenHeight = Camera.main.orthographicSize * 2;
            float platformWidth = (screenWidth - (cols - 1) * _spacing) / cols;
            float platformHeight = _platformPrefabs[0].GetComponent<Renderer>().bounds.size.y;

            // ��������� ��������� ������� �� ��� X
            float startX = -screenWidth / 2 + platformWidth / 2;
            float startY = screenHeight/2 - screenHeight * 0.1f - platformHeight/2;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int platformType = levelMatrix[i, j];
                    if (platformType > 0 && platformType <= _platformPrefabs.Length)
                    {
                        // ��������� ������� ���������
                        float posX = startX + j * (platformWidth + _spacing);
                        float posY = startY - i * (_platformPrefabs[platformType - 1].transform.localScale.y + _spacing);

                        Vector3 position = new Vector3(posX, posY, 0);  // -posY, ����� ������ ���������� ������ ����
                        GameObject platform = Instantiate(_platformPrefabs[platformType - 1], position, Quaternion.identity);
                        platform.transform.localScale = new Vector3(platformWidth, platform.transform.localScale.y, platform.transform.localScale.z);
                        platform.transform.parent = transform;
                        
                        var a = platform.GetComponent<BasePlatformItem>();
                        a.PlatformDestroyedEvent += PlatformDestroyed;
                        _platformItems.Add(platform.GetComponent<BasePlatformItem>());
                    }
                }
            }
        }

        private void PlatformDestroyed(BasePlatformItem item)
        {
            _platformItems.Remove(item);
            if(_platformItems.Count == 0)
            {
                //��� ��������� ���������, �� �������� ���.
                _gameStateMachine.ChangeState(new YouWinState(_gameStateMachine));
            }
        }
    }
}
