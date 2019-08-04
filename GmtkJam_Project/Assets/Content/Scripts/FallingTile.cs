using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{

    public float lowestPosition = 20;
    public float _startDelay = 1f;

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

    private BoxCollider _collider;

    private void OnTriggerExit(Collider other)
    {
        if (!destroyed)
        {
            if (other.GetComponent<PlayerMovement>())
            {
                if (other.GetComponent<PlayerMovement>().IsAllowingInput())
                {
                    DestroyTile();
                }
            }
        }
    }

    void DestroyTile()
    {
        destroyed = true;


        _Move = transform.DOMove(transform.position + Vector3.down * lowestPosition, MoveOutDuration).SetDelay(_startDelay);
        _Move.SetEase(MoveOutEase);

        _Scale = transform.DOScale(Vector3.zero, ScaleOutDuration).SetDelay(_startDelay);
        _Scale.SetEase(ScaleOutEase);

        _Move.OnComplete(() => { _collider.enabled = false; });

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
        _delay = Random.Range(1f, 3f);
        startPos = new Vector3() + transform.position;
        startScale = new Vector3() + transform.lossyScale;

        _collider = GetComponent<BoxCollider>();
        KillZone.Instance().BindOnDieEvent(Rise);
    }

    private void OnTweenCompleted()
    {
        if (!_Move.IsPlaying() && !_Scale.IsPlaying())
        {
            _collider.enabled = true;
            destroyed = false;
            StartRise = false;
        }
    }
}
