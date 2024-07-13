using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public override void Clear()
    {
        //Destroy(SceneUI.gameObject);
    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.MainScene;
        SceneUI = Managers.UI.ShowSceneUI<UI_MainScene>();
    }
    void Awake()
    {
        Init();
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
