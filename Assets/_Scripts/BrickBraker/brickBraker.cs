using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickBraker : MonoBehaviour
{
    static List<Transform> _objects;

    public static void Brake(Transform _objToBrake)
    {
        Transform newObject = _objToBrake;
        if(_objects == null)
        {
            _objects = new List<Transform>();
        }

        _objects.Add(newObject);
    }

    public static void Restore(Vector3 pos)
    {
        for(int i = 0; i < _objects.Count; i++)
        {
            if (_objects[i].position == pos)
            {
                Instantiate(_objects[i]);
            }
        }
    }
}
