using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager
{
    public int counter { get; set; }

    /*
    private void Start() // 나중에 게임 시작 하는 순간으로 변경
    {
        startTime = Time.time;
    }

    // 시작 할 때 startTime + endTime

    private void Update()
    {
        currentTime = startTime + endTime - Time.time;

        Debug.Log((int)currentTime);
    }

    public void Clear()
    {
        // 시간 초기화
    }


    */
    public void Clear()
    {
        counter = 60;
    }
}
