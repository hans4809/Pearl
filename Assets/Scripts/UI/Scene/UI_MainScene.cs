using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MainScene : UI_Scene
{
    [SerializeField] TextMeshProUGUI _mainSceneText;
    public TextMeshProUGUI MainSceneText { get => _mainSceneText; private set => _mainSceneText = value; }
    public override void Init()
    {
        base.Init();
        if (MainSceneText == null)
            MainSceneText = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(Managers.UI.BlinkText(MainSceneText));
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            //Managers.Scene.LoadScene(Define.Scene.ExplainScene);
            Managers.Scene.Clear();
            Managers.Scene.CurrentScene.SceneUI = Managers.UI.ShowSceneUI<UI_ExplainScene>();
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
