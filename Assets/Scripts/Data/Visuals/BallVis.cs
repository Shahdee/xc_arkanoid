using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVis : BaseVis
{
    public Rigidbody2D rigidbodyObj;

    float currSpeed;
    bool resting;


    void SetParamsToDefault(){
        resting = true;
        rigidbodyObj.simulated = false;

        moveDir.x = 0;
        moveDir.y = 0;
    }

    public void SetParams(float speed){
        currSpeed = speed;
    }

    public void RestOnObject(BaseVis vis){
        SetParamsToDefault();
        PutOnObject(vis);
    }

    public void PutOnObject(BaseVis vis){
        posTmp.x = vis.transformObj.position.x;
        posTmp.y = vis.transformObj.position.y + vis.colliderObj.bounds.extents.y + sprite.bounds.size.y/2;  // sprite bounds instead of collider, cos collider is turned off => bounds = 0
        posTmp.z = transformObj.position.z;
        transformObj.position = posTmp;

        // Debug.Log("vis.colliderObj.bounds.extents " + vis.colliderObj.bounds.extents.y);
        // Debug.Log("colliderObj.bounds.extents.y " + colliderObj.bounds.extents.y);
        // Debug.Log("sprite.size.y " + sprite.bounds.size.y);
    }

    public override void UpdatePhysics(float delta){

        hitBlock = false;

        CheckResting();

        UpdateMove(delta);
    }

    bool hitBlock = false;
    Vector3 moveDir = new Vector3();
    float moveDeltaX = 0;
    Vector3 posTmp;
    Vector3 vecTmp;

    void UpdateMove(float deltaTime){

        if (resting) return;

        posTmp = transformObj.position;
        posTmp.x += (moveDir.x * currSpeed * deltaTime);
        posTmp.y += (moveDir.y * currSpeed * deltaTime);

        transformObj.position = posTmp;
    }

    void CheckResting(){
        if (resting){
              moveDeltaX = GameMan.instance.GetInputMan().GetHorizontal();
              if (moveDeltaX > 0 || moveDeltaX < 0){                  
                  resting = false;
                  rigidbodyObj.simulated = true;
                  ApplyInitialImpulse(moveDeltaX);
              }
        }
    }

    void ApplyInitialImpulse(float direction){
        moveDir.x = (-1) * Mathf.Sign(direction);
        moveDir.y = 1;
    }

   public override void ClearForBuffer(){
      SetParamsToDefault();
   }


    // Collisions 
    // Should I make one step back upon collision? 

    void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log(col.name);

         // on floor 
        if (col.tag == ReusableObject.tgFloor)
        {
            EventMan.OnBallDie(this);
            return;
        }

        // platform 
        if (col.tag == ReusableObject.tgStick){
            moveDir.y *= -1;
            return;
        }

        if (col.tag == ReusableObject.tgWall){
            Bounce(col);
            return;
        }

        //block
        if (col.tag == ReusableObject.tgBlock){

            if (! hitBlock){
                hitBlock = true; // to allow only 1 block hit per frame 
                Bounce(col);
            }
                
            EventMan.OnBlockHit(col);
            return;
        }       
    }

    Vector3 vecTmp2;
    float value;
    float deltaX, deltaY;

    void Bounce(Collider2D coll){

        // Debug.Log("last dir " + moveDir.ToString());

        // if (coll.tag == ReusableObject.tgBlock){

            // Debug.Log("coll.bounds.size " + coll.bounds.size);
            // Debug.Log("coll.bounds.extents " + coll.bounds.extents);
            // Debug.Log("coll.bounds.min " + coll.bounds.min);
            // Debug.Log("coll.bounds.max " + coll.bounds.max);
            // Debug.Log("minX " + minX + " maxX " + maxX + " minY " + minY + " maxY " + maxY);

            // Debug.Log("transformObj.position.x " + transformObj.position.x + " / transformObj.position.y " + transformObj.position.y);
        // }

        // top or bottom 
        if (transformObj.position.x >= coll.bounds.min.x && transformObj.position.x <= coll.bounds.max.x){
            moveDir.y *= -1;
            // Debug.Log("new dir " + moveDir.ToString());
            return;
        }

        // left or right 
        if (transformObj.position.y >= coll.bounds.min.y && transformObj.position.y <= coll.bounds.max.y){
            moveDir.x *= -1;
            // Debug.Log("new dir " + moveDir.ToString());
            return;
        }

        deltaX = Mathf.Abs(transformObj.position.x - coll.transform.position.x) - coll.bounds.size.x;
        deltaY = Mathf.Abs(transformObj.position.y - coll.transform.position.y) - coll.bounds.size.y;

        if (deltaX > deltaY){
            moveDir.x *= -1;
            // Debug.Log("new dir " + moveDir.ToString());
            return;
        }
        else{
            moveDir.y *= -1;
            // Debug.Log("new dir " + moveDir.ToString());
            return;
        }
    }
}
