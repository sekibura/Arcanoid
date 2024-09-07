using SekiburaGames.Arkanoid.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.System
{
    public class GamePlayController : MonoBehaviour
    {
        private GameStateMachine stateMachine;

        void Start()
        {
            stateMachine = SystemManager.Get<GameStateMachine>(); 
            stateMachine.ChangeState(new ResetState(stateMachine));
        }

        void Update()
        {
            stateMachine.UpdateState();
        }
    }
}
