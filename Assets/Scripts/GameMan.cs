using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMan : MonoBehaviour
{
    public GUIMan guiMan;

    public EntityMan entityMan;
    
    ItemMan itemMan;

    public LevelMan levelMan;
    DataLoader dataLoader;
    Player player;

    public InputMan inputMan;
    public MapParser mapParser;

    static GameMan _instance;
    public static GameMan instance{
        get{return _instance;}
        private set{_instance = value;}
    }

    public EntityMan GetEntityMan(){
        return entityMan;
    }

    public LevelMan GetLevelMan(){
        return levelMan;
    }

    public GUIMan GetGUIMan(){
        return guiMan;
    }

    public ItemMan GetItemMan(){
        return itemMan;
    }

    public Player GetPlayer(){
        return player;
    }

    public MapParser GetMapParser(){
        return mapParser;
    }

    public InputMan GetInputMan(){
        return inputMan;
    }

    public enum GameStates{
        Menu,
        Play,
        Over,
    }

    GameStates currGameState = GameStates.Menu;

    void SetGameState(GameStates state){

        if (currGameState != state){
            currGameState = state;

            EventMan.OnGameStateChange(currGameState);
        }
    }

    void Start()
    {
        instance = this;

        itemMan = new ItemMan();
        dataLoader = new DataLoader();
        player = new Player();

        EventMan.AddGameStartListener(GameStarted);
        EventMan.AddGameEndedListener(GameEnded);

        onDataLoaded();

        guiMan.Init();
    }

    void onDataLoaded(){
        var gameData = dataLoader.GetGameData();
        itemMan.PrepareItems(gameData);

        player.Setup(gameData.player);
    }

    public void StartGame(){
        player.Restore();
        levelMan.StartLevel();
    }

    public void StartNextLevel(){
        levelMan.StartNextLevel();
    }

#region Level events 
    void GameStarted(){

        SetGameState(GameStates.Play);
    }

    void GameEnded(){
        SetGameState(GameStates.Over);
    }
#endregion

    void FixedUpdate(){
        if (currGameState == GameStates.Play)
        {
            levelMan.UpdatePhysics(Time.fixedDeltaTime);
        }
    }
    
    void Update()
    {
        UpdateStates();
    }

    void UpdateStates(){
        switch(currGameState){
            case GameStates.Menu:
                UpdateMenu(Time.deltaTime);
            break;

            case GameStates.Play:
                UpdatePlay(Time.deltaTime);
            break;

            case GameStates.Over:
                UpdateOver(Time.deltaTime);
            break;
        }
    }

    void UpdateMenu(float deltaTime){

        guiMan.UpdateMe(deltaTime);
    }

    void UpdatePlay(float deltaTime){
        inputMan.UpdateMe(deltaTime);        
        levelMan.UpdateMe(deltaTime);       
        guiMan.UpdateMe(deltaTime);
    }

    void UpdateOver(float deltaTime){

        guiMan.UpdateMe(deltaTime);
    }
}
