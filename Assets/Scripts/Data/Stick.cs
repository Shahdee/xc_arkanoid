using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

// model 
// visual

public class Stick 
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


    public void PutToDefaultPlace(){
        // Screen.width

    }

    // QA 
    // from where to control the stick movement?
}
