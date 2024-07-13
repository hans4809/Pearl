using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManagerEx
{
    GameObject[] _players;
    public GameObject[] GetPlayers() { return _players; }
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    HashSet<GameObject> _items = new HashSet<GameObject>();
    public Action<int> OnSpawnEvent;
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
                //if (_players[])
                //_player = go;
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
        Managers.UI.ShowSceneUI<UI_GameOverScene>();
    }
}
