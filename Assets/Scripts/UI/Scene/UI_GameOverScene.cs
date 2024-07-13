using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOverScene : UI_Scene
{
    [SerializeField] Image _winnerIMG;
    public Image WinnerIMG { get => _winnerIMG; set => _winnerIMG = value; }

    [SerializeField] TextMeshProUGUI _gameOverSceneText;
    public TextMeshProUGUI GameOverSceneText { get => _gameOverSceneText; private set => _gameOverSceneText = value; }
    // Start is called before the first frame update
    void Start()
    {
        Managers.Sound.Play("Sounds/SFX/GameEnding");
        WinnerIMG.SetNativeSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Managers.Scene.LoadScene(Define.Scene.MainScene);
            var asyncOper = Managers.Scene.AsyncLoadSceneOper;
            if (asyncOper != null)
            {
                if (asyncOper.progress >= 0.9f)
                    asyncOper.allowSceneActivation = true;
            }
        }
        
    }

    public void Blink()
    {
        StartCoroutine(Managers.UI.BlinkText(GameOverSceneText, GameOverSceneText.text));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
