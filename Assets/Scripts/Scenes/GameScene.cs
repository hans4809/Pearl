using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GameScene : BaseScene
{
    [SerializeField] float _startTimer = 3;
    public float StartTimer 
    { 
        get => _startTimer;
        set
        {
            if(value > 0)
            {
                _startTimer = value;
                (SceneUI as UI_GameScene).StartTimerText.text = $"{_startTimer}";
            }
            else
            {
                _startTimer = 0;
                (SceneUI as UI_GameScene).StartTimerText.gameObject.SetActive(false);
            }

        }
    }

    [SerializeField] float _gameTimer = 60;
    public float GameTimer 
    {
        get => _gameTimer;
        set
        {
            if (value > 0)
            {
                _gameTimer = value;
                (SceneUI as UI_GameScene).GameTimerText.text = $"{_gameTimer}";
            }
            else
            {
                _gameTimer = 0;
                Managers.Game.GameEnd();
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
        Time.timeScale = 0;
        StartTimer = 3f;
        while (StartTimer > 0f)
        {
            yield return new WaitForSecondsRealtime(1f);
            StartTimer -= 1f;
        }
        StartCoroutine(GamePlayTimer());
    }

    IEnumerator GamePlayTimer()
    {
        Time.timeScale = 1;
        GameTimer = 60f;
        while (GameTimer > 0f)
        {
            yield return new WaitForSecondsRealtime(1f);
            GameTimer -= 1f;
        }
    }
}
