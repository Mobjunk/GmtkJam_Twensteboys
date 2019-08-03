using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CinematicCamera : MonoBehaviour
{
    [SerializeField] private new Transform camera;
    [SerializeField] private Vector3 cameraStartPosition, cameraEndPosition;

    [SerializeField] private float time = 2f, delay = 0.5f;
    [SerializeField] private Ease cinematicEasing = Ease.InOutCubic;

    private void Start()
    {
        StartCinematic();
    }

    private void StartCinematic()
    {
        camera.position = cameraStartPosition;

        camera.DOMove(cameraEndPosition, time)
            .SetEase(cinematicEasing)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                Debug.Log("Cinematic camera is done");
            });
    }
}