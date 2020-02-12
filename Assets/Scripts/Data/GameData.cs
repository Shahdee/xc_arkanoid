using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

[System.Serializable]    
public class GameData 
{
    public BonusModel[] bonuses;

    public BlockModel[] blocks;

    public ColorModel[] colors;

    public BallModel ball;
    public StickModel stick;
    public PlayerModel player;

}
