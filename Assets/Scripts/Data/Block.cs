using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Models;

// model
// visual 

public class Block 
{
    BlockModel _model;

    public BlockModel model{
        get{return _model;}
        private set{_model = value;}
    }

    BlockVis _visual;

    public BlockVis visual{
        get{return _visual;}
        private set{_visual = value;}
    }

    public void Setup(BlockModel m){
        model = m;
    }
}
