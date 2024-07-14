using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Item : MonoBehaviour
{
    //public Vector2 itemLocation;
    public ItemEnum itemType;

    public int pearlAmount = 1;
    public int kingPearlAmount = 10;
    public int bugAmount = -5;
    public int netAmount = 5;
    public int timerPlusAmount = 1;
    public int timerMinusAmount = -1;

    public float delayTimeToCheckInBox = 0.1f;

    [SerializeField] GameScene _gameScene;
    public GameScene Scene { get => _gameScene; set=> _gameScene = value; }

    [SerializeField] UI_GameScene uI_GameScene;
    public UI_GameScene UI_Game { get => uI_GameScene; set => uI_GameScene = value; }

    public void UseItemPlayer1()
    {
        if (Managers.Game.GameState != EGameState.Playing)
            return;

        var gameScene = Managers.Scene.CurrentScene as GameScene;
        switch (itemType)
        {
            case ItemEnum.PEARL:
                Managers.Score.player1Score += pearlAmount;
                Managers.Game.Player1Score += pearlAmount;
                Managers.Sound.Play("SFX/getitem", 0.5f);
                break;
            case ItemEnum.KING_PEARL:
                Managers.Score.player1Score += kingPearlAmount;
                Managers.Game.Player1Score += kingPearlAmount;
                Managers.Item.KingPearl -= 1;
                Managers.Sound.Play("SFX/getitem", 0.5f);
                break;
            case ItemEnum.BUG:
                Managers.Score.player1Score += bugAmount;
                Managers.Game.Player1Score += bugAmount;
                Managers.Game.Players[0].GetComponent<CharacterControllerEx>().State = Define.State.Bad;
                Managers.Sound.Play("SFX/getitem", 0.5f);
                break;
            case ItemEnum.NET:
                Managers.Score.player1Score += netAmount;
                Managers.Score.player2Score -= netAmount;
                Managers.Game.Player1Score += netAmount;
                Managers.Game.Player2Score -= netAmount;
                Managers.Game.Players[1].GetComponent<CharacterControllerEx>().State = Define.State.Damaged;
                Managers.Sound.Play("SFX/net", 1f);
                break;
            case ItemEnum.TIME_PLUS:

                if (Scene != null)
                {
                    StartCoroutine((Scene.SceneUI as UI_GameScene).OnChangedTimer(10));
                    Scene.GameTimer += timerPlusAmount;
                }
                Managers.Sound.Play("SFX/getitem", 0.5f);
                break;
            case ItemEnum.TIME_MINUS:
                Managers.Game.Players[0].GetComponent<CharacterControllerEx>().State = Define.State.Bad;

                if (Scene != null)
                {
                    StartCoroutine((Scene.SceneUI as UI_GameScene).OnChangedTimer(-10));
                    Scene.GameTimer += timerMinusAmount;
                }
                Managers.Sound.Play("SFX/getitem", 0.5f);
                break;
            case ItemEnum.BOMB:
                Managers.Game.Players[1].GetComponent<CharacterControllerEx>().State = Define.State.Airborne;
                Managers.Sound.Play("SFX/boom", 0.75f);
                break;
            case ItemEnum.BROKEN_PEARL:
                Managers.Game.Players[0].GetComponent<CharacterControllerEx>().State = Define.State.Airborne;
                Managers.Game.Player1Score -= 1;
                break;
        }
        //Destroy(gameObject);
        Managers.Resource.Destroy(gameObject);
        //Debug.Log("Player1:   "+Managers.Score.player1Score);
    }

    public void UseItemPlayer2() // 분명 더 좋은 방법이 있는데......포인터 못 써서 이렇게 따로 만든다...
    {
        if (Managers.Game.GameState != EGameState.Playing)
            return;

        if (Scene == null) Scene = Managers.Scene.CurrentScene as GameScene;
        if (UI_Game == null && Scene != null) UI_Game = Scene.SceneUI as UI_GameScene;
        switch (itemType)
        {
            case ItemEnum.PEARL:
                Managers.Score.player2Score += pearlAmount;
                Managers.Game.Player2Score += pearlAmount;
                Managers.Sound.Play("SFX/getitem");
                break;
            case ItemEnum.KING_PEARL:
                Managers.Score.player2Score += kingPearlAmount;
                Managers.Game.Player2Score += kingPearlAmount;
                Managers.Item.KingPearl -= 1;
                Managers.Sound.Play("SFX/getitem");
                break;
            case ItemEnum.BUG:
                Managers.Game.Players[1].GetComponent<CharacterControllerEx>().State = Define.State.Bad;
                Managers.Score.player2Score += bugAmount;
                Managers.Game.Player2Score += bugAmount;
                Managers.Sound.Play("SFX/getitem");
                break;
            case ItemEnum.NET:
                Managers.Score.player2Score += netAmount;
                Managers.Score.player1Score -= netAmount;
                Managers.Game.Player2Score += netAmount;
                Managers.Game.Player1Score -= netAmount;
                Managers.Game.Players[0].GetComponent<CharacterControllerEx>().State = Define.State.Damaged;
                Managers.Sound.Play("SFX/net");
                break;
            case ItemEnum.TIME_PLUS:
                Managers.Time.counter += timerPlusAmount;
                if (Scene != null)
                {
                    StartCoroutine((Scene.SceneUI as UI_GameScene).OnChangedTimer(+10));
                    Scene.GameTimer += timerPlusAmount;
                }
                Managers.Sound.Play("SFX/getitem");
                break;
            case ItemEnum.TIME_MINUS:
                Managers.Game.Players[1].GetComponent<CharacterControllerEx>().State = Define.State.Bad;
                if (Scene != null)
                {
                    StartCoroutine((Scene.SceneUI as UI_GameScene).OnChangedTimer(-10));
                    Scene.GameTimer += timerMinusAmount;
                }
                Managers.Sound.Play("SFX/getitem");
                break;
            case ItemEnum.BOMB:
                Managers.Game.Players[0].GetComponent<CharacterControllerEx>().State = Define.State.Airborne;
                Managers.Sound.Play("SFX/boom");
                break;
            case ItemEnum.BROKEN_PEARL:
                Managers.Game.Players[1].GetComponent<CharacterControllerEx>().State = Define.State.Airborne;
                Managers.Game.Player1Score -= 1;
                break;

        }
        //Destroy(gameObject);
        Managers.Resource.Destroy(gameObject);
        //Debug.Log("Player2:   " + Managers.Score.player2Score);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Managers.Game.GameState != EGameState.Playing)
            return;

        if (other.CompareTag("Player1"))
        {
            UseItemPlayer1();
        }
        else if (other.CompareTag("Player2"))
        {
            UseItemPlayer2();
        }
    }

    private void Start()
    {
        if (Scene == null) Scene = Managers.Scene.CurrentScene as GameScene;
        if (UI_Game == null && Scene != null) UI_Game = Scene.SceneUI as UI_GameScene;
    }
}
