using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

// stick
// balls 
// blocks 
// bonuses 

//scores - ? 

public class LevelMan : MonoBehaviour, IUpdatable, IFixUpdatable
{
    // public StickVis stickVis;
    // public BallVis ballVis; 

    Stick stick;
    Ball ballPrimary;
    List<Block> blocks = new List<Block>();
    List<Bonus> bonuses = new List<Bonus>();
    List<Ball> ballsAdditional = new List<Ball>();

    int currLevel = -1;

    void Awake()
    {

    }

    void PrepareInitialObjects(){

        if (stick == null && ballPrimary == null){
            stick = new Stick();
            ballPrimary = new Ball();

            stick.Setup(GameMan.instance.GetItemMan().stickModel);
            ballPrimary.Setup(GameMan.instance.GetItemMan().ballModel);


        }
    }

    public void StartLevel(){

        PrepareInitialObjects();
   
        EventMan.OnGameStart();

        MoveToNextLevel();
    }

    void MoveToNextLevel(){

        ResetObjects();

        PlaceInitialObjects();

        currLevel ++ ;
        PlaceBlocks(currLevel);

        // retrieve level data 
        // iteman
        // mapgenerator 
        // entityman 

    }

    void ResetObjects(){

        // return objects to buffer 
        // clear now but reuse links in the future

        ResetBalls();
        ResetBlocks();
        ResetBonuses();
    }

    void ResetBalls(){

    }

    void ResetBlocks(){

    }

    void ResetBonuses(){

    }

    void PlaceInitialObjects(){
        // stick in the center of the screen
        // primary ball is on top of the stick, resting 

        stick.PutToDefaultPlace();
        ballPrimary.PutOnStick(stick);
    }

    Vector3 posTmp = new Vector3();

    void PlaceBlocks(int level){
        int width = GameMan.instance.GetMapParser().GetLevelWidth(level);
        int height = GameMan.instance.GetMapParser().GetLevelHeight(level);

        Color color;
        BlockModel blockModel;
        GameObject obj; 

        for (int i=0; i<width; i++){
            for (int j=0; j<height; j++){

                color = GameMan.instance.GetMapParser().GetColor(level, i, j);
                blockModel = GameMan.instance.GetItemMan().GetBlock(color);

                // Debug.Log("we are here 1");

                if (blockModel != null){
                    
                    // 
                    // Debug.Log("we are here");

                    // tmp to test
                    obj= GameMan.instance.GetEntityMan().GetEntity(blockModel.assetName);
                    if (obj == null)
                        continue;

                    posTmp.x = i;
                    posTmp.y = j;
                    posTmp.z = 0;

                    obj.SetActive(true);
                    obj.transform.SetParent(transform);

                    obj.transform.position = posTmp;
                    // tmp to test eof 

                    (obj.GetComponent<BlockVis>()).sprite.color = color;
                }
            }
        }
    }

    void LevelComplete(){
        // if (currLevel == .Length -1)
        //     EventManager.OnGameEnded();
        // else 
            MoveToNextLevel();
    }


    public void UpdateMe(float delta){

    }

    public void FixUpdateMe(float delta){

        // upd level physics 
    }
}
