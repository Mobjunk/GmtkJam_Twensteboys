using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonEffect : MonoBehaviour
{

    [SerializeField] private float ScaleInDuration = 1.2f;
    [SerializeField] private Ease ScaleInEase = Ease.OutCirc;
    [SerializeField] private float ScaleOutDuration = 1.2f;
    [SerializeField] private Ease ScaleOutEase = Ease.OutCirc;

    public MainMenuMethods menuMethods;
    public MainMenuTogetterEffect[] togetterEffect;

    Tween Scale;

    private void OnEnable()
    {
        Scale = transform.DOScale(Vector3.one, ScaleInDuration);
        Scale.SetEase(ScaleInEase);

        Scale.PlayForward();

    }

    public void ButtonClickedEffectQuit()
    {
        togeter();
        Scale = transform.DOScale(Vector3.zero, ScaleOutDuration);
        Scale.OnComplete(menuMethods.Quit);
        Scale.SetEase(ScaleOutEase);

        Scale.Play();
    }

    public void ButtonClickedEffectLoadscene()
    {
        togeter();
        Scale = transform.DOScale(Vector3.zero, ScaleOutDuration);
        Scale.OnComplete(menuMethods.LoadScene);
        Scale.SetEase(ScaleOutEase);

        Scale.Play();
    }

    public void ButtonClickedEffectswitchPanel()
    {
        togeter();
        Scale = transform.DOScale(Vector3.zero, ScaleOutDuration);
        Scale.OnComplete(menuMethods.SwitchPanels);
        Scale.SetEase(ScaleOutEase);

        Scale.Play();
    }

    void togeter()
    {
        foreach (var item in togetterEffect)
        {
            item.Togeter();
        }
    }
}
