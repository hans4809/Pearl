using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UI_LoadingScene;

public class UI_CutScene : UI_Popup
{
    [SerializeField] Sprite[] _sprites;
    public Sprite[] Sprites { get => _sprites; set => _sprites = value; }// 페이드할 이미지 배열
    [SerializeField] float _fadeDuration = 1f; // 페이드 인/아웃 지속 시간
    public float FadeDuration { get => _fadeDuration; set => _fadeDuration = value; }
    [SerializeField] float _displayDuration = 5f; // 각 이미지가 표시되는 시간
    public float DisplayDuration { get => _displayDuration; set => _displayDuration = value; }
    [SerializeField] Image _cutScene;
    public Image CutScene { get => _cutScene; set => _cutScene = value; }
    [SerializeField] Coroutine _cor;
    Coroutine Cor { get => _cor; set => _cor = value; }

    public override void Init()
    {
        base.Init();
        StartCoroutine(MoveToExplain());
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator MoveToExplain()
    {
        yield return StartCoroutine(FadeImages());
        Managers.Scene.LoadScene(Define.Scene.ExplainScene);
    }
    
    private IEnumerator FadeImages()
    {
        int index = 0;
        while (index < _sprites.Length)
        {
            CutScene.sprite = _sprites[index];
            yield return StartCoroutine(FadeImage(CutScene, FadeDuration, () => Input.GetMouseButtonDown(0)));
            yield return StartCoroutine(WaitForSecondsWithSkip(FadeDuration, () => Input.GetMouseButtonDown(0)));
            yield return StartCoroutine(FadeImage(CutScene, FadeDuration, () => Input.GetMouseButtonDown(0), false));
            index++;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
