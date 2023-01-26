using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using ScriptableObjects;
using UniRx;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public static GameState Instance;
    [NonSerialized] public ReactiveProperty<int> coin;
    [NonSerialized] public ReactiveProperty<int> score;
    [NonSerialized] public ReactiveProperty<int> highScore;
    [NonSerialized] public Levels levels;
    [NonSerialized] public Dictionary<SpaceCrewType, SpaceCrew> spaceCrews;
    private void Awake()
    {
        Instance = this;

        var highScoreInt = Storage.GetHighScore();
        var coinInt = Storage.GetCoin();
        
        coin = new ReactiveProperty<int>(coinInt);
        score = new ReactiveProperty<int>(0);
        highScore = new ReactiveProperty<int>(highScoreInt);
        levels = Resources.Load<Levels>("Levels");
        var red = Resources.Load<SpaceCrew>("RedCrew");
        var yellow = Resources.Load<SpaceCrew>("YellowCrew");
        var cyan = Resources.Load<SpaceCrew>("CyanCrew");
        
        spaceCrews = new Dictionary<SpaceCrewType, SpaceCrew>()
        {
            {red.type, red},
            {yellow.type, yellow},
            {cyan.type, cyan},
        };

        highScore.AsObservable().Subscribe(x =>
        {
            var current = Storage.GetHighScore();
            if (current < x) Storage.SaveHighScore(x);
        });
        
        coin.AsObservable().Subscribe(x =>
        {
            Storage.SaveCoin(x);
        });
        
        DontDestroyOnLoad(gameObject);
    }
}