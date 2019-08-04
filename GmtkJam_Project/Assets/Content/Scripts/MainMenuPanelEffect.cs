using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuPanelEffect : MonoBehaviour
{
    [SerializeField] private float ScaleInDuration = 1.2f;
    [SerializeField] private Ease ScaleInEase = Ease.OutCirc;

    Tween Scale;

    private void Awake()
    {
        Scale = transform.DOScale(Vector3.one, ScaleInDuration);
        Scale.SetEase(ScaleInEase);

        Scale.PlayForward();
    }
}
