using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models{

    [System.Serializable]
    public class BonusModel{

        public enum BonusItem{
            Match,
            Stick,
            Ball
        }

        public int id;
        public bool positive;
        public string name;
        public int itemID;
        public int time;

        public BonusItem bonusItem;

        public void AssignBonusItem(){
            bonusItem = (BonusItem)itemID;
        }
    }

    [System.Serializable]
    public class BlockModel{

        public int id;
        public int scores;
        public int lives;
        public int colorID;
        public float bonusProbability;
        public string assetName;
    }

    [System.Serializable]
    public class ColorModel{

        public int id;
        public float[] value;

        public Color color;

        public void AssignColor(){
            if (value.Length == 4)
                color = new Color(value[0], value[1], value[2], value[3]);
            else
                Debug.LogError("Color has to have 4 components");
        }
    }

   
    [System.Serializable]
    public class BallModel{
        public int speed;
    }


    [System.Serializable]
    public class StickModel{
        public int speed;
    }


    [System.Serializable]
    public class PlayerModel{
        public int lives;
    }

    [System.Serializable]
    public class LevelModel{
        public int id;
        public int[] bonuses;
    }

    // TODO - optional - update json from editor 

}




