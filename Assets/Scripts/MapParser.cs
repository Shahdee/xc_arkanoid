using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from texture pixels to block types and placement 

public class MapParser : MonoBehaviour
{
    public Texture2D[] maps;
    // 
    // textures []

    // list of blocks and their position 
        // x,y, type/id

    public int GetLevelWidth(int level){
        if (level < maps.Length)
            return maps[level].width;

        return 0;
    }

    public int GetLevelHeight(int level){
        if (level < maps.Length)
            return maps[level].height;

        return 0;
    }

    public Color GetColor(int lvl, int x, int y){
        if (lvl < maps.Length)
            return maps[lvl].GetPixel (x, y);
        return Color.white;
    }

    public bool HasLevel(int level){
        return (level < maps.Length);
    }

    public bool isLastLevel(int lvl){
        return (lvl == maps.Length-1);
    }

    // QA 
    // color -> block id + 
    // x,y -> position on scene 
    // what if blocks are rectangular? - I assume blocks are square
    
}
