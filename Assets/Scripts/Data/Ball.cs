using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

// model
// visual 

public class Ball : IUpdatable, IFixUpdatable, IBonusable
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
        
        CreateVisual();
        SetParams();
    }

    public void Reset(){
        SetParams();
        ReturnVisual();
    }   

    void SetParams(){
        visual.SetParams(model.speed);
    }

    void CreateVisual(){
        GameObject gobject = GameMan.instance.GetEntityMan().GetEntity(model.assetName);
        gobject.SetActive(true);     
        gobject.transform.SetParent(GameMan.instance.GetLevelMan().trsParent);
        visual = gobject.GetComponent<BallVis>();
   }

   public void ReturnVisual(){
      if (visual != null){
         GameMan.instance.GetEntityMan().ReturnEntity(visual);
         visual = null;
      }     
   }

    public void PutOnStick(Stick stick){
        visual.RestOnObject(stick.visual);
    }

   public void ApplyBonus(BonusModel model){
        switch(model.bonusCharacteristic){
            case BonusModel.BonusCharacteristic.Speed:
                float newSpeed = model.speed * model.value;
                visual.SetParams(newSpeed);
            break;

            default:

            break;
        }
    }

   public void RemoveBonus(BonusModel model){
        switch(model.bonusCharacteristic){
            case BonusModel.BonusCharacteristic.Speed:
                visual.SetParams(model.speed);
            break;

            default:

            break;
        }
    }

    // Collider2D.IsTouchingLayers 
    // or untouchable during N seconds 
    // IsTouching

    public void UpdatePhysics(float delta){
        visual.UpdatePhysics(delta);
    }

    public void UpdateMe(float delta){
        visual.UpdateMe(delta);
    }


   
}
