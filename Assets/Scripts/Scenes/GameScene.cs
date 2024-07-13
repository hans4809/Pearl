using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GameScene : BaseScene
{
    [SerializeField] int _timeLimit = 10;
    public int TimeLimit { get => _timeLimit; private set => _timeLimit = value; }

    [SerializeField] int _startTimer = 3;

    [SerializeField] Coroutine _gameEndTimerCoroutine;
    public Coroutine GameEndTimerCoroutine { get => _gameEndTimerCoroutine; private set => _gameEndTimerCoroutine = value; }

    [SerializeField] Transform[] _spawnTransforms;
    public Transform[] SpawnTransfroms { get => _spawnTransforms; private set => _spawnTransforms = value; }
    public int StartTimer 
    { 
        get => _startTimer;
        set
        {
            if(value > 0)
            {
                _startTimer = value;
                (SceneUI as UI_GameScene).StartTimerText.text = $"{_startTimer}";
                StartCoroutine((SceneUI as UI_GameScene).StartTimerEffect_FadeIn((SceneUI as UI_GameScene).StartTimerText));
            }
            else
            {
                _startTimer = 0;
                (SceneUI as UI_GameScene).StartTimerText.gameObject.SetActive(false);
                Managers.Game.GameStart();
            }

        }
    }

    [SerializeField] int _gameTimer = 60;
    public int GameTimer 
    {
        get => _gameTimer;
        set
        {
            if (value > 0)
            {
                _gameTimer = value;
                (SceneUI as UI_GameScene).GameTimerText.text = $"{_gameTimer}";
                if (_gameTimer <= TimeLimit)
                {
                    if(_gameTimer == TimeLimit)
                        StartCoroutine((SceneUI as UI_GameScene).TimeLimitEffectFirst((SceneUI as UI_GameScene).GameTimerText));
                    StartCoroutine((SceneUI as UI_GameScene).TimeLimitEffectIteration((SceneUI as UI_GameScene).GameTimerText));

                    if (_gameTimer == 3)
                    {
                        if (GameEndTimerCoroutine != null)
                        {
                            StopCoroutine(GameEndTimerCoroutine);
                            GameEndTimerCoroutine = null;
                        }

                        GameEndTimerCoroutine = StartCoroutine(GameEndTimer());
                    }

                }
            }
            else
            {
                _gameTimer = 0;
            }
        }
    }

    [SerializeField] int _endTimer = 3;
    public int EndTimer
    {
        get => _endTimer;
        set
        {
            if (value > 0)
            {
                _endTimer = value;
                if(!(SceneUI as UI_GameScene).EndTimerText.gameObject.activeInHierarchy)
                    (SceneUI as UI_GameScene).EndTimerText.gameObject.SetActive(true);
                (SceneUI as UI_GameScene).EndTimerText.text = $"{_endTimer}";
                StartCoroutine((SceneUI as UI_GameScene).StartTimerEffect_FadeIn((SceneUI as UI_GameScene).EndTimerText, (SceneUI as UI_GameScene).EndTimerColor.a));
            }
            else
            {
                _endTimer = 0;
                (SceneUI as UI_GameScene).EndTimerText.gameObject.SetActive(false);
                StartCoroutine(Ending());
            }
        }
    }

    public override void Clear()
    {
        StopAllCoroutines();
    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameScene;
        SceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();
    }

    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameStartTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameStartTimer()
    {
        //Time.timeScale = 0;
        StartTimer = 3;
        while (StartTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            StartTimer -= 1;
        }
        StartCoroutine(GamePlayTimer());
    }

    IEnumerator GamePlayTimer()
    {
        Time.timeScale = 1;
        GameTimer = 60;
        while (GameTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            GameTimer -= 1;
        }
    }

    IEnumerator GameEndTimer()
    {
        EndTimer = 3;
        while(EndTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            EndTimer -= 1;
        }
    }

    IEnumerator Ending()
    {
        if(!(SceneUI as UI_GameScene).GameEndingText.gameObject.activeInHierarchy)
            (SceneUI as UI_GameScene).GameEndingText.gameObject.SetActive(true);
        Coroutine cor = StartCoroutine((SceneUI as UI_GameScene).TimeEndEffect((SceneUI as UI_GameScene).GameEndingText));
        yield return cor;
        Managers.Game.GameEnd();
    }
}
