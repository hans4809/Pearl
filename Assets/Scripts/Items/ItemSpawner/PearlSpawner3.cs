using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlSpawner3 : ItemSpawner
{
    public float startPearl3 = 40f;
    public int numOfPearls = 16;
    public float distanceFromCenterPearl = 1f;
    private  bool showOnce = false;

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

        if (playerTransform == null)
            playerTransform = Managers.Game.Players[RecommendedPlayerIndex].transform;
        // 현재 시점이 마지막 생성 시점에서 생성 주기 이상 지남
        // && 플레이어 캐릭터가 존재함
        // if (타이머의 시간이 해당 제한 시간보다 지났을 떄)
        // startSpawnHourGlassTime

        if (Scene != null && (60f - Scene.GameTimer) > startPearl3 && !showOnce)
        {
            if (Time.time >= lastSpawnTime + timeBetSpawn/*&& playerTransform != null*/)
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

        //Vector2 center = GetRandomPointInBox();
        Vector2 center = Vector2.zero;

        float angleIncrement = 360f / numOfPearls;

        for (int i = 0; i < numOfPearls; i++)
        {
            // 각도를 라디안으로 변환
            float angleInRadians = (i * angleIncrement) * Mathf.Deg2Rad;

            // 새로운 좌표 계산
            Vector2 newPoint = new Vector2(
                center.x + distanceFromCenterPearl * Mathf.Cos(angleInRadians),
                center.y + distanceFromCenterPearl * Mathf.Sin(angleInRadians)
            );

            //GameObject bomb = Instantiate(item, newPoint, Quaternion.identity);
            GameObject bomb = Managers.Resource.InstantiateItem("Item/Pearl/Pearl", newPoint, Quaternion.identity);
            showOnce = true;
        }
    }
}
