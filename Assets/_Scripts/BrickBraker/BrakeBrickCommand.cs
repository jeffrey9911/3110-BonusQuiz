using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeBrickCommand : iCommand
{
    Vector3 _position;
    Transform _transform;

    public BrakeBrickCommand(Vector3 pos, Transform objTrans)
    {
        this._transform = objTrans;
        this._position = pos;
    }

    public void Execute()
    {
        brickBraker.Brake(_transform);
    }

    public void Undo()
    {
        brickBraker.Restore(_position);
    }
}
