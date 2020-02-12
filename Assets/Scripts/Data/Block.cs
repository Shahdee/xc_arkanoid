using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

// model
// visual 

public class Block 
{
    BlockModel _model;

    public BlockModel model{
        get{return _model;}
        private set{_model = value;}
    }

    BlockVis _visual;

    public BlockVis visual{
        get{return _visual;}
        private set{_visual = value;}
    }

    public void Setup(BlockModel m){
        model = m;

        CreateVisual();
        SetParams();
    }

    void SetParams(){
        Color c = GameMan.instance.GetItemMan().GetColor(model.colorID);
        visual.SetParams(model.lives, c);
    }

    public void Reset(){
        ReturnVisual();
    } 

    public bool isAlive(){
        return model.lives > 0;
    }

    void CreateVisual(){
        GameObject gobject = GameMan.instance.GetEntityMan().GetEntity(model.assetName);
        gobject.SetActive(true);     
        gobject.transform.SetParent(GameMan.instance.GetLevelMan().trsParent);
        visual = gobject.GetComponent<BlockVis>();
    }

    public void Place(float x, float y){
        visual.PlaceTo(x, y);
    }

    void ReturnVisual(){
        if (visual != null){
            GameMan.instance.GetEntityMan().ReturnEntity(visual);
            visual = null;
        }   
    }


}
