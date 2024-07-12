using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlSpawner : ItemSpawner
{
    public Transform[] initialPearlSpawnPoints;
    public GameObject item;


    private void Awake()
    {
        foreach (Transform spawnPoint in initialPearlSpawnPoints)
        {
            Instantiate(item, spawnPoint.position, spawnPoint.rotation);
        }
    }
    /*
    private void Update()
    {
        var gameScene = Managers.Scene.CurrentScene as GameScene;
        if (gameScene != null && ameScene.GameTimer < 59)
        {
            for each
        }
    }
    */



}
