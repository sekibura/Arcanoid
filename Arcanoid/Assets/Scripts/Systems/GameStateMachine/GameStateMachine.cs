using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SekiburaGames.Arkanoid.System
{
    public class GameStateMachine : IInitializable
    {
        //private GameState currentState;

        public GameState CurrentState { get; private set; }
        public Action<GameState> GameStateChangedEvent;
        public GameState LastState { get; private set; }

        public void ChangeState(GameState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            LastState = CurrentState;
            Debug.Log($"[GameStateMachine] state changed:  -> {newState.ToString()}");
            CurrentState = newState;
            CurrentState.Enter();
            GameStateChangedEvent?.Invoke(CurrentState);
        }

        public void UpdateState()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }

        // Метод для проверки текущего состояния
        public bool IsCurrentState<T>() where T : GameState
        {
            return CurrentState is T;
        }

        public bool IsCurrentState<T>(T gamestate) where T : GameState
        {
            return CurrentState is T;
        }
        public bool IsLastState<T>() where T : GameState
        {
            return LastState is T;
        }
        public bool IsLastState<T>(T gamestate) where T : GameState
        {
            return LastState is T;
        }

        //public bool IsTransition<T>(T from, T to) where T: GameState
        //{
        //    return LastState is T && CurrentState is T;
        //}

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}
