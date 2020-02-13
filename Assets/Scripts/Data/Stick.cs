using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

// model 
// visual

public class Stick : IUpdatable, IFixUpdatable, IBonusable
{
    StickModel _model;
    public StickModel model{
        get{return _model;}
        private set{_model = value;}
    }

    StickVis _visual;
    public StickVis visual{
        get{return _visual;}
        private set{_visual = value;}
    }

    public void Setup(StickModel m){
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
        visual = gobject.GetComponent<StickVis>();
   }

   public void ReturnVisual(){
      if (visual != null){
         GameMan.instance.GetEntityMan().ReturnEntity(visual);
         visual = null;
      }     
   }

    public void ApplyBonus(BonusModel m){
        switch(m.bonusCharacteristic){
            case BonusModel.BonusCharacteristic.Speed:
                float newSpeed = model.speed * m.value;
                visual.SetParams(newSpeed);
            break;

            default:

            break;
        }
    }

    public void RemoveBonus(BonusModel m){
        switch(m.bonusCharacteristic){
            case BonusModel.BonusCharacteristic.Speed:
                visual.SetParams(model.speed);
            break;

            default:

            break;
        }
    }

    public void PlaceToCenter(){    
        visual.PlaceToCenter();
    }

    public void UpdatePhysics(float delta){
        visual.UpdatePhysics(delta);
    }

    public void UpdateMe(float delta){
        visual.UpdateMe(delta);
    }

    
}
