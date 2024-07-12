using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public float MaxGameTimerFontSize { get => _maxGameTimerFontSize; private set => _maxGameTimerFontSize  = value; }

    [SerializeField] float _minGameTimerFontSize;
    public float MinGameTimerFontSize { get => _minGameTimerFontSize; private set => _minGameTimerFontSize = value; }
    #endregion

    #region TimeLimitVariable
    [Header("TimeLimitVariable")]
    [SerializeField] Color _timeLimitTimerColor;
    public Color TimeLimitTimerColor { get => _timeLimitTimerColor; private set => _timeLimitTimerColor = value; }
    [SerializeField] float _timeLimitEffectTime = 1f;
    public float TimeLimitEffectTime {  get => _timeLimitEffectTime; private set => _timeLimitEffectTime = value;}
    [SerializeField] float _textRotation = 10f;
    public float TextRotation { get => _textRotation; private set => _textRotation = value; }
    [SerializeField] float _cameraRotateAngle;
    public float CameraRotateAngle { get => _cameraRotateAngle; private set => _cameraRotateAngle = value; }
    #endregion

    [SerializeField] TMP_Text _startTimerText;
    public TMP_Text StartTimerText { get => _startTimerText; set => _startTimerText = value; }

    [SerializeField] TMP_Text _gameTimerText;
    public TMP_Text GameTimerText { get => _gameTimerText; set => _gameTimerText = value; }

    [SerializeField] TMP_Text _player1_ItemText;
    public TMP_Text Player1_ItemText { get => _player1_ItemText; set => _player1_ItemText = value; }

    [SerializeField] TMP_Text _player2_ItemText;
    public TMP_Text Player2_ItemText { get => _player2_ItemText; set => _player2_ItemText = value; }



    public void FadeIn(TMP_Text text)
    {
        if(text != null)
        {
            StartCoroutine(FadeInText(text));
        }
    }

    public IEnumerator FadeInText(TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.fontSize = MinStartTimerFontSize;

        float elapsedTime = 0f;
        while (text.color.a < 1.0f || text.fontSize <= MaxStartTimerFontSize)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / FadeTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

            float fontSize = Mathf.Lerp(MinStartTimerFontSize, MaxStartTimerFontSize, alpha);
            text.fontSize = fontSize;
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
        text.fontSize = MaxStartTimerFontSize;

        StartCoroutine(FadeOutText(text));
    }

    public IEnumerator FadeOutText(TMP_Text text)
    {
        float elapsedTime = 0f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        while (text.color.a > 0.0f)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1.0f - (elapsedTime / FadeTime));
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    public IEnumerator SizeUpText(TMP_Text text)
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

    public IEnumerator RotateText(TMP_Text text)
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
