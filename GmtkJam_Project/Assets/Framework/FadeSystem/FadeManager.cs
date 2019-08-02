using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class FadeManager : Singleton<FadeManager>
{
    [SerializeField] private Image fadeImage = null;

    public void FadeWithAction(UnityAction action)
    {
        StartCoroutine(CoFadeWithAction(action));
    }

    private IEnumerator CoFadeWithAction(UnityAction action)
    {
        fadeImage.DOBlendableColor(new Color(1, 1, 1, 1), 0.25f);

        yield return new WaitForSeconds(0.25f);
        action?.Invoke();

        fadeImage.DOBlendableColor(new Color(1, 1, 1, 0), 0.25f);
    }
}