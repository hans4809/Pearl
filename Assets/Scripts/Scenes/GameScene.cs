using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] CameraControllerEx _cameraController;

    [SerializeField] Coroutine _cameraRotateCoroutine;
    public Coroutine CameraRotateCoroutine { get => _cameraRotateCoroutine; private set => _cameraRotateCoroutine = value; }

    public delegate void TextEffectDelegate(TMP_Text text);
    public delegate void ImageEffectDelegate(TMP_Text text);

    public TextEffectDelegate StartTextDelegate { get; set; }
    public TextEffectDelegate EndTextDelegate { get; set; }
    public TextEffectDelegate TimeLimitDelegate { get; set; }
    public int StartTimer 
    { 
        get => _startTimer;
        set
        {
            if(value > 0)
            {
                _startTimer = value;
                (SceneUI as UI_GameScene).StartTimerText.text = $"{_startTimer}";
                StartTextDelegate.Invoke((SceneUI as UI_GameScene).StartTimerText);
                //StartCoroutine((SceneUI as UI_GameScene).StartTimerEffect_FadeIn((SceneUI as UI_GameScene).StartTimerText));
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
            var sceneUI = SceneUI as UI_GameScene;
            if (value > 0)
            {
                _gameTimer = value;
                
                if (_gameTimer <= TimeLimit)
                {
                    sceneUI.GameTimerText.color = Color.red;
                    sceneUI.GameTimerText.fontSize = 40f;
                    if (_gameTimer == TimeLimit)
                    {
                        //StartCoroutine((SceneUI as UI_GameScene).TimeLimitFirstEffectText((SceneUI as UI_GameScene).GameTimerText));
                        if(CameraRotateCoroutine != null)
                        {
                            StopCoroutine(CameraRotateCoroutine);
                            CameraRotateCoroutine = null;
                            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        }
                        CameraRotateCoroutine = StartCoroutine(sceneUI.RotateObject(Camera.main.transform, sceneUI.CameraRotateAngle, 1, 5));
                    }
                    TimeLimitDelegate.Invoke(sceneUI.GameTimerText);
                    //StartCoroutine((SceneUI as UI_GameScene).TimeLimitEffectIteration((SceneUI as UI_GameScene).GameTimerText));

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
                else
                {
                    sceneUI.GameTimerText.color = Color.white;
                    sceneUI.GameTimerText.fontSize = 40f;
                }
                sceneUI.GameTimerText.transform.rotation = Quaternion.Euler(Vector3.zero);
                sceneUI.GameTimerText.text = $"{_gameTimer}";
            }
            else
            {
                _gameTimer = 0;
                sceneUI.GameTimerText.transform.rotation = Quaternion.Euler(Vector3.zero);
                sceneUI.GameTimerText.color = Color.red;
                sceneUI.GameTimerText.fontSize = 40f;
                sceneUI.GameTimerText.text = $"{_gameTimer}";
                StartCoroutine(Ending());
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
                EndTextDelegate.Invoke((SceneUI as UI_GameScene).EndTimerText);
                //StartCoroutine((SceneUI as UI_GameScene).StartTimerEffect_FadeIn((SceneUI as UI_GameScene).EndTimerText, (SceneUI as UI_GameScene).EndTimerColor.a));
            }
            else
            {
                _endTimer = 0;
                (SceneUI as UI_GameScene).EndTimerText.text = $"{_endTimer}";
                (SceneUI as UI_GameScene).EndTimerText.gameObject.SetActive(false);
            }
        }
    }

    public override void Clear()
    {
        StopAllCoroutines();
        Destroy(SceneUI.gameObject);
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameScene;
        SceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();

        StartTextDelegate += (SceneUI as UI_GameScene).StartTimerEffect;
        TimeLimitDelegate += (SceneUI as UI_GameScene).TimeLimitEffect;
        EndTextDelegate += (SceneUI as UI_GameScene).EndTimerEffect;

        StartCoroutine(_cameraController.CameraZoomIn(StartTimer));
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
        StartCoroutine(Managers.Scene.LoadSceneAsync<UI_GameOverScene>(Define.Scene.MainScene));
        Managers.Game.GameEnd();
        
    }
}
