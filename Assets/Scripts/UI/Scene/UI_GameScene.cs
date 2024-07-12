using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_GameScene : UI_Scene
{
    [SerializeField] float _fadeTime = 0.5f;
    public float FadeTime { get => _fadeTime; private set => _fadeTime = value; }

    [SerializeField] float _maxFontSize = 250f;
    public float MaxFontSize { get => _maxFontSize; private set => _maxFontSize = value; }

    [SerializeField] float _minFontSize = 200f;
    public float MinFontSize { get => _minFontSize; private set => _minFontSize = value; }

    [SerializeField] TMP_Text _startTimerText;
    public TMP_Text StartTimerText { get => _startTimerText; set => _startTimerText = value; }

    [SerializeField] TMP_Text _gameTimerText;
    public TMP_Text GameTimerText { get => _gameTimerText; set => _gameTimerText = value; }

    [SerializeField] TMP_Text _player1_ItemText;
    public TMP_Text Player1_ItemText { get => _player1_ItemText; set => _player1_ItemText = value; }

    [SerializeField] TMP_Text _player2_ItemText;
    public TMP_Text Player2_ItemText { get => _player2_ItemText; set => _player2_ItemText = value; }

    [SerializeField] float _cameraRotateAngle;
    public float CameraRotateAngle { get => _cameraRotateAngle; private set => _cameraRotateAngle = value; }

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
        text.fontSize = MinFontSize;

        float elapsedTime = 0f;
        while (text.color.a < 1.0f && text.fontSize <= MaxFontSize)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / FadeTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

            float fontSize = Mathf.Lerp(MinFontSize, MaxFontSize, alpha);
            text.fontSize = fontSize;
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
        text.fontSize = MaxFontSize;

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
