using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{

    public float lowestPosition = 20;

    [SerializeField] private float MoveOutDuration = 1.3f;
    [SerializeField] private Ease MoveOutEase = Ease.OutCirc;
    [SerializeField] private float MoveInDuration = 1.3f; // in sec
    [SerializeField] private Ease MoveInEase = Ease.OutCirc;

    [SerializeField] private float ScaleOutDuration = 1.2f;
    [SerializeField] private Ease ScaleOutEase = Ease.OutCirc;
    [SerializeField] private float ScaleInDuration = 1.2f;
    [SerializeField] private Ease ScaleInEase = Ease.OutCirc;

    private Vector3 startPos;
    private Vector3 startScale;

    bool destroyed = false;

    private Tween _Move;
    private Tween _Scale;

    [SerializeField]
    private float _delay;


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            if (!destroyed)
            {
                DestroyTile();
            }
        }
    }

    void DestroyTile()
    {
        destroyed = true;


        _Move = transform.DOMove(transform.position + Vector3.down * lowestPosition, MoveOutDuration).Pause();
        _Move.SetEase(MoveOutEase);

        _Scale = transform.DOScale(Vector3.zero, ScaleOutDuration).Pause();
        _Scale.SetEase(ScaleOutEase);

        _Move.PlayForward();
        _Scale.PlayForward();
    }

    private bool StartRise = false;

    void Rise()
    {
        if (!StartRise && destroyed)
        {
            StartRise = true;
            foreach (Tween tween in new List<Tween>() { _Move, _Scale })
            {
                if (tween == null)
                {
                    continue;
                }

                if (tween.active)
                {
                    tween.Kill(true);
                }
            }

            _Move = transform.DOMove(startPos, MoveInDuration);
            _Move.OnComplete(OnTweenCompleted);
            _Move.SetEase(MoveInEase).SetDelay(_delay);
            _Move.Play();

            _Scale = transform.DOScale(startScale, ScaleInDuration);
            _Scale.OnComplete(OnTweenCompleted);
            _Scale.SetEase(ScaleInEase).SetDelay(_delay);
            _Scale.Play();

        }
    }


    private void Start()
    {
        _delay = Random.Range(0.1f, 2f);
        startPos = new Vector3() + transform.position;
        startScale = new Vector3() + transform.lossyScale;

        KillZone.Instance().BindOnDieEvent(Rise);
    }

    private void OnTweenCompleted()
    {
        if (_Move.IsComplete() && _Scale.IsComplete())
        {
            destroyed = false;
            StartRise = false;
        }
    }
}
