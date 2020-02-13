using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGameplay : WinViewBase
{
    public Text lives; 
    public Text level;

    public Button btnNextLevel;

    protected override void InInit(){
        EventMan.AddPlayerLivesChangeListener(SetLives);
        EventMan.AddLevelChnageCallback(SetLevel);

        btnNextLevel.onClick.AddListener(NextClick);
    }

    protected override WinControllerBase CreateController(){
        return new WinGameplayController(this);;
    }

    void NextClick(){
        (m_Controller as WinGameplayController).SendNext();
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
