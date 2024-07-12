using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_GameOverScene : UI_Scene
{
    [SerializeField] TextMeshProUGUI _gameOverSceneText;
    public TextMeshProUGUI GameOverSceneText { get => _gameOverSceneText; private set => _gameOverSceneText = value; }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Managers.UI.BlinkText(GameOverSceneText));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)) 
            Managers.Scene.LoadScene(Define.Scene.MainScene);
    }
}
