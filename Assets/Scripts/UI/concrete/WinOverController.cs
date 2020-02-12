using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOverController : WinControllerBase
{
    public WinOverController(WinViewBase view) : base(view){
       
    }

    public void SendRestart(){
        GameMan.instance.StartGame();
    }
}
