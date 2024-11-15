using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("# Game Object")]
    public Player Player;
    public PoolManager Pool;
    [Header("# Game Control")]
    public float GameTime = 0;
    public float MaxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public int Level;
    public int Kill;
    public int Exp;
    public int[] NextExp = {3, 5, 10, 60, 100, 150, 210, 280, 360};
    public int Health;
    public int MaxHealth;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        GameTime += Time.deltaTime;
        if(GameTime >= MaxGameTime)
        {
            GameTime = MaxGameTime;
        }
    }

    public void PlusKill()
    {
        Kill += 1;
    }

    public void GetExp()
    {
        Exp += 1;
        if(Exp == NextExp[Level])
        {
            Level += 1;
            Exp = 0;
            Level = Mathf.Min(Level, NextExp.Length - 1);
        }
    }
}
