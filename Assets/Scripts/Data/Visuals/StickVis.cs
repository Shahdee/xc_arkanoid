using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickVis : BaseVis
{

    float currSpeed;

    public void SetParams(float speed){
        currSpeed = speed;
    }

    Vector3 posTmp;

    public void PlaceToCenter(){
        posTmp = transformObj.position;
        posTmp.x = 0;
        posTmp.y = -LevelMan.instance.worldBounds.y + colliderObj.bounds.extents.y;
        transformObj.position = posTmp;
    }
   
    public override void UpdatePhysics(float delta){
        UpdateMove(delta);
    }

    public override void UpdateMe(float delta){
       
    }

    float moveDeltaX = 0;
    void UpdateMove(float deltaTime){
        moveDeltaX = GameMan.instance.GetInputMan().GetHorizontal();

        if (moveDeltaX > 0 || moveDeltaX < 0){
            posTmp = transformObj.position;
            posTmp.x += (moveDeltaX * currSpeed * deltaTime);
            posTmp.x = Mathf.Clamp(posTmp.x, -LevelMan.instance.worldBounds.x + colliderObj.bounds.extents.x, LevelMan.instance.worldBounds.x - colliderObj.bounds.extents.x);
            transformObj.position = posTmp;
        }
    }
}
