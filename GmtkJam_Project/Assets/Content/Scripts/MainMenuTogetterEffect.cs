using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuTogetterEffect : MonoBehaviour
{
    [SerializeField] private float ScaleOutDuration = 1.2f;
    [SerializeField] private Ease ScaleOutEase = Ease.OutCirc;

    Tween Scale;

    public void Togeter()
    {
        Scale = transform.DOScale(Vector3.zero, ScaleOutDuration);
        Scale.SetEase(ScaleOutEase);

        Scale.PlayForward();
    }
}
