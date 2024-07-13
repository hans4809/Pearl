using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerEx : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    public Camera MainCamera { get { return _mainCamera; } private set => _mainCamera = value; }

    [SerializeField] float _maxCameraSize = 13f;
    public float MaxCameraSize { get => _maxCameraSize; private set => _maxCameraSize = value; }

    [SerializeField] float _minCameraSize = 5f;
    public float MinCameraSize { get => _minCameraSize; private set => _minCameraSize = value; }
    public IEnumerator CameraZoomIn(float ZoomTimer)
    {
        float elapsedTime = 0f;
        MainCamera.orthographicSize = MaxCameraSize;

        while (MainCamera.orthographicSize > MinCameraSize)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / ZoomTimer);
            float cameraSize = Mathf.Lerp(MaxCameraSize, MinCameraSize, alpha);
            MainCamera.orthographicSize = cameraSize;
            yield return null;
        }

        MainCamera.orthographicSize = MinCameraSize;
    }

    private void Start()
    {
        if(MainCamera == null)
        MainCamera = Camera.main;
        StartCoroutine(CameraZoomIn(3f));
    }
}
