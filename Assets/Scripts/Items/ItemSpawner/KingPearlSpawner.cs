using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class KingPearlSpawner : ItemSpawner
{
    public Transform[] kingPearlSpawnPoints1;
    public Transform[] kingPearlSpawnPoints2;
    public float startSpawnKingPearlTime = 10f;
    public float increaseSpawnKingPearlTime = 35f;
    //private Vector2[] spawnPositions;
    public GameObject item; // 생성할 아이템들
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
        // 현재 시점이 마지막 생성 시점에서 생성 주기 이상 지남
        // && 플레이어 캐릭터가 존재함
        // if (타이머의 시간이 해당 제한 시간보다 지났을 떄)
        // startSpawnHourGlassTime
        if (Time.time >= lastSpawnTime + timeBetSpawn && Managers.Item.KingPearl == 0 /*&& playerTransform != null*/)
        {
            // 마지막 생성 시간 갱신
            lastSpawnTime = Time.time;
            // 생성 주기를 랜덤으로 변경
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            // 아이템 생성 실행

            if (Scene != null && 60 - Scene.GameTimer > startSpawnKingPearlTime)
                Spawn();
            
        }
    }

    protected override void Spawn()
    {
        /*
        spawnPositions = null;
        for (int i = 0; i < kingPearlSpawnPoints.Length; i++)
        {
            spawnPositions[i] = kingPearlSpawnPoints[i].transform.position;
        }
        */

        // 바닥에서 0.5만큼 위로 올리기
        // spawnPosition += Vector3.up * 0.5f;

        // 아이템 중 하나를 무작위로 골라 랜덤 위치에 생성
        //GameObject selectedItem = items[Random.Range(0, items.Length)];

        /*
        for (int i = 0; i < kingPearlSpawnPoints.Length; i++)
        {
            Debug.Log(kingPearlSpawnPoints[i]);
            GameObject kingPearl = Instantiate(selectedItem, kingPearlSpawnPoints[i].position, Quaternion.identity);
        }*/

        if (Scene != null && 60 - Scene.GameTimer < increaseSpawnKingPearlTime)
        {
            foreach (Transform spawnPoint in kingPearlSpawnPoints1)
            {
                //Instantiate(item, spawnPoint.position, spawnPoint.rotation);
                Managers.Resource.InstantiateItem("Item/KingPearl/KingPearl", spawnPoint.position, spawnPoint.rotation);
            }
            Managers.Item.KingPearl = kingPearlSpawnPoints1.Length;
        }
        else
        {
            foreach (Transform spawnPoint in kingPearlSpawnPoints2)
            {
                //Instantiate(item, spawnPoint.position, spawnPoint.rotation);
                Managers.Resource.InstantiateItem("Item/KingPearl/KingPearl", spawnPoint.position, spawnPoint.rotation);
            }
            Managers.Item.KingPearl = kingPearlSpawnPoints2.Length;
        }
        

    }
}
