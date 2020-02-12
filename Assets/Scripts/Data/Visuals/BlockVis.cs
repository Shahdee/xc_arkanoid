using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockVis : BaseVis
{

    Vector3 posTmp = new Vector3();

    public void PlaceTo(float x, float y){
        posTmp.x = x;
        posTmp.y = y;
        posTmp.z = 0;
        transformObj.position = posTmp;
    }

    int currLives;

    public void SetParams(int lives, Color c){
        currLives = lives;
        SetColor(c);
    }

    void SetColor(Color c){
        sprite.color = c;
    }

    void ReduceLives(){
        // to ignore immortal block
        if (currLives > 0){
            currLives--;
            if (currLives == 0){
                EventMan.OnBlockDie(this);
            }  
        }    
    }
    
    public override void UpdatePhysics(float delta){

    }

    public override void UpdateMe(float delta){
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ReusableObject.tgBall)
        {
            ReduceLives();

        }
    }
}
