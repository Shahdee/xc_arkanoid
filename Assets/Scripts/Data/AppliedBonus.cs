using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Game.Models;

public class AppliedBonus : IUpdatable
{

    public BonusModel model;

    float currTime;

    bool activated = false;

    public void Activate(BonusModel m){
        model = m;
        currTime = model.time;
        activated = true;
    }

    UnityAction<AppliedBonus> m_OnTimeElapseCallback;

    public void AddTimeElapseListener(UnityAction<AppliedBonus> listener){
        m_OnTimeElapseCallback += listener;
    }

    // public void RemoveTimeElapseListener(UnityAction<AppliedBonus> listener){
    //     m_OnTimeElapseCallback -= listener;
    // }

    void OnTimeElapse(){
        if (m_OnTimeElapseCallback != null)
            m_OnTimeElapseCallback(this);
    }
    
    public void UpdateMe(float deltatime){

        if (! activated) return;

        // Debug.Log("bonus t " + currTime);

        if (currTime > 0){
            currTime -= deltatime;
        }
        else{
            activated = false;
            OnTimeElapse();
        }
    }
}
