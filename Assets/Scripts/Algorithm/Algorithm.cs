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
            throw new ArgumentException($"���� ���׽� ������ ���ؼ��� �ּ� 2���� Point�� �ʿ��մϴ�. Point ����: {points.Length}��");
        }

        int n = points.Length; // ������ ���� == �����Ϸ��� �����Լ� ���� ��� ����
        float[,] dividedDifferenceTable = new float[n, n]; // ���� ��������ǥ

        // 0��° ���� y��ǥ(�Լ���) ����
        for (int i = 0; i < n; i++)
        {
            dividedDifferenceTable[i, 0] = points[i].y;
        }

        // ���� ���� ������ ���� ���� ���� �����κ��� ���������� ����
        for (int j = 1; j < n; j++)
        {
            for (int i = 0; i < n - j; i++)
            {
                dividedDifferenceTable[i, j] = (dividedDifferenceTable[i + 1, j - 1] - dividedDifferenceTable[i, j - 1]) / (points[i + j].x - points[i].x);
            }
        }

        // �����Ϸ��� �����Լ��� ���: ��������ǥ�� 0��° ��
        float[] coef = new float[n];
        for (int i = 0; i < n; i++)
        {
            coef[i] = dividedDifferenceTable[0, i];
        }

        // �ʿ��� �������� x��(������ x���� �ʿ� ����)
        float[] x_points = new float[n - 1];
        for (int i = 0; i < n - 1; i++)
        {
            x_points[i] = points[i].x;
        }

        // ���� dividedDifferenceTable�� points �迭 ������ ����ص� ������ �Ʒ� Interpolate() ���� �Լ����� ���� ĸ�� ��
        // �ʿ���� ������� �����Ǳ� ������ �� coef�� x_points �迭�� ���� ������ �ʿ��� ���� �Ҵ��� �� �� �迭���� ĸ�Ľ���
        // ������ ���� ��������ǥ�� �ִ� �ʿ���� ������ ������ �÷��Ͱ� �����ϵ��� �ϱ� ������.
        // �� �޸� ������ ���ؼ� ���Ӱ� �迭�� �Ҵ���. �ڼ��� ������ C# Ŭ���� ������ ����.
        float Interpolate(float x)
        {
            float functionValue = 0f; // ��ȯ�� �Լ���
            float newtonBasisPoly = 1f; // ���� ��� ���׽�

            for (int i = 0; i < coef.Length; i++)
            {
                functionValue += coef[i] * newtonBasisPoly;
                if (i < x_points.Length) // x_points�� ������ ����� �ʵ��� ���� �߰�
                {
                    newtonBasisPoly *= x - x_points[i]; // �������� �����
                }

            }

            return functionValue;
        }

        // �� �����Լ� Interpolate(float)�� �븮�� �ν��Ͻ��� �߰��� ��ȯ��.
        return new InterpolatedFunction(Interpolate);
    }
}

