using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenuController : WinControllerBase
{
    public WinMenuController(WinViewBase view) : base(view){
       
    }

    public void SendPlay(){
        GameMan.instance.StartGame();
    }
}
