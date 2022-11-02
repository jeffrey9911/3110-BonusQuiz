using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    public int _health = 100;

    public TMP_Text _healthText;


    private void Awake()
    {
        if(!Instance)
            Instance = this;
    }

    private void Start()
    {
        _healthText.text = " Your Health: [" + _health.ToString() + "]";
    }

    public void ChangeHealth(int healthToChange)
    {
        _health += healthToChange;
        _healthText.text = " Your Health: [" + _health.ToString() + "]";
    }
}
