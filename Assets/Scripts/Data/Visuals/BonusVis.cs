using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVis : BaseVis
{

    Vector3 posTmp = new Vector3();

    public void PlaceTo(Vector3 position){
        transformObj.position = position;
    }
    Vector3 moveDir = new Vector3(0, -1);

    float currSpeed;

    public void SetParams(float speed){
        currSpeed = speed;
    }

    public override void UpdatePhysics(float delta){
        UpdateMove(delta);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ReusableObject.tgFloor)
        {
            EventMan.OnBonusDie(this);
            return;
        }

        if (col.tag == ReusableObject.tgStick)
        {
            EventMan.OnBonusCatch(this);
            return;
        }
    }

    void UpdateMove(float deltaTime){

        posTmp = transformObj.position;
        posTmp.x += (moveDir.x * currSpeed * deltaTime);
        posTmp.y += (moveDir.y * currSpeed * deltaTime);

        transformObj.position = posTmp;
    }
    
}
