using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTwist : MonoBehaviour
{
    public float rotationSpeed = 500f;   // 회전 속도 (각도/초)
    public float rotationRange = 30f;   // 회전 범위 (좌우 최대 회전 각도)

    private float startRotation;

    void Start()
    {
        startRotation = transform.rotation.eulerAngles.z;  // 시작 시 회전 각도 저장
    }

    void Update()
    {
        // Mathf.Sin 함수를 사용하여 회전 각도 계산
        float angle = startRotation + rotationRange * Mathf.Sin(Time.time * rotationSpeed * Mathf.Deg2Rad);

        // 회전 각도를 적용하여 객체 회전
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
