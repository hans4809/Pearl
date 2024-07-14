using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MainScene : UI_Scene
{
    [SerializeField] TextMeshProUGUI _mainSceneText;
    public TextMeshProUGUI MainSceneText { get => _mainSceneText; private set => _mainSceneText = value; }

    [SerializeField] Coroutine _cor;
    public Coroutine Cor { get => _cor; private set => _cor = value; }
    [SerializeField] UI_CutScene _cutScene;
    public UI_CutScene CutScene { get => _cutScene;  private set => _cutScene = value; }
    public override void Init()
    {
        base.Init();
        if (MainSceneText == null)
            MainSceneText = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(Managers.UI.BlinkText(MainSceneText, MainSceneText.text));
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && CutScene == null)
        {
            //Managers.Scene.LoadScene(Define.Scene.ExplainScene);
            //Managers.Scene.Clear();
            //Managers.Scene.CurrentScene.SceneUI = Managers.UI.ShowSceneUI<UI_ExplainScene>();
            //if(Cor == null)
            //    Cor = StartCoroutine(Managers.Scene.LoadSceneAsync<UI_ExplainScene>(Define.Scene.GameScene));
            CutScene = Managers.UI.ShowPopUpUI<UI_CutScene>();
            gameObject.SetActive(false);
        }
    }
    public void OnClickStart()
    {

    }
    public void OnClickExit()
    {
        Application.Quit();
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
