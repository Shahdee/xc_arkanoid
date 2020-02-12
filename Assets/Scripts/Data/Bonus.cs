using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

public class Bonus 
{
    BonusModel _model;

    public BonusModel model{
        get{return _model;}
        private set{_model = value;}
    }

    BonusVis _visual;

    public BonusVis visual{
        get{return _visual;}
        private set{_visual = value;}
    }

    public void Setup(BonusModel m){
        model = m;

        CreateVisual();
        SetParams();
    }

    void SetParams(){
        visual.SetParams(model.speed);
    }

    public void Reset(){
        ReturnVisual();
    }

    void CreateVisual(){
        GameObject gobject = GameMan.instance.GetEntityMan().GetEntity(model.assetName);
        gobject.SetActive(true);     
        gobject.transform.SetParent(GameMan.instance.GetLevelMan().trsParent);
        visual = gobject.GetComponent<BonusVis>();
    }

    public void Place(Vector3 position){
        visual.PlaceTo(position);
    }

   public void ReturnVisual(){
      if (visual != null){
         GameMan.instance.GetEntityMan().ReturnEntity(visual);
         visual = null;
      }     
   }


    public void UpdatePhysics(float delta){
        if (visual != null)
            visual.UpdatePhysics(delta);
    }

    public void UpdateMe(float delta){
        if (visual != null)
            visual.UpdateMe(delta);
    }
}
