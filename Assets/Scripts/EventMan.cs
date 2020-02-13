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


#region Gameplay events 

    public static UnityAction<BallVis> onBallDieCallback;

    public static void AddBallDieListener(UnityAction<BallVis> listener){
        onBallDieCallback += listener;
    }

    public static void OnBallDie(BallVis ball){
        if (onBallDieCallback != null)
            onBallDieCallback(ball);
    }

    public static UnityAction<int> onPlayerLivesChangeCallback;

    public static void AddPlayerLivesChangeListener(UnityAction<int> listener){
        onPlayerLivesChangeCallback += listener;
    }

    public static void OnPlayerLivesChange(int lives){
        if (onPlayerLivesChangeCallback != null)
            onPlayerLivesChangeCallback(lives);
    }

    public static UnityAction<int> onLevelChangeCallback;

    public static void AddLevelChnageCallback(UnityAction<int> listener){
        onLevelChangeCallback += listener;
    }

    public static void OnLevelChange(int level){
        if (onLevelChangeCallback != null)
            onLevelChangeCallback(level);
    }


    public static UnityAction<BlockVis> onBlockDieCallback;

    public static void AddBlockDieListener(UnityAction<BlockVis> listener){
        onBlockDieCallback += listener;
    }

    public static void OnBlockDie(BlockVis ball){
        if (onBlockDieCallback != null)
            onBlockDieCallback(ball);
    }

    public static UnityAction<Collider2D> onBlockHitCallback;

    public static void AddBlockHitListener(UnityAction<Collider2D> listener){
        onBlockHitCallback += listener;
    }

    public static void OnBlockHit(Collider2D collider){
        if (onBlockHitCallback != null)
            onBlockHitCallback(collider);
    }

#region bonus 
    public static UnityAction<BonusVis> onBonusDieCallback;

    public static void AddBonusDieListener(UnityAction<BonusVis> listener){
        onBonusDieCallback += listener;
    }

    public static void OnBonusDie(BonusVis bonus){
        if (onBonusDieCallback != null)
            onBonusDieCallback(bonus);
    }

    public static UnityAction<BonusVis> onBonusCatchCallback;

    public static void AddBonusCatchListener(UnityAction<BonusVis> listener){
        onBonusCatchCallback += listener;
    }

    public static void OnBonusCatch(BonusVis bonus){
        if (onBonusCatchCallback != null)
            onBonusCatchCallback(bonus);
    }
#endregion

#endregion
}
