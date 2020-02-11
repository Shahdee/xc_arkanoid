using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

// model
// visual 

public class Ball : IUpdatable, IFixUpdatable
{
    BallModel _model;

    public BallModel model{
        get{return _model;}
        private set{_model = value;}
    }

    BallVis _visual;

    public BallVis visual{
        get{return _visual;}
        private set{_visual = value;}
    }

    public void Setup(BallModel m){
        model = m;
        
    }

    public void CreateVisual(){
        //   GameObject gobject = MainLogic.GetMainLogic().GetEntityManager().GetEntity(meta.assetName);
        //   gobject.SetActive(true);     
        //   visual = gobject.GetComponent<SushiVisual>();

        //   Debug.Log("CreateVisual " + visual);

        //TODO subscribe to events - ? 

   }

   public void ReturnVisual(){
      // TODO unsubscribe from events 
      // return to pool 
    //   if (visual != null){
    //      MainLogic.GetMainLogic().GetEntityManager().ReturnEntity(visual);
    //      visual = null;
    //   }     
   }

    public void PutOnStick(Stick stick){

        // position
        // collider off 

        SetResting(true);

    }

    void SetResting(bool resting){

    }

    public void Impulse(){
        // select direction
            // opposite direction of stick movement ? 
        // make move in that direction 

        // start untouchable timer 

    }


    // the ball should be able to rest on stick, until stick makes a move and gives impulse to the ball 

    // physics 


    // QA
    // select direction upon collision 

    // Collider2D.IsTouchingLayers 
    // or untouchable during N seconds 

    public void UpdateMe(float delta){

    }

    public void FixUpdateMe(float delta){

    }
   
}
