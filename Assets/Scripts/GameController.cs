using System.Collections.Generic;
using DG.Tweening;
using ScriptableObjects;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private SpaceCrewMono spaceCrewMono;
    [SerializeField] private SpaceCrewMono susCrew;

    [SerializeField] private Text highScore;
    [SerializeField] private Text score;
    [SerializeField] private Text coin;
    [SerializeField] private Slider timer;
    [SerializeField] private Text timerText;

    [SerializeField] private int time;
    [SerializeField] private int scoreForEachLevel;
    [SerializeField] private int coinForEachImpostor;

    [SerializeField] private GameObject loseScreen;
    [SerializeField] private Button exit;
    [SerializeField] private Button loseScreenExit;
    [SerializeField] private Text loseScreenText;
    
    
    private Dictionary<SpaceCrewType, SpaceCrew> _dict;
    private int _currentSusCount, _currentLevel;
    private List<SpaceCrewMono> _crews;
    private Tweener _tweener;
    private Levels _levels;
    private int _score;
    private int _coin;
    private void Awake()
    {
        _dict = GameState.Instance.spaceCrews;
        _levels = GameState.Instance.levels;
        _score = GameState.Instance.score.Value;
        _coin = GameState.Instance.coin.Value;
        _currentLevel = 0;
        
        // initial setup
        Setup(_levels.levels[_currentLevel].n, _levels.levels[_currentLevel].susCount);

        
        // handle timer
        _tweener = DOTween.To(() => timer.value, x =>
            {
                timer.value = x;
                timerText.text = $"Time: {(x * time):0.00}";
            }, 1, time)
            .OnComplete(() => Lose((true)));

        GameState.Instance.score.AsObservable().Subscribe(x =>
        {
            DOTween.To(() => _score, a =>
            {
                _score = a;
                score.text = $"Score: {a}";
            }, x, .3f);
        });
        
        GameState.Instance.coin.AsObservable().Subscribe(x =>
        {
            DOTween.To(() => _coin, a =>
            {
                _coin = a;
                coin.text = $"Coin: {a}";
            }, x, .3f);
        });

        GameState.Instance.highScore.AsObservable().Subscribe(x =>
        {
            highScore.text = $"High Score: {x}";
        });
        
        exit.onClick.AddListener(() => MainMenu(false));
        loseScreenExit.onClick.AddListener(() => MainMenu(false));
    }

    private void MainMenu(bool showLoseScreen)
    {
        Lose(showLoseScreen);
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    private void ClearGrid()
    {
        if (_crews == null) return;
        if (_crews.Count == 0) return;
        
        foreach (var spaceCrewMono in _crews)
        {
            spaceCrewMono.onClickAction -= OnSpaceCrewClick;
            Destroy(spaceCrewMono.gameObject);
        }
    } 
    
    private void Setup(int n, int susCount)
    {
        ClearGrid();

        var susType = (SpaceCrewType)Random.Range(0, 3);
        _crews = new List<SpaceCrewMono>();
        
        // set grid size
        grid.SetGridSize(n);
        // set sus crew
        susCrew.Init(true, _dict[susType]);
        _currentSusCount = susCount;
        
        for (int i = 0; i < n * n; i++)
        {
            var obj = Instantiate(spaceCrewMono, grid.transform);
            obj.onClickAction += OnSpaceCrewClick;
            _crews.Add(obj);
            
            if (susCount == 0)
            {
                obj.Init(false, _dict[GetOtherType(susType)]);
                continue;
            }
            
            obj.Init(true, _dict[susType]);
            susCount--;
        }
    }

    private void OnSpaceCrewClick(bool isSus)
    {
        if (!isSus)
        {
            _tweener.Kill();
            Lose(true);
            return;
        }

        GameState.Instance.coin.Value += coinForEachImpostor;
        _currentSusCount--;
        if(_currentSusCount != 0) return;
        
        NextLevel();
    }

    private void NextLevel()
    {
        GameState.Instance.score.Value += scoreForEachLevel;
        
        _tweener.Restart();
        _currentLevel++;
        if (_currentLevel == _levels.levels.Count) _currentLevel--;
        Setup(_levels.levels[_currentLevel].n, _levels.levels[_currentLevel].susCount);
    }

    private void Lose(bool showLoseScreen)
    {
        // save score
        GameState.Instance.highScore.Value = GameState.Instance.score.Value;
        GameState.Instance.score.Value = 0;
        if (!showLoseScreen) return;
            
        var cg = loseScreen.GetComponent<CanvasGroup>();
        cg.DOFade(1, .3f).OnComplete(() => cg.blocksRaycasts = true);

    }
    private SpaceCrewType GetOtherType(SpaceCrewType susType)
    {
        while (true)
        {
            var t = (SpaceCrewType)Random.Range(0, 3);
            if (t != susType) return t;
        }
    }
}