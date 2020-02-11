using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVis : ReusableObject
{
   public Collider2D collider;

   public SpriteRenderer sprite;


   public override void ClearForBuffer(){

      // turn off collider 
   }
}
