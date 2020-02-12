using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

public class ItemMan 
{
    BonusModel[] allBonuses;
    BlockModel[] allBlocks;
    ColorModel[] allColors;

    BallModel _ballModel;
    public BallModel ballModel{
        get{return _ballModel;}
        private set{_ballModel = value;}
    }

    StickModel _stickModel;
    public StickModel stickModel{
        get{return _stickModel;}
        private set{_stickModel = value;}
    }

    PlayerModel _playerModel;
    public PlayerModel playerModel{
        get{return _playerModel;}
        private set{_playerModel = value;}
    }

    public void PrepareItems(GameData data){

        // stick 
        // ball
        // levels
        // bonuses 

        PrepareBlocks(data);
        PrepareBonuses(data);
        PrepareColors(data);
        PrepareCommon(data);

    }

    void PrepareBlocks(GameData data){
        allBlocks = data.blocks;
    }

    void PrepareBonuses(GameData data){
        allBonuses = data.bonuses;

        for (int i=0; i<allBonuses.Length; i++){
            allBonuses[i].AssignBonus();
        }
    }

    void PrepareColors(GameData data){
        allColors = data.colors;

        for (int i=0; i<allColors.Length; i++){
            allColors[i].AssignColor();
        }
    }

    void PrepareCommon(GameData data){
        ballModel = data.ball;
        stickModel = data.stick;
        playerModel = data.player;
    }

    public BlockModel GetBlock(Color c){
        var colorModel = GetColorModel(c);
        if (colorModel != null){
            var blockModel = GetBlock(colorModel.id);
            return blockModel;
        }
        return null;
    }

    BlockModel GetBlock(int colorID){
        for (int i=0; i<allBlocks.Length; i++){
            if (allBlocks[i].colorID == colorID)
                return allBlocks[i];
        }
        return null;
    }

    public BonusModel GetBonus(int id){
        for (int i=0; i<allBonuses.Length; i++){
            if (allBonuses[i].id == id)
                return allBonuses[i];
        }
        return null;
    }


    public Color GetColor(int id){
        for (int i=0; i<allColors.Length; i++){
            if (allColors[i].id == id)
                return allColors[i].color;
        }
        return Color.gray;
    }

    ColorModel GetColorModel(Color c){

        // Debug.Log("c " + c);

        for (int i=0; i<allColors.Length; i++){

            // Debug.Log("allColors " + allColors[i].color);

            if (allColors[i].color.Equals(c))
                return allColors[i];
        }
        // Debug.Log(":(");
        return null;
    }

}
