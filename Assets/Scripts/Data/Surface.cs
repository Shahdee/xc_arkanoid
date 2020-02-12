using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    // collides with ball 
    // represents walls floor and ceiling 
    // stretches to fit the world boundaries 

    public SpriteRenderer rendererObj;

     public enum SurfaceType{
        Floor,
        WallLeft,
        WallRight,
        Ceiling
    }

    public SurfaceType surfaceType;
    static Vector3 vecTmp;

    void Awake(){
        rendererObj = GetComponent<SpriteRenderer>();
    }

    public void Setup(){
        Stretch();
    }

    void Stretch(){

        switch(surfaceType){
            case SurfaceType.WallLeft: // stretch vertically
            case SurfaceType.WallRight: 

                // scale 
                vecTmp = transform.localScale;
                vecTmp.y = LevelMan.instance.worldBounds.y*2;
                transform.localScale = vecTmp;

                //position
                vecTmp = transform.position;
                vecTmp.y = 0;

                if (surfaceType == SurfaceType.WallLeft)                                        
                    vecTmp.x = -LevelMan.instance.worldBounds.x - rendererObj.bounds.size.x/2;
                else
                    vecTmp.x = LevelMan.instance.worldBounds.x + rendererObj.bounds.size.x/2;                
                transform.position = vecTmp;
                
            break;

            case SurfaceType.Floor: // stretch horizontally
            
                // scale 
                vecTmp = transform.localScale;
                vecTmp.x = LevelMan.instance.worldBounds.x*2;
                transform.localScale = vecTmp;

                //position 
                vecTmp = transform.position;
                vecTmp.x = 0;
                vecTmp.y = -LevelMan.instance.worldBounds.y - rendererObj.bounds.size.y/2;
                transform.position = vecTmp;

            break;

            case SurfaceType.Ceiling: // stretch horizontally
            
                // scale 
                vecTmp = transform.localScale;
                vecTmp.x = LevelMan.instance.worldBounds.x*2;
                transform.localScale = vecTmp;

                //position 
                vecTmp = transform.position;
                vecTmp.x = 0;
                vecTmp.y = LevelMan.instance.worldBounds.y + rendererObj.bounds.size.y/2;
                transform.position = vecTmp;

            break;
        }
    }
}
