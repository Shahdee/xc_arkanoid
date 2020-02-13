using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

public class Player 
{

    PlayerModel _model;

    public PlayerModel model{
        get{return _model;}
        private set{_model = value;}
    }

    int _currLives;
    public int currLives{
        get{return _currLives;}
        set{_currLives = value;}
    }

   public void Setup(PlayerModel m){
       model = m;

       SetParams();
   }

   public void Restore(){
       SetParams();
   }

   void SetParams(){
       currLives = model.lives;
   }

   public void ReduceLives(){
       currLives--;

    //    Debug.Log("lives left " + currLives);

       EventMan.OnPlayerLivesChange(currLives);
   }

   public bool isAlive(){
       return currLives > 0;
   }

}
