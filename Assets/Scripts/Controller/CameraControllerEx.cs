using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerEx : MonoBehaviour
{
    [SerializeField] float _maxCameraSize = 13f;
    public float MaxCameraSize { get => _maxCameraSize; private set => _maxCameraSize = value; }

    [SerializeField] float _minCameraSize = 5f;
    public float MinCameraSize { get => _minCameraSize; private set => _minCameraSize = value; }
    public IEnumerator CameraZoomIn(float ZoomTimer)
    {
        float elapsedTime = 0f;
        Camera.main.orthographicSize = MaxCameraSize;

        while (Camera.main.orthographicSize > MinCameraSize)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / ZoomTimer);
            float cameraSize = Mathf.Lerp(MaxCameraSize, MinCameraSize, alpha);
            Camera.main.orthographicSize = cameraSize;
            yield return null;
        }

        Camera.main.orthographicSize = MinCameraSize;
    }
}
