using SekiburaGames.Arkanoid.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.System
{
    public class MainMenuState : GameState
    {
        public MainMenuState(GameStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ViewManager.Show<MainMenuView>();
        }
    }
}
