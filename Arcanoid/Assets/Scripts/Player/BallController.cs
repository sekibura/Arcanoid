using SekiburaGames.Arkanoid.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SekiburaGames.Arkanoid.System.StaticData;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public class BallController : MonoBehaviour
    {
        [SerializeField]
        private int _damage = 1;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        private GameObject _playerGameObject;
        private Renderer _playerRenderer;

        void Start()
        {
            InitPlayerFields();
            GameStatesManager.Instance.GameStateChanged.AddListener(GameStateUpdated);
            ResetBallState();
        }

        private void InitPlayerFields()
        {

            var playerMovement = FindAnyObjectByType<PlayerMovement>();
            if (playerMovement != null)
            {
                _playerGameObject = playerMovement.gameObject;
                _playerRenderer = playerMovement.GetComponent<Renderer>();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.transform.CompareTag("Platform"))
                return;

            var platform = collision.gameObject.GetComponent<BasePlatformItem>();
            if (platform != null)
                platform.ApplyDamage(_damage);
        }

        private void GameStateUpdated()
        {
            switch (GameStatesManager.gameState)
            {
                case AvailableGameStates.Menu:
                    break;
                case AvailableGameStates.Starting:
                    ResetBallState();
                    break;
                case AvailableGameStates.Playing:
                    break;
                case AvailableGameStates.Tutorial:
                    break;
                case AvailableGameStates.Pausing:
                    break;
                case AvailableGameStates.Ending:
                    break;
                default:
                    break;
            }
        }

        private void ResetBallState()
        {
            Debug.Log($"[BallController]: ResetBallState!");
            BallToStart();
        }

        private void BallToStart()
        {
            if (_playerGameObject == null)
                return;

            gameObject.transform.parent = _playerGameObject.transform;
            gameObject.transform.localPosition= new Vector3(0, _playerRenderer.bounds.size.y/2, 0);
            //gameObject.transform.localPosition = new Vector3(0, 0, 0);
        }

    }
}
