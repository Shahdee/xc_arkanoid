using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameplayController : WinControllerBase
{
    public WinGameplayController(WinViewBase view) : base(view){
       
    }

    public void SendNext(){
        GameMan.instance.StartNextLevel();
    }

}
