using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainScene : BaseScene
{
    public override void Clear()
    {
        
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.ExplainScene;
        Managers.UI.ShowSceneUI<UI_ExplainScene>();
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
}
