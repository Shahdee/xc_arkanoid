using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

// stick
// balls 
// blocks 
// bonuses 
//scores 

public class LevelMan : MonoBehaviour, IUpdatable, IFixUpdatable
{
    public Transform trsParent; 
    public Surface[] surfaces;

    Stick stickPrimary;

    public Stick GetStick(){
        return stickPrimary;
    }

    Ball ballPrimary;

    public Ball GetBall(){
        return ballPrimary;
    }

    List<Block> blocks = new List<Block>();
    List<Bonus> bonuses = new List<Bonus>();
    List<Ball> ballsAdditional = new List<Ball>();
    BonusHandler bonusHandler;

    List<AppliedBonus> appliedBonuses = new List<AppliedBonus>();

    static LevelMan _instance;
    public static LevelMan instance{
        get{return _instance;}
        private set{_instance = value;}
    }

    int _currLevel = -1;
    
    public int currLevel{
        get{return _currLevel;}
        set{
            _currLevel = value;
            EventMan.OnLevelChange(_currLevel);
        }
    }

    int killedBlocks = 0;
    int blocksToKill = 0;

    void Awake()
    {
        instance = this;

        bonusHandler = new BonusHandler(this);

        trsParent = transform;

        SubscribeToEvents();

        CalcWorldBounds();
    }

    void SubscribeToEvents(){

        EventMan.AddBallDieListener(BallDied);
        EventMan.AddBlockHitListener(BlockHit);
        // EventMan.AddBlockDieListener(BlockDie);

        EventMan.AddBonusDieListener(BonusDie);
        EventMan.AddBonusCatchListener(BonusCatch);
    }
    

    Vector3 _worldBounds;
    public Vector3 worldBounds{
        get{return _worldBounds;}
        private set {_worldBounds = value;}
    }

    void CalcWorldBounds(){
        Vector3 vec = new Vector3();
        vec.x = Screen.width;
        vec.y = Screen.height;
        vec.z = Camera.main.transform.position.z;

        worldBounds = Camera.main.ScreenToWorldPoint(vec);

        // Debug.Log("worldBounds " + worldBounds);
    }

    void PrepareInitialObjects(){
        if (stickPrimary == null && ballPrimary == null){
            CreateStickAndBall();
            SetupSurfaces();
        }
    }

    void CreateStickAndBall(){
        stickPrimary = new Stick();
        ballPrimary = new Ball();

        stickPrimary.Setup(GameMan.instance.GetItemMan().stickModel);
        ballPrimary.Setup(GameMan.instance.GetItemMan().ballModel);
    }

    void SetupSurfaces(){
        for (int i=0; i<surfaces.Length; i++)
            surfaces[i].Setup();
    }

    public void StartLevel(){

        PrepareInitialObjects();

        currLevel = -1;
   
        EventMan.OnGameStart();

        MoveToNextLevel();
    }

    // for tests 
    public void StartNextLevel(){
        LevelComplete();
    }

    // when we still have lives 
    void ContinueLevel(){
        // TODO - remove bonuses and additional balls 

        ResetBonuses();
        ResetBalls();
        ResetAppliedBonuses();

        // TODO clear bonuses from current objects

        PlaceInitialObjects();
    }

    void MoveToNextLevel(){

        ResetObjects();

        PlaceInitialObjects();

        killedBlocks = 0;
        blocksToKill = 0;
        currLevel ++ ;

        PlaceBlocks(currLevel);
    }

    void ResetObjects(){
        ResetBalls();
        ResetBlocks();
        ResetBonuses();
        ResetAppliedBonuses();
    }

    void ResetBalls(){
        for (int i=0;i<ballsAdditional.Count; i++){
            ballsAdditional[i].Reset();
        }
        ballsAdditional.Clear(); // TODO reuse instead of clear in the future
    }

    void ResetAppliedBonuses(){
        bonusHandler.Reset();
    }

    void ResetBlocks(){
        for (int i=0;i<blocks.Count; i++){
            blocks[i].Reset();
        }
        blocks.Clear(); // TODO reuse instead of clear in the future
    }

    void ResetBonuses(){
        for (int i=0;i<bonuses.Count; i++){
            bonuses[i].Reset();
        }
        bonuses.Clear(); // TODO reuse instead of clear in the future
    }

    void PlaceInitialObjects(){
        stickPrimary.PlaceToCenter();
        ballPrimary.PutOnStick(stickPrimary);
    }

    static float tolerance = 0.5f;

    void PlaceBlocks(int level){
        int width = GameMan.instance.GetMapParser().GetLevelWidth(level);
        int height = GameMan.instance.GetMapParser().GetLevelHeight(level);

        // Debug.Log("width " + width);
        // Debug.Log("height " + height);

        Color color;
        BlockModel blockModel;
        float x,y;

        Block block = null;

        float disp = (worldBounds.x*2 - width)/2; 
        
        for (int i=0; i<width; i++){
            for (int j=0; j<height; j++){

                color = GameMan.instance.GetMapParser().GetColor(level, i, j);
                blockModel = GameMan.instance.GetItemMan().GetBlock(color);

                if (blockModel != null){

                    block = new Block();                  
                    blocks.Add(block);

                    x = - worldBounds.x + disp + i + tolerance;
                    y = worldBounds.y - height  + j + tolerance;

                    block.Setup(blockModel);
                    block.Place(x, y);

                    if (block.isBreakable())
                        blocksToKill++;
                }
            }
        }
    }

    void LevelComplete(){
        if (GameMan.instance.GetMapParser().isLastLevel(currLevel))
            EventMan.OnGameEnded();
        else 
            MoveToNextLevel();
    }

    void BallDied(BallVis vis){
        var player = GameMan.instance.GetPlayer();
        player.ReduceLives();

        if (player.isAlive())
            ContinueLevel();
        else
            EventMan.OnGameEnded();
    }

    void BlockHit(Collider2D collider){
        var block = GetBlock(collider);
        if (block != null){

            if (block.isBreakable() && ! block.isAlive()){
                TryDispatchBonus(block);
                block.Reset();

                killedBlocks++;

                if (killedBlocks == blocksToKill) 
                    LevelComplete();

            }
        }
    }

    // void BlockDie(BlockVis vis){
    //     var block = GetBlock(vis);
    //     if (block != null){
    //         TryDispatchBonus(block);
    //         block.Reset();
    //     }

    //     killedBlocks++;

    //     if (killedBlocks == blocksToKill) 
    //         LevelComplete();
    // }

    void BonusDie(BonusVis vis){
        // Debug.Log("bonus died");

        var bonus = GetBonus(vis);
        if (bonus != null){
            bonus.Reset();
        }
    }

    void BonusCatch(BonusVis vis){
        // Debug.Log("bonus catch !");

        var bonus = GetBonus(vis);
        if (bonus != null){
            bonusHandler.AddBonus(bonus.model);
            bonus.Reset();
        }
    }

    void TryDispatchBonus(Block block){

        int max = 100;
        int min;

        int rand = Random.Range(0, max);       

        for (int i=0; i<block.model.bonusProbability.Length; i++){

            if (block.model.bonusProbability[i] > 0){
                min = max - block.model.bonusProbability[i];
                max-=1;

                Debug.Log("try dispatch bonus min " + min + " max " + max + " ran " + rand);

                if (rand >= min && rand <= max){

                    var model = GameMan.instance.GetItemMan().GetBonus(i);
                    if (model != null){
                        var bonus = new Bonus();
                        bonuses.Add(bonus);
                        bonus.Setup(model);
                        bonus.Place(block.visual.transformObj.position);
                    }
                    break;
                }
                else{
                    max = min; 
                }
            }
        }
    }

    Block GetBlock(Collider2D collider){
        for (int i=0; i<blocks.Count; i++){
            if (blocks[i].visual != null && blocks[i].visual.colliderObj == collider)
                return blocks[i];
        }
        return null;
    }


    Block GetBlock(BlockVis vis){
        for (int i=0; i<blocks.Count; i++)
            if (blocks[i].visual == vis)
                return blocks[i];

        return null;
    }

    Bonus GetBonus(BonusVis vis){
        for (int i=0; i<bonuses.Count; i++)
            if (bonuses[i].visual == vis)
                return bonuses[i];

        return null;
    }

    public void UpdatePhysics(float delta){

        ballPrimary.UpdatePhysics(delta);

        for (int i=0; i<ballsAdditional.Count; i++){
            ballsAdditional[i].UpdatePhysics(delta);
        }

        for (int i=0; i<bonuses.Count; i++){
            bonuses[i].UpdatePhysics(delta);
        }

        stickPrimary.UpdatePhysics(delta);
    }

    public void UpdateMe(float delta){
        stickPrimary.UpdateMe(delta);
        ballPrimary.UpdateMe(delta);
        bonusHandler.UpdateMe(delta);
    }
}
