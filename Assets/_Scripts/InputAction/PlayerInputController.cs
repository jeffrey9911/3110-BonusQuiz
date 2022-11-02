using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance;

    public PlayerAction _playerAction;

    private void OnEnable()
    {
        _playerAction.Enable();
    }

    private void OnDisable()
    {
        _playerAction.Disable();
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        _playerAction = new PlayerAction();
    }
}
