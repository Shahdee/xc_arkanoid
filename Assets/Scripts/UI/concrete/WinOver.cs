using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 
public class WinOver : WinViewBase
{
    public Text level;
    public Button btnRestart;

    protected override void InInit(){
        btnRestart.onClick.AddListener(RestartClick);
    }

    protected override WinControllerBase CreateController(){
        return new WinOverController(this);
    }

    protected override void OnShow(){
        int level = LevelMan.instance.currLevel;
        
        SetLevel(level);
    }

    void SetLevel(int lvl){
        level.text = "level " + (lvl + 1).ToString();
    }

    void RestartClick(){
        (m_Controller as WinOverController).SendRestart();
    }
}
