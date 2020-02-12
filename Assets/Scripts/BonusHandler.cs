using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

// handles all curr applied bonuses for level man

public class BonusHandler: IUpdatable
{
    List<AppliedBonus> appliedBonuses = new List<AppliedBonus>();

    LevelMan levelMan;

    public BonusHandler(LevelMan lvlman){
        levelMan = lvlman;
    }


    public void AddBonus(BonusModel model){

        var bonus = GetBonus(model.bonusItem, model.bonusCharacteristic);
        if (bonus == null){
            bonus = new AppliedBonus();
            bonus.AddTimeElapseListener(BonusTimeElapsed);
            appliedBonuses.Add(bonus);

        }

        bonus.Activate(model);

        Debug.Log("apply bonus " + bonus.model.bonusItem + " / " + bonus.model.bonusCharacteristic);

        switch(model.bonusItem){
            case BonusModel.BonusItem.Stick:
                levelMan.GetStick().ApplyBonus(model);
            break;

            case BonusModel.BonusItem.Ball:
                levelMan.GetBall().ApplyBonus(model);
            break;

            case BonusModel.BonusItem.Match:
                // TODO
            break;
        }
    }

    void BonusTimeElapsed(AppliedBonus bonus){
       RemoveBonus(bonus);
    }

    void RemoveBonus(AppliedBonus bonus){

        Debug.Log("remove bonus " + bonus.model.bonusItem + " / " + bonus.model.bonusCharacteristic);

         switch(bonus.model.bonusItem){
            case BonusModel.BonusItem.Stick:
                levelMan.GetStick().RemoveBonus(bonus.model);
            break;

            case BonusModel.BonusItem.Ball:
                levelMan.GetBall().RemoveBonus(bonus.model);
            break;

            case BonusModel.BonusItem.Match:
                // TODO
            break;
        }
    }

    AppliedBonus GetBonus(BonusModel.BonusItem item, BonusModel.BonusCharacteristic characteristic){
        for (int i=0; i<appliedBonuses.Count; i++){
            if (appliedBonuses[i].model.bonusItem == item && appliedBonuses[i].model.bonusCharacteristic == characteristic)
                return appliedBonuses[i];
        }
        return null;
    }

    public void Reset(){
        for (int i=0; i<appliedBonuses.Count; i++){
            RemoveBonus(appliedBonuses[i]);
        }
        appliedBonuses.Clear();
    }

    public void UpdateMe(float delta){
        for (int i=0; i<appliedBonuses.Count; i++){
            appliedBonuses[i].UpdateMe(delta);
        }
    }
}
