using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate float InterpolatedFunction(float x);

public class Algorithm
{
    public static InterpolatedFunction NewtonPolynomial(params Vector2[] points)
    {
        if (points.Length < 2)
        {
            throw new ArgumentException($"뉴턴 다항식 보간을 위해서는 최소 2개의 Point가 필요합니다. Point 개수: {points.Length}개");
        }

        int n = points.Length; // 데이터 개수 == 보간하려는 다항함수 식의 계수 개수
        float[,] dividedDifferenceTable = new float[n, n]; // 뉴턴 분할차분표

        // 0번째 열에 y좌표(함수값) 복사
        for (int i = 0; i < n; i++)
        {
            dividedDifferenceTable[i, 0] = points[i].y;
        }

        // 분할 차분 값들을 이전 분할 차분 값으로부터 순차적으로 구함
        for (int j = 1; j < n; j++)
        {
            for (int i = 0; i < n - j; i++)
            {
                dividedDifferenceTable[i, j] = (dividedDifferenceTable[i + 1, j - 1] - dividedDifferenceTable[i, j - 1]) / (points[i + j].x - points[i].x);
            }
        }

        // 보간하려는 다항함수의 계수: 분할차분표의 0번째 행
        float[] coef = new float[n];
        for (int i = 0; i < n; i++)
        {
            coef[i] = dividedDifferenceTable[0, i];
        }

        // 필요한 데이터의 x값(마지막 x값은 필요 없음)
        float[] x_points = new float[n - 1];
        for (int i = 0; i < n - 1; i++)
        {
            x_points[i] = points[i].x;
        }

        // 기존 dividedDifferenceTable과 points 배열 변수를 사용해도 되지만 아래 Interpolate() 로컬 함수에서 변수 캡쳐 시
        // 필요없는 값들까지 유지되기 떄문에 위 coef와 x_points 배열을 새로 생성해 필요한 값만 할당한 후 이 배열들을 캡쳐시켜
        // 기존에 뉴턴 분할차분표에 있던 필요없는 값들을 갈비지 컬렉터가 수집하도록 하기 위함임.
        // 즉 메모리 절약을 위해서 새롭게 배열을 할당함. 자세한 내용은 C# 클로저 개념을 참조.
        float Interpolate(float x)
        {
            float functionValue = 0f; // 반환할 함수값
            float newtonBasisPoly = 1f; // 뉴턴 기반 다항식

            for (int i = 0; i < coef.Length; i++)
            {
                functionValue += coef[i] * newtonBasisPoly;
                if (i < x_points.Length) // x_points의 범위를 벗어나지 않도록 조건 추가
                {
                    newtonBasisPoly *= x - x_points[i]; // 누적곱을 사용함
                }

            }

            return functionValue;
        }

        // 위 로컬함수 Interpolate(float)를 대리자 인스턴스에 추가후 반환함.
        return new InterpolatedFunction(Interpolate);
    }
}

