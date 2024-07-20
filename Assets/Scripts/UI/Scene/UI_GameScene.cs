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
    [SerializeField] Image[] _playerScoreIMGs = new Image[2];
    public Image[] PlayerScoreIMGs { get => _playerScoreIMGs; private set => _playerScoreIMGs = value; }
    [SerializeField] Coroutine _changeCoroutine;
    public Coroutine ChangeCoroutine { get => _changeCoroutine; private set => _changeCoroutine = value; }

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

    [SerializeField] TMP_Text _changedTimeText;
    public TMP_Text ChangedTimeText { get => _changedTimeText; set => _changedTimeText = value; }

    public void StartTimerEffect(TMP_Text text)
    {
        StartCoroutine(StartEffect(text));
    }

    public IEnumerator StartEffect(TMP_Text text)
    {
        StartCoroutine(FadeText(text, FadeTime));
        yield return StartCoroutine(TextSizeUp(text, MinStartTimerFontSize, MaxStartTimerFontSize, FadeTime));
        StartCoroutine(FadeText(text, FadeTime, 1, false));
    }

    public void EndTimerEffect(TMP_Text text)
    {
        StartCoroutine(EndEffect(text));
    }

    public IEnumerator EndEffect(TMP_Text text) 
    {
        StartCoroutine(FadeText(text, FadeTime));
        yield return StartCoroutine(TextSizeUp(text, MinEndTextFontSize, MaxStartTimerFontSize, FadeTime));
        StartCoroutine(FadeText(text, FadeTime, EndTimerColor.a, false));
    }

    public void TimeLimitEffect(TMP_Text text)
    {
        if((Managers.Scene.CurrentScene as GameScene).GameTimer == (Managers.Scene.CurrentScene as GameScene).TimeLimit)
            StartCoroutine(TextSizeUp(text, MinGameTimerFontSize, MaxGameTimerFontSize, TimeLimitEffectTime));

        StartCoroutine(RotateObject(text.transform, TextRotation, TimeLimitEffectTime));
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

    public void UpdatePlayerScore(int orginBestPlayerIndex, int changedBestPlayerIndex)
    {
        if(ChangeCoroutine != null)
        {
            StopCoroutine(ChangeCoroutine); 
            ChangeCoroutine = null;
            for (int i = 0; i < 2; i++)
                PlayerScoreIMGs[i].transform.localScale = new Vector3(1, 1, PlayerScoreIMGs[i].transform.localScale.z);
        }

        ChangeCoroutine = StartCoroutine(OnChangedBestPlayerIteration(orginBestPlayerIndex, changedBestPlayerIndex));
    }


    public IEnumerator OnChangedBestPlayerIteration(int orginBestPlayerIndex, int changedBestPlayerIndex)
    {
        yield return StartCoroutine(OnChangedBestPlayer(orginBestPlayerIndex, changedBestPlayerIndex, 0.8f, (0.8f + (ChangedScale - 0.8f)/3), -ChangedRotation, 0, ChangeEffectTime / 3));

        yield return StartCoroutine(OnChangedBestPlayer(orginBestPlayerIndex, changedBestPlayerIndex, (0.8f + (ChangedScale - 0.8f) / 3), (0.8f + 2 * (ChangedScale - 0.8f) / 3), 0, ChangedRotation, ChangeEffectTime / 3));
        // 두 번째 코루틴: 현재 각도에서 (0,0,0)으로 회전
        yield return StartCoroutine(OnChangedBestPlayer(orginBestPlayerIndex, changedBestPlayerIndex, (0.8f + 2 * (ChangedScale - 0.8f) / 3), ChangedScale, ChangedRotation, 0, ChangeEffectTime / 3));
       
        PlayerScoreIMGs[changedBestPlayerIndex].transform.localScale = new Vector3(0.8f, 0.8f, 1);
        PlayerScoreIMGs[changedBestPlayerIndex].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public IEnumerator OnChangedBestPlayer(int orginBestPlayerIndex, int changedBestPlayerIndex, float fromScale, float toScale, float fromRotation, float toRotation, float duration)
    {
        float elapsedTime = 0f;
        if (orginBestPlayerIndex == -1)
        {
            PlayerScoreIMGs[changedBestPlayerIndex].sprite = Managers.Resource.Load<Sprite>($"Sprites/UI/BestScore{changedBestPlayerIndex + 1}");
        }
        else
        {
            PlayerScoreIMGs[orginBestPlayerIndex].sprite = Managers.Resource.Load<Sprite>($"Sprites/UI/NormalScore{orginBestPlayerIndex + 1}");
            PlayerScoreIMGs[orginBestPlayerIndex].transform.localScale = new Vector3(0.8f, 0.8f, 1);
            PlayerScoreIMGs[changedBestPlayerIndex].sprite = Managers.Resource.Load<Sprite>($"Sprites/UI/BestScore{changedBestPlayerIndex + 1}");
        }

        PlayerScoreIMGs[changedBestPlayerIndex].transform.rotation = Quaternion.Euler(new Vector3(0, 0, fromRotation));
        PlayerScoreIMGs[changedBestPlayerIndex].transform.localScale = new Vector3(fromScale, fromScale, 1);

        while(PlayerScoreIMGs[changedBestPlayerIndex].transform.localScale.x < toScale)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            float targetScale = Mathf.Lerp(fromScale, toScale, alpha);
            float targetRotation = Mathf.Lerp(fromRotation, toRotation, alpha);
            PlayerScoreIMGs[changedBestPlayerIndex].transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetRotation));
            PlayerScoreIMGs[changedBestPlayerIndex].transform.localScale = new Vector3(targetScale, targetScale, 1);
            yield return null;
        }
        PlayerScoreIMGs[changedBestPlayerIndex].transform.rotation = Quaternion.Euler(new Vector3(0, 0, toRotation));
        PlayerScoreIMGs[changedBestPlayerIndex].transform.localScale = new Vector3(toScale, toScale, 1);
    }

    public IEnumerator OnChangedTimer(int timer)
    {
        ChangedTimeText.gameObject.SetActive(true);
        if (timer > 0) ChangedTimeText.text = "+ 10";
        else ChangedTimeText.text = "- 10";
        yield return FadeInChangedTimeText(ChangedTimeText);
        yield return new WaitForSeconds(1f);
        yield return FadeOutChangedTimeText(ChangedTimeText);
        ChangedTimeText.gameObject.SetActive(false);
    }

    public IEnumerator FadeInChangedTimeText(TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        float elapsedTime = 0f;
        while (text.color.a < 1)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / 0.5f);
            float a = Mathf.Lerp(0, 1, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    }

    public IEnumerator FadeOutChangedTimeText(TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        float elapsedTime = 0f;
        while (text.color.a > 0)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / 0.5f);
            float a = Mathf.Lerp(1, 0, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }


    public override void Init()
    {
        base.Init();
    }

    private void Start()
    {
        Init();
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
