using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : WinViewBase
{
    public Button btnPlay;

    protected override WinControllerBase CreateController(){
        return new WinMenuController(this);
    }

    protected override void InInit(){
        btnPlay.onClick.AddListener(PlayClick);
    }

    void PlayClick(){
        (m_Controller as WinMenuController).SendPlay();
    }
}
