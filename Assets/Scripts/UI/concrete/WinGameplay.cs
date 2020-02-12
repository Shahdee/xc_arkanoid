using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGameplay : WinViewBase
{
    public Text lives; 
    public Text level;

    protected override void InInit(){
        EventMan.AddPlayerLivesChangeListener(SetLives);
        EventMan.AddLevelChnageCallback(SetLevel);
    }

    protected override WinControllerBase CreateController(){
        return new WinGameplayController(this);;
    }


    void SetLives(int lvs){
        lives.text = "lives " + lvs.ToString();
    }

    protected override void OnShow(){
        int lives = GameMan.instance.GetPlayer().currLives;
        SetLives(lives);

        int level = LevelMan.instance.currLevel;
        SetLevel(level);
    }

      void SetLevel(int lvl){
        level.text = "level " + (lvl + 1).ToString();
    }
}
