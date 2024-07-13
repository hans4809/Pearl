using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    #region TimerVariable
    [Header("TimerVariable")]
    [SerializeField] float _fadeTime = 0.5f;
    public float FadeTime { get => _fadeTime; private set => _fadeTime = value; }

    [SerializeField] float _maxStartTimerFontSize = 250f;
    public float MaxStartTimerFontSize { get => _maxStartTimerFontSize; private set => _maxStartTimerFontSize = value; }

    [SerializeField] float _minStartTimerFontSize = 200f;
    public float MinStartTimerFontSize { get => _minStartTimerFontSize; private set => _minStartTimerFontSize = value; }

    [SerializeField] float _maxGameTimerFontSize;
    public float MaxGameTimerFontSize { get => _maxGameTimerFontSize; private set => _maxGameTimerFontSize = value; }

    [SerializeField] float _minGameTimerFontSize;
    public float MinGameTimerFontSize { get => _minGameTimerFontSize; private set => _minGameTimerFontSize = value; }
    #endregion

    #region TimeLimitVariable
    [Header("TimeLimitVariable")]
    [SerializeField] Color _timeLimitTimerColor;
    public Color TimeLimitTimerColor { get => _timeLimitTimerColor; private set => _timeLimitTimerColor = value; }
    [SerializeField] float _timeLimitEffectTime = 1f;
    public float TimeLimitEffectTime { get => _timeLimitEffectTime; private set => _timeLimitEffectTime = value; }
    [SerializeField] float _textRotation = 10f;
    public float TextRotation { get => _textRotation; private set => _textRotation = value; }
    [SerializeField] float _cameraRotateAngle;
    public float CameraRotateAngle { get => _cameraRotateAngle; private set => _cameraRotateAngle = value; }
    #endregion
    [Header("EndTimerVariable")]

    #region EndTimerVariable
    [SerializeField] Color _endTimerColor;
    public Color EndTimerColor { get => _endTimerColor; private set => _endTimerColor = value; }
    #endregion
    [Header("EndGameVariable")]
    [SerializeField] float _maxEndTextFontSize = 250f;
    public float MaxEndTextFontSize { get => _maxEndTextFontSize; private set => _maxEndTextFontSize = value; }

    [SerializeField] float _minEndTextFontSize = 200f;
    public float MinEndTextFontSize { get => _minEndTextFontSize; private set => _minEndTextFontSize = value; }

    [SerializeField] float _endEffectTime = 1;
    public float EndEffectTime { get => _endEffectTime; private set => _endEffectTime = value; }
    [Header("PlayerScoreVariable")]
    [SerializeField] Image _player1ScoreIMG;
    public Image Player1ScoreIMG { get => _player1ScoreIMG; private set => _player1ScoreIMG = value; }

    [SerializeField] Image _player2ScoreIMG;
    public Image Player2ScoreIMG { get => _player2ScoreIMG; private set => _player2ScoreIMG = value; }

    [SerializeField] float _changedScale;
    public float ChangedScale { get => _changedScale; private set => _changedScale = value; }

    [SerializeField] float _changedRotation;
    public float ChangedRotation { get => _changedRotation; private set => _changedRotation = value; }

    [SerializeField] float _changeEffectTime = 1;
    public float ChangeEffectTime { get => _changeEffectTime; private set => _changeEffectTime = value; }

    [Header("Text Variable")]
    [SerializeField] TMP_Text _startTimerText;
    public TMP_Text StartTimerText { get => _startTimerText; set => _startTimerText = value; }

    [SerializeField] TMP_Text _endTimerText;
    public TMP_Text EndTimerText { get => _endTimerText; set => _endTimerText = value; }

    [SerializeField] TMP_Text _gameTimerText;
    public TMP_Text GameTimerText { get => _gameTimerText; set => _gameTimerText = value; }

    [SerializeField] TMP_Text _player1_ItemText;
    public TMP_Text Player1_ItemText { get => _player1_ItemText; set => _player1_ItemText = value; }

    [SerializeField] TMP_Text _player2_ItemText;
    public TMP_Text Player2_ItemText { get => _player2_ItemText; set => _player2_ItemText = value; }

    [SerializeField] TMP_Text _gameEndingText;
    public TMP_Text GameEndingText { get => _gameEndingText;  set => _gameEndingText = value; }

    public IEnumerator StartTimerEffect_FadeIn(TMP_Text text, float opacity = 1)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.fontSize = MinStartTimerFontSize;

        float elapsedTime = 0f;
        while (text.color.a < opacity || text.fontSize <= MaxStartTimerFontSize)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / FadeTime);
            float a =  Mathf.Lerp(0, opacity, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);

            float fontSize = Mathf.Lerp(MinStartTimerFontSize, MaxStartTimerFontSize, alpha);
            text.fontSize = fontSize;
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
        text.fontSize = MaxStartTimerFontSize;

        StartCoroutine(StartTimerEffect_FadeOut(text, opacity));
    }

    public IEnumerator StartTimerEffect_FadeOut(TMP_Text text, float opacity = 1)
    {
        float elapsedTime = 0f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);

        while (text.color.a > 0.0f)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1.0f - (elapsedTime / FadeTime));
            float a = Mathf.Lerp(0, opacity, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);

            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    public IEnumerator TimeLimitEffectFirst(TMP_Text text)
    {
        float elapsedTime = 0f;
        text.fontSize = MinGameTimerFontSize;
        text.color = TimeLimitTimerColor;
        var mainCamera = Camera.main;
        mainCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -CameraRotateAngle));
        while(text.fontSize < MaxGameTimerFontSize)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / TimeLimitEffectTime);
            float fontSize = Mathf.Lerp(MinGameTimerFontSize, MaxGameTimerFontSize, alpha);
            float cameraRotation = Mathf.Lerp(-CameraRotateAngle, CameraRotateAngle, alpha);
            mainCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 0, cameraRotation));
            text.fontSize = fontSize;

            yield return null;
        }
        text.fontSize = MinGameTimerFontSize;
        mainCamera.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public IEnumerator TimeLimitEffectIteration(TMP_Text text)
    {
        float elapsedTime = 0f;
        text.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -TextRotation));
        while(text.rectTransform.rotation.z < TextRotation)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / TimeLimitEffectTime);
            float textRotation = Mathf.Lerp(-TextRotation, TextRotation, alpha);
            text.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, textRotation));
            yield return null;
        }
    }

    public IEnumerator TimeEndEffect(TMP_Text text)
    {
        float elapsedTime = 0f;
        text.fontSize = MinEndTextFontSize;
        while(text.fontSize < MaxEndTextFontSize)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / EndEffectTime);
            float fontSize = Mathf.Lerp(MinEndTextFontSize, MaxEndTextFontSize, alpha);
            text.fontSize = fontSize;
            yield return null;
        }

        text.fontSize = MaxEndTextFontSize;
    }

    public void UpdatePlayerScore()
    {

    }

    public IEnumerator OnChangedBestPlayer(Image originBestPlayerIMG, Image changedBestPlayerIMG)
    {
        float elapsedTime = 0f;
        changedBestPlayerIMG.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -ChangedRotation));
        while(changedBestPlayerIMG.transform.localScale.x < ChangedScale && changedBestPlayerIMG.transform.rotation.z < ChangedRotation)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime/ChangeEffectTime);
            float targetScale = Mathf.Lerp(1, ChangedScale, alpha);
            float targetRotation = Mathf.Lerp(-ChangedRotation, ChangedRotation, alpha);
            changedBestPlayerIMG.transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetRotation));
            changedBestPlayerIMG.transform.localScale = new Vector3(targetScale, targetScale, changedBestPlayerIMG.transform.localScale.z);
            yield return null;
        }

        changedBestPlayerIMG.transform.localScale = Vector3.one;
        changedBestPlayerIMG.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void Start()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        while(true)
        {
            yield return StartCoroutine(OnChangedBestPlayer(Player2ScoreIMG, Player1ScoreIMG));
            yield return new WaitForSecondsRealtime(5f); // 반복 실행 전에 대기
        }
    }
}
