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

        public enum BonusCharacteristic{
            Speed,
            Size,
        }

        public int id;
        public string name;
        public int itemID;
        public string assetName;
        public int characteristicID;
        public int time;
        public int speed;
        public float value;

        public BonusItem bonusItem;

        public BonusCharacteristic bonusCharacteristic;

        public void AssignBonus(){
            bonusItem = (BonusItem)itemID;
            bonusCharacteristic = (BonusCharacteristic)characteristicID;
        }
    }

    [System.Serializable]
    public class BlockModel{

        public int id;
        public int scores;
        public int lives;
        public int colorID;
        public int[] bonusProbability;
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
        public string assetName;
    }


    [System.Serializable]
    public class StickModel{
        public int speed;
        public string assetName;
    }


    [System.Serializable]
    public class PlayerModel{
        public int lives;
    }

    // TODO - update json from editor in the future

}




