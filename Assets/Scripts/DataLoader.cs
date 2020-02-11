using UnityEngine;
using System.IO;

public class DataLoader 
{
    GameData gameData;

    const string dataFileName = "data.json";

    public DataLoader(){
        LoadGameData();
    }

    public GameData GetGameData(){
        return gameData;
    }

    void LoadGameData(){
        string filePath;
        string dataAsJson;
        
        filePath = Path.Combine(Application.streamingAssetsPath, dataFileName); 

        if (File.Exists(filePath)){
            dataAsJson = File.ReadAllText(filePath);

            Debug.Log(dataAsJson);
                    
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);                
        }
        else{
            Debug.LogError("no game data file");
        }
    }
}
