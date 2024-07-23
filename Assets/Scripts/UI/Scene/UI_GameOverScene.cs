using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOverScene : UI_Scene
{
    [SerializeField] Image _winnerIMG;
    public Image WinnerIMG { get => _winnerIMG; set => _winnerIMG = value; }
    [SerializeField] Button _mainButton;
    [SerializeField] Button _returnButton;
    // Start is called before the first frame update
    void Start()
    {
        Managers.Sound.Play("Sounds/SFX/GameEnding");
        WinnerIMG.SetNativeSize();
        _mainButton.gameObject.AddUIEvent(OnClickMainButton);
        _returnButton.gameObject.AddUIEvent(OnClickReturnButton);
    }

    public void OnClickMainButton(PointerEventData eventData)
    {
        if (Managers.Scene.AsyncLoadSceneOper != null)
        {
            if (Managers.Scene.AsyncLoadSceneOper.isDone || Managers.Scene.AsyncLoadSceneOper.progress >= 0.9f)
            {
                Managers.Scene.AsyncLoadSceneOper.allowSceneActivation = true;
            }
        }
    }

    public void OnClickReturnButton(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.ExplainScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
    }
}
