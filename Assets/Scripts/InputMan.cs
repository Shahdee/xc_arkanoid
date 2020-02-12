using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// handles screen touches  

public class InputMan : MonoBehaviour, IUpdatable
{

    public float horizontal = 0;
    public float vertical = 0;

    public float GetHorizontal(){
        return horizontal;
    }

    public float GetVertical(){
        return vertical;
    }

    public void UpdateMe(float deltaTime)
    {
        UpdateAxes();
    }

    void UpdateAxes(){
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
}
