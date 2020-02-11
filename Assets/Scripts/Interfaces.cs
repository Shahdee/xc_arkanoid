using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 // bonus
public interface IBonusable{
    // TODO 
}

public interface IUpdatable{
    void UpdateMe(float deltaTime);
}

// physics 
public interface IFixUpdatable{
    void FixUpdateMe(float deltaTime);
}
