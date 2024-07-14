using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Setting : UI_Popup
{
    [SerializeField] private Button _mainButton;
    [SerializeField] private Button _returnButton;

    public Button MainButton { get => _mainButton; private set => _mainButton = value; }
    public Button ReturnButton { get => _returnButton; private set => _returnButton = value; }

    public override void Init()
    {
        base.Init();
        MainButton.gameObject.AddUIEvent(OnClickMainButton);
        ReturnButton.gameObject.AddUIEvent(OnClickReturnButton);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void OnClickMainButton(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainScene);
    }

    public void OnClickReturnButton(PointerEventData eventData)
    {
        Time.timeScale = 1;
        Managers.UI.CloseAllPopUPUI();
        Managers.Game.GameState = EGameState.Playing;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 1;
            Managers.UI.CloseAllPopUPUI();
            Managers.Game.GameState = EGameState.Playing;
        }
    }
}
