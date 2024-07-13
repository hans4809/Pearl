using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum EGameState
{
    Start,
    Playing,
    End,
}

public class GameManagerEx
{
    [SerializeField] GameObject[] _players = new GameObject[2];
    public GameObject[] Players { get => _players; private set => _players = value; }
    public GameObject[] GetPlayers() { return _players; }
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    HashSet<GameObject> _items = new HashSet<GameObject>();
    public Action<int> OnSpawnEvent;
    EGameState _gameState = EGameState.End;
    public EGameState GameState { get => _gameState; set => _gameState = value; }

    private int _player1Score;
    public int Player1Score 
    { 
        get => _player1Score; 
        set 
        {
            _player1Score = value;
            var gameScene = Managers.Scene.CurrentScene as GameScene;
            if (gameScene != null)
            {
                var sceneUi = gameScene.SceneUI as UI_GameScene;
                if(sceneUi != null)
                    sceneUi.Player1_ItemText.text = $"{_player1Score}";
            }
                
            if(_player1Score >= _player2Score)
                BestPlayerIndex = 1;
        } 
    }

    private int _player2Score;
    public int Player2Score 
    { 
        get => _player2Score; 
        set 
        { 
            _player2Score = value;
            var gameScene = Managers.Scene.CurrentScene as GameScene;
            if (gameScene != null)
            {
                var sceneUi = gameScene.SceneUI as UI_GameScene;
                if (sceneUi != null)
                    sceneUi.Player2_ItemText.text = $"{_player2Score}";
            }

            if (_player2Score >= _player1Score)
                BestPlayerIndex = 2;
        }
    }

    private int _bestPlayerIndex;
    public int BestPlayerIndex 
    { 
        get => _bestPlayerIndex; 
        set 
        {
            var gameScene = Managers.Scene.CurrentScene as GameScene;
            if (_bestPlayerIndex != value && 1 <= value && value <= 2)
            {
                if (gameScene != null)
                {
                    var sceneUi = gameScene.SceneUI as UI_GameScene;
                    if(sceneUi != null)
                        sceneUi.UpdatePlayerScore(_bestPlayerIndex - 1, value - 1);
                }
                _bestPlayerIndex = value;
            }
        }
    }


    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Enemy:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                Players[go.GetComponent<CharacterMovement>().PlayerIndex - 1] = go;
                break;
        }

        return go;
    }
    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
        {
            BaseItem Item = go.GetComponent<BaseItem>();
            if (Item == null)
                return Define.WorldObject.Unknown;
            return Item.WorldObjectType;
        }

        return bc.WorldObjectType;
    }
    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Enemy:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.WorldObject.Player:
                {
                    //if (_player == go)
                    //    _player = null;
                }
                break;
            case Define.WorldObject.Item:
                {
                    if (_items.Contains(go))
                    {
                        _items.Remove(go);
                    }
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }

    public void GameEnd()
    {
        GameState = EGameState.End;
        //var gameScene = Managers.Scene.CurrentScene;
        //gameScene.Clear();

        Player1Score = 0;
        Player2Score = 0;
        BestPlayerIndex = 0;
    }

    public void GameStart()
    {
        GameState = EGameState.Start;

        if (Managers.Scene.CurrentScene is GameScene)
        {
            for (int i = 0; i < 2; i++)
            {
                Spawn(Define.WorldObject.Player, $"PC/Character{i + 1}");
                Players[i].transform.position = (Managers.Scene.CurrentScene as GameScene).SpawnTransfroms[i].position;
            }
            Managers.Sound.Play("BGM/BGM", Define.Sound.BGM);
            GameState = EGameState.Playing;
        }
    }
}
