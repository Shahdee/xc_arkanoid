using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventMan 
{
    public static UnityAction<GameMan.GameStates> onGameStateChangeCallback;
    public static void AddGameStateChangeListener(UnityAction<GameMan.GameStates> listener){
        onGameStateChangeCallback += listener;
    }

    public static void RemoveGameStateChangeListener(UnityAction<GameMan.GameStates> listener){
        onGameStateChangeCallback -= listener;
    }

    public static void OnGameStateChange(GameMan.GameStates state){
        if (onGameStateChangeCallback != null)
            onGameStateChangeCallback(state);
    }


#region Game starts and ends
    public static UnityAction onGameStartCallback;

    public static void AddGameStartListener(UnityAction listener){
        onGameStartCallback += listener;
    }

    public static void OnGameStart(){
        if (onGameStartCallback != null)
            onGameStartCallback();
    }

    public static UnityAction onGameEndedCallback;

    public static void AddGameEndedListener(UnityAction listener){
        onGameEndedCallback += listener;
    }

    public static void OnGameEnded(){
        if (onGameEndedCallback != null)
            onGameEndedCallback();
    }
#endregion
}
