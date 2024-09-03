//This script keeps track of the current game-state and warns other scripts when the game-state happens to change.
//Place a unique instance of it somewhere in your game, on a dummy GameObject.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static SekiburaGames.Arcanoid.System.StaticData;

namespace SekiburaGames.Arcanoid.System
{
    public class GameStatesManager : MonoBehaviour {

        public AvailableGameStates gameStateAtLaunch = AvailableGameStates.Menu;

        //The emitter
        public UnityEvent GameStateChanged;
	
        //The following variable contains the current GameState.
	    public static AvailableGameStates gameState {get; private set;}
        private readonly Stack<AvailableGameStates> _history = new Stack<AvailableGameStates>();

        #region SingletonPattern
        public static GameStatesManager Instance {get; private set;}

        void Awake() {
            //Makes this script a singleton
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);

            //Sets the GameState at launch
            ChangeGameStateTo(gameStateAtLaunch);

            //Registers the events
            GameStateChanged = new UnityEvent();       
        }
        #endregion SingletonPattern

	    //Call this function from anywhere to request a game state change
	    public void ChangeGameStateTo(AvailableGameStates desiredState, bool remember = true) {
            if (desiredState == gameState)
                return;

            if(remember)
                Instance._history.Push(gameState);

            gameState = desiredState;
		    GameStateChanged.Invoke();
            Debug.Log($"ChangeGameStateTo {gameState}");
        }

        public void ChangeGameStateToLast()
        {
            if (Instance._history.Count != 0)
            {
                gameState = _history.Pop();
                GameStateChanged.Invoke();
                Debug.Log($"ChangeGameStateTo {gameState}");
            }
        }
    }

    //This static class makes all the possible game states available to any script.
    //You may insert in this enumeration any other game state your game may require.
    public static class StaticData {
	    public enum AvailableGameStates {
		    Menu,	//Consulting the menu
		    Starting,	//Game is starting
		    Playing,	//Game is playing
		    Tutorial,	//Game in tutorial mode
		    Pausing, //Game is paused
            Ending  //Game is ending
	    };
    }
}
