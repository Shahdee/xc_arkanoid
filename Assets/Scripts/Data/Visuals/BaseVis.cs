using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVis : ReusableObject
{

   Collider2D _colliderObj;

   public Collider2D colliderObj{
      get{
        if (_colliderObj == null)
            _colliderObj = GetComponent<Collider2D>();

         return _colliderObj;
      }
      set{
         _colliderObj = value;
      }
   }

   public SpriteRenderer sprite;

   Transform _transformObj;

   public Transform transformObj{
      get{
        if (_transformObj == null)
            _transformObj = transform;

         return _transformObj;
      }
      set{
         _transformObj = value;
      }
   }
}
