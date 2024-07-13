using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMove : MonoBehaviour
{
    public float moveSpeed = 2f;  // 이동 속도
    public float minX = -6f;      // 이동 범위 최소 x 값
    public float maxX = 6f;       // 이동 범위 최대 x 값
    private float moveDirection = 1;


    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime * moveDirection;
        transform.position = new Vector2(transform.position.x + moveX, transform.position.y);
        if (Mathf.Abs(transform.position.x) > maxX)
        {
            transform.position = new Vector2(maxX*moveDirection, transform.position.y);
            moveDirection = -moveDirection;
        }
    }


}
