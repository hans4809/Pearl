using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // public Text counterText; // UI 텍스트 컴포넌트

    void Start()
    {
        // 코루틴 시작
        StartCoroutine(StartCounter());
    }

    // 코루틴 정의
    IEnumerator StartCounter()
    {
        while (true)
        {
            Managers.Time.counter--; // 카운터 값 증가
            // counterText.text = counter; // UI 텍스트 업데이트
            Debug.Log(Managers.Time.counter);

            yield return new WaitForSeconds(1f); // 1초 지연
        }
    }
}
