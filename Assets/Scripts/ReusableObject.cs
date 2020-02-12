using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReusableObject : MonoBehaviour, IUpdatable, IFixUpdatable
{
    // list of tags 

    public static string tgBall = "ball";
    public static string tgBonus = "bonus";
    public static string tgStick = "stick";
    public static string tgWall = "wall";
    public static string tgFloor = "floor";
    public static string tgBlock = "block";

    public virtual void ClearForBuffer(){}

    public virtual void UpdatePhysics(float delta){}

    public virtual void UpdateMe(float delta){}
}
