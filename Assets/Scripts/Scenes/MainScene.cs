using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    [SerializeField] bool _bIsNext = false;
    public bool IsNext { get => _bIsNext; set => _bIsNext = value; }
    public override void Clear()
    {
        //Destroy(SceneUI.gameObject);
    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.MainScene;
        SceneUI = Managers.UI.ShowSceneUI<UI_MainScene>();
        Managers.Sound.Play("Sounds/BGM/cuttomainBGM", 1, Define.Sound.BGM);
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
