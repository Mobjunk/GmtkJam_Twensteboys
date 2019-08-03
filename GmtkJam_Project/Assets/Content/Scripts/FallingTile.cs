using DG.Tweening;
using UnityEngine;

public class FallingTile : MonoBehaviour
{

    public float lowestPosition = 20;
    [SerializeField] private float MoveDuration = 10; // in sec
    [SerializeField] private Ease MoveEase;

    [SerializeField] private float ScaleDuration = 5;
    [SerializeField] private Ease ScaleEase;

    bool destroyed = false;

    private Tween _Move;

    private Tween _Scale;

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

        float randomDelay = Random.Range(.1f, .5f);
        _Move.SetDelay(randomDelay).PlayForward();
        _Scale.SetDelay(randomDelay).PlayForward();
    }

    void Rise()
    {
        Random.InitState(Mathf.CeilToInt(transform.position.x + transform.position.y + Time.deltaTime + Time.timeSinceLevelLoad));
        float randomDelay = Random.Range(.5f, 5f);

        _Move.SetDelay(randomDelay).PlayBackwards();
        _Scale.SetDelay(randomDelay).PlayBackwards();
    }

    private void Start()
    {
        _Move = transform.DOMove(transform.position + Vector3.down * lowestPosition, MoveDuration).Pause();
        _Move.OnComplete(OnTweenCompleted).SetAutoKill(false);
        _Move.SetEase(MoveEase);

        _Scale = transform.DOScale(Vector3.zero, ScaleDuration).Pause();
        _Scale.OnComplete(OnTweenCompleted).SetAutoKill(false);
        _Scale.SetEase(ScaleEase);

        DOTween.SetTweensCapacity(500, 50);
        KillZone.Instance().BindOnDieEvent(Rise);
    }

    private void OnTweenCompleted()
    {
        //because it was destroyed we know it got reversed
        if (destroyed)
        {
            if (_Move.IsComplete() && _Scale.IsComplete())
            {
                destroyed = false;
            }
        }
    }
}
