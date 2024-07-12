using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetSpawner : ItemSpawner
{
    public float maxDistance = 5f; // 플레이어 위치로부터 아이템이 배치될 최대 반경
    public GameObject item; // 생성할 아이템
    public int gapBetPlayerScore = 10;
    private void Update()
    {
        if (Mathf.Abs(Managers.Score.player1Score - Managers.Score.player2Score) > gapBetPlayerScore)
        {
            if (!Managers.Item.netHasBeenSpawned)
                Spawn();
            else
            {
                var gameScene = Managers.Scene.CurrentScene as GameScene;
                if (gameScene != null && (60f - gameScene.GameTimer) > Managers.Item.netSpawnedTime)
                    Spawn();
            }
        }
    }

    protected override void Spawn()
    {
        //if ()
        if (Managers.Score.player1Score < Managers.Score.player2Score)
            playerTransform = GameObject.FindGameObjectWithTag("Player1").transform;
        else
            playerTransform = GameObject.FindGameObjectWithTag("Player2").transform;
        Vector2 spawnPosition =
            GetRandomPointInRange(playerTransform.position, maxDistance);

        // 바닥에서 0.5만큼 위로 올리기
        // spawnPosition += Vector3.up * 0.5f;

        // 아이템 중 하나를 무작위로 골라 랜덤 위치에 생성
        //GameObject selectedItem = items[Random.Range(0, items.Length)];



        GameObject net = Instantiate(item, spawnPosition, Quaternion.identity);

        Managers.Item.netHasBeenSpawned = true;
        var gameScene = Managers.Scene.CurrentScene as GameScene;
        Managers.Item.netSpawnedTime = 60f - gameScene.GameTimer;

    }
}
