using SekiburaGames.Arkanoid.Audio;
using SekiburaGames.Arkanoid.System;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SekiburaGames.Arkanoid.System.StaticData;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public class BallController : MonoBehaviour
    {
        [SerializeField]
        private int _damage = 1;
        [SerializeField]
        private float _pushForce = 1000;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        private GameObject _playerGameObject;
        private Renderer _playerRenderer;
        private GameStateMachine _stateMachine;
        private Vector3 _collPos;
        private Vector2 _lastVelocity;

        void Start()
        {
            InitPlayerFields();
            SystemManager.Get(out _stateMachine);
            _stateMachine.GameStateChangedEvent += GameStateUpdated;
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
            Debug.Log($"[BallController]: {collision.transform.tag.ToString()} Velocity:{_rigidbody2D.velocity.ToString()}");
            if (collision.transform.CompareTag("Platform"))
            {

                var platform = collision.gameObject.GetComponent<BasePlatformItem>();
                if (platform != null)
                    platform.ApplyDamage(_damage);
                SoundManager.instance.PlaySound(SoundManager.Sound.Hurt);
                CheckVelocity();
            }
            else if (collision.transform.CompareTag("Player"))
            {
                Vector2 newVelocity = RecalcBallVelocity(collision.GetContact(0).point);
                _collPos = collision.GetContact(0).point;
                _rigidbody2D.velocity = newVelocity;
                SoundManager.instance.PlaySound(SoundManager.Sound.Jump);
            }
            else
            {
                SoundManager.instance.PlaySound(SoundManager.Sound.Tap);
            }
        }

        private void CheckVelocity()
        {
            if (Mathf.Abs(_rigidbody2D.velocity.y) < 5)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 2);
            }
        }

        //ѕлатформа поделена на 3 части:
        //перва€ лева€ - отбрасывает м€чик влево
        //средн€€ - не измен€ет отскок
        //права€ - отбрасывает вправо
        private Vector2 RecalcBallVelocity(Vector3 collisionPosition)
        {
            Vector2 newVelocity = Vector3.zero;
            float leftPointX = _playerRenderer.gameObject.transform.position.x - _playerRenderer.bounds.size.x / 2;
            float sideValue = UnityEngine.Random.Range(3, 7);
            if (collisionPosition.x > leftPointX + ((_playerRenderer.bounds.size.x/3)*2))
            {
                //права€ часть
                Debug.Log("[BallController]: ѕрава€ часть");
                if (Mathf.Abs(_rigidbody2D.velocity.x) < 1)                    
                    newVelocity = new Vector2(_rigidbody2D.velocity.x < 0 ? -sideValue : sideValue, _rigidbody2D.velocity.y);
                else
                newVelocity = new Vector2(_rigidbody2D.velocity.x < 0 ? -_rigidbody2D.velocity.x : _rigidbody2D.velocity.x, _rigidbody2D.velocity.y);
            }
            else if(collisionPosition.x > leftPointX + (_playerRenderer.bounds.size.x / 3))
            {
                //средн€€ часть
                Debug.Log("[BallController]: —редн€€ часть");
                newVelocity = _rigidbody2D.velocity;
            }
            else if(collisionPosition.x > leftPointX)
            {
                //лева часть
                Debug.Log("[BallController]: Ћева€ часть");
                if (Mathf.Abs(_rigidbody2D.velocity.x) < 1)
                    newVelocity = new Vector2(_rigidbody2D.velocity.x > 0 ? -sideValue : sideValue, _rigidbody2D.velocity.y);
                else
                    newVelocity = new Vector2(_rigidbody2D.velocity.x > 0 ? -_rigidbody2D.velocity.x : _rigidbody2D.velocity.x, _rigidbody2D.velocity.y);
            }

            Debug.Log($"[BallController]: {collisionPosition.x} {leftPointX} {(_playerRenderer.bounds.size.x / 3)} {(_playerRenderer.bounds.size.x / 3)*2} {leftPointX + ((_playerRenderer.bounds.size.x / 3) * 2)}");
            return newVelocity;
        }

        //private void OnDrawGizmos()
        //{
        //    float leftPointX = _playerRenderer.gameObject.transform.position.x - _playerRenderer.bounds.size.x / 2;
        //    float leftPointy = _playerRenderer.gameObject.transform.position.y + _playerRenderer.bounds.size.y / 2;
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawSphere(new Vector3(leftPointX, leftPointy, 0), 0.1f);
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawSphere(new Vector3(leftPointX + (_playerRenderer.bounds.size.x / 3), leftPointy, 0), 0.1f);
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawSphere(new Vector3(leftPointX + (_playerRenderer.bounds.size.x / 3) * 2, leftPointy, 0), 0.1f);
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawSphere(new Vector3(leftPointX + (_playerRenderer.bounds.size.x / 3) * 3, leftPointy, 0), 0.1f);
        //    Gizmos.color = Color.black;
        //    Gizmos.DrawSphere(_collPos, 0.1f);
        //}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("GameOverBound"))
            {
                _stateMachine.ChangeState(new GameOverZoneState(_stateMachine));
                SoundManager.instance.PlaySound(SoundManager.Sound.Explosion);
            }
        }

        private void GameStateUpdated(GameState gameState)
        {
            if (_stateMachine.IsCurrentState<GameplayState>() && _stateMachine.IsLastState<ResetState>())
            {
                PushBall();
            }
        }

        public void ResetBallState()
        {
            Debug.Log($"[BallController]: ResetBallState!");
            BallToStart();
            _rigidbody2D.velocity = Vector3.zero;
        }

        private void BallToStart()
        {
            if (_playerGameObject == null)
                return;

            gameObject.transform.parent = _playerGameObject.transform;
            gameObject.transform.localPosition= new Vector3(0, _playerRenderer.bounds.size.y/2, 0);
        }

        public void PushBall()
        {
            Debug.Log($"[BallController]: PushBall!");
            gameObject.transform.parent = null;
            float X = UnityEngine.Random.Range(-20, 20);
            _rigidbody2D.AddForce(new Vector2(X, 20) * _pushForce);
        }


        public void StopMovement()
        {
            _lastVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void ContinueMovement()
        {
            _rigidbody2D.velocity = _lastVelocity;
        }

        private void FixedUpdate()
        {
            CheckVelocity();
        }
    }
}
