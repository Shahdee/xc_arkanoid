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
        CheckResting();

        UpdateMove(delta);
    }

    public override void UpdateMe(float delta){

      
    }

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
        // Debug.Log(" enter " + col.name);

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

        // block or wall
        if (col.tag == ReusableObject.tgBlock || col.tag == ReusableObject.tgWall){
            Bounce(col);
            return;
        }       
    }

    Vector3 vecTmp2;
    float value;

    void Bounce(Collider2D collider){
        posTmp.x = transformObj.position.x - collider.transform.position.x;
        posTmp.y = transformObj.position.y - collider.transform.position.y;

        vecTmp.x = Mathf.Abs(posTmp.x) - collider.bounds.size.x;
        vecTmp.y = Mathf.Abs(posTmp.y) - collider.bounds.size.y;

        if (vecTmp.y > vecTmp.x){
            // Debug.Log("x");
            moveDir.y *= -1;
        }
        else{
            moveDir.x *= -1;
            // Debug.Log("y");                
        }

        posTmp.Normalize();

        value = Mathf.Abs(Mathf.Max(vecTmp.x, vecTmp.y));

        vecTmp2 = transformObj.position;

        vecTmp2.x += posTmp.x * value;
        vecTmp2.y += posTmp.y * value;
        
        transformObj.position = vecTmp2;
    }

    // void OnTriggerExit2D(Collider2D col)
    // {
    //     Debug.Log(" exit " + col.name);
    // }

    //  void OnTriggerStay2D(Collider2D col)
    // {
    //     Debug.Log(" stay " + col.name);
    // }


}
