using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourGlassSpawner : ItemSpawner
{
    public float startSpawnHourGlassTime;
    public float minDistance = 2f; // 플레이어 위치로부터 아이템이 배치될 최소 반경
    public GameObject[] items; // 생성할 아이템들
    public float lastTimeSpawn = 7f;
    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }
    private void Update()
    {
        if (Managers.Game.GameState != EGameState.Playing)
            return;

        if (playerTransform == null)
            playerTransform = Managers.Game.Players[RecommendedPlayerIndex].transform;
        // 현재 시점이 마지막 생성 시점에서 생성 주기 이상 지남
        // && 플레이어 캐릭터가 존재함
        // if (타이머의 시간이 해당 제한 시간보다 지났을 떄)
        // startSpawnHourGlassTime

        if (Scene != null && (60f - Scene.GameTimer) > startSpawnHourGlassTime && Scene.GameTimer > lastTimeSpawn)
        {
            if (Time.time >= lastSpawnTime + timeBetSpawn && playerTransform != null)
            {
                // 마지막 생성 시간 갱신
                lastSpawnTime = Time.time;
                // 생성 주기를 랜덤으로 변경
                timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
                // 아이템 생성 실행
                Spawn();
            }
        }
    }

    protected override void Spawn()
    {
        //if ()
        
        Vector2 spawnPosition =
            GetRandomPointOutRange(playerTransform.position, minDistance);

        // 바닥에서 0.5만큼 위로 올리기
        // spawnPosition += Vector3.up * 0.5f;

        // 아이템 중 하나를 무작위로 골라 랜덤 위치에 생성
        //GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject selectedItem;
        int choice = Random.Range(0, 2);
        if (choice == 0)
        {
            selectedItem = items[0];
            Managers.Resource.InstantiateItem("Item/HourGlassPlus/HourGlassMinus", spawnPosition, Quaternion.identity);
        }
        else
        {
            selectedItem = items[1];
            Managers.Resource.InstantiateItem("Item/HourGlassPlus/HourGlassPlus", spawnPosition, Quaternion.identity);
        }

        //GameObject hourGlass = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

    }
}
