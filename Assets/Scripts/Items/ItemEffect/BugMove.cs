using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMove : MonoBehaviour
{
    public float moveSpeed = 2f;  // 이동 속도
    public float minX = -7f;      // 이동 범위 최소 x 값
    public float maxX = 7f;       // 이동 범위 최대 x 값

    private float startX;

    void Start()
    {
        // 객체의 초기 x 위치 저장
        startX = transform.position.x;
    }

    void Update()
    {
        float newX = Mathf.PingPong(Time.time * moveSpeed, maxX - minX) + minX;

        // 새로운 x 값을 사용하여 객체 위치 변경
        transform.position = new Vector2(newX, transform.position.y);
    }


}
