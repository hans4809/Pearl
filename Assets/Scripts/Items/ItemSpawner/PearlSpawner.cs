using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PearlSpawner : ItemSpawner
{
    public Transform[] initialPearlSpawnPoints;
    public GameObject item;
    public bool started = false;
    public float gap = 1f;
    public float distanceFromCenterPearl = 1f;

    public float fastSpawnTime = 1f;


    private void Update()
    {

        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // 마지막 생성 시간 갱신
            lastSpawnTime = Time.time;
            // 생성 주기를 랜덤으로 변경
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            // 아이템 생성 실행
            Spawn();
        }




    }
    protected override void Spawn()
    {
        var gameScene = Managers.Scene.CurrentScene as GameScene;
        if (gameScene != null && gameScene.GameTimer < 59)
        {
            if (!started)
            {
                foreach (Transform spawnPoint in initialPearlSpawnPoints)
                {
                    Instantiate(item, spawnPoint.position, spawnPoint.rotation);

                }
            }
            else
            {
                // ??? 기획 이해 안됨
                foreach (Transform spawnPoint in initialPearlSpawnPoints)
                {
                    Instantiate(item, spawnPoint.position, spawnPoint.rotation);

                }
            }
        }

        if (gameScene != null && gameScene.GameTimer < 59)
        {
            int randomChoice = Random.Range(0, 2);
            Vector2 center = GetRandomPointInBox();
            if (randomChoice == 0)
            {
                Vector2 pos1 = new Vector2(center.x - 1 * gap, center.y);
                Vector2 pos2 = new Vector2(center.x + 1 * gap, center.y);
                Vector2 pos3 = new Vector2(center.x + 2 * gap, center.y);
                Vector2 pos4 = new Vector2(center.x + 3 * gap, center.y);

                GameObject pearl1 = Instantiate(item, pos1, Quaternion.identity);
                GameObject pearl2 = Instantiate(item, pos2, Quaternion.identity);
                GameObject pearl3 = Instantiate(item, pos3, Quaternion.identity);
                GameObject pearl4 = Instantiate(item, pos4, Quaternion.identity);
            }
            else
            {
                Vector2 pos1 = new Vector2(center.x, center.y - 1 * gap);
                Vector2 pos2 = new Vector2(center.x, center.y + 1 * gap);
                Vector2 pos3 = new Vector2(center.x, center.y + 2 * gap);
                Vector2 pos4 = new Vector2(center.x, center.y + 3 * gap);

                GameObject pearl1 = Instantiate(item, pos1, Quaternion.identity);
                GameObject pearl2 = Instantiate(item, pos2, Quaternion.identity);
                GameObject pearl3 = Instantiate(item, pos3, Quaternion.identity);
                GameObject pearl4 = Instantiate(item, pos4, Quaternion.identity);
            }
        }

        if (gameScene != null && gameScene.GameTimer < 39)
        {
            Vector2 center = GetRandomPointInBox();
            Vector2 pos1 = new Vector2(center.x - 1 * gap, center.y - 1 * gap);
            Vector2 pos2 = new Vector2(center.x + 1 * gap, center.y - 1 * gap);
            Vector2 pos3 = new Vector2(center.x + 2 * gap, center.y - 1 * gap);
            Vector2 pos4 = new Vector2(center.x + 3 * gap, center.y - 1 * gap);
            Vector2 pos5 = new Vector2(center.x - 1 * gap, center.y - 1 * gap);
            Vector2 pos6 = new Vector2(center.x + 1 * gap, center.y - 1 * gap);
            Vector2 pos7 = new Vector2(center.x + 2 * gap, center.y - 1 * gap);
            Vector2 pos8 = new Vector2(center.x + 3 * gap, center.y - 1 * gap);

            GameObject pearl1 = Instantiate(item, pos1, Quaternion.identity);
            GameObject pearl2 = Instantiate(item, pos2, Quaternion.identity);
            GameObject pearl3 = Instantiate(item, pos3, Quaternion.identity);
            GameObject pearl4 = Instantiate(item, pos4, Quaternion.identity);
            GameObject pearl5 = Instantiate(item, pos5, Quaternion.identity);
            GameObject pearl6 = Instantiate(item, pos6, Quaternion.identity);
            GameObject pearl7 = Instantiate(item, pos7, Quaternion.identity);
            GameObject pearl8 = Instantiate(item, pos8, Quaternion.identity);
        }

        if (gameScene != null && gameScene.GameTimer < 19)
        {
            timeBetSpawnMax -= fastSpawnTime;
            timeBetSpawnMax -= fastSpawnTime;

            int numberOfPoints = 8;
            float angleIncrement = 360f / numberOfPoints;

            for (int i = 0; i < numberOfPoints; i++)
            {
                // 각도를 라디안으로 변환
                float angleInRadians = (i * angleIncrement) * Mathf.Deg2Rad;
                Vector2 center = GetRandomPointInBox();
                // 새로운 좌표 계산
                Vector2 newPoint = new Vector2(
                    center.x + distanceFromCenterPearl * Mathf.Cos(angleInRadians),
                    center.y + distanceFromCenterPearl * Mathf.Sin(angleInRadians)
                );

            }
        }
    }
}

