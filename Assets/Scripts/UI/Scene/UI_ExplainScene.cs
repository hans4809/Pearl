using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ExplainScene : UI_Scene
{
    [SerializeField] Button _startButton;
    public Button StartButton { get => _startButton; private set => _startButton = value; }
    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder += 1;
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
