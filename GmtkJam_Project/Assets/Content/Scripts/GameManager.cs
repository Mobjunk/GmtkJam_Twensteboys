using DG.Tweening;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject GetPlayer => _playerPawn;
    public Camera GetCamera => _camera;

    [SerializeField] private GameObject _cameraGameObject;
    [SerializeField] private GameObject _playerPawnObject;
    private GameObject _playerPawn;

    private Camera _camera;
    private PlayerSpawn _playerSpawn;
    private Finish _finish;
    private Tween _CamTween;
    private void Awake()
    {
        _playerSpawn = PlayerSpawn.Instance();
        _finish = Finish.Instance();
        _camera = Instantiate(_cameraGameObject, _finish.transform.position + Vector3.up * 5, _cameraGameObject.transform.rotation).GetComponent<Camera>();
        KillZone.Instance().BindOnDieEvent(SpawnPlayer);

        _CamTween = _camera.transform.DOMove(_playerSpawn.transform.position + Vector3.back * 1.3f + Vector3.up * 5, 4).Play();
        _CamTween.OnComplete(gameStart);
        SpawnPlayer();
    }

    void gameStart()
    {
        _camera.transform.parent = _playerPawn.transform;
        _playerPawn.GetComponent<PlayerMovement>().AllowInput();
    }

    public void SpawnPlayer()
    {
        if (GetPlayer == null)
        {
            _playerPawn = Instantiate(_playerPawnObject, _playerSpawn.GetSpawnLocation(), _playerSpawn.GetSpawnRotation());
            _playerPawn.GetComponent<PlayerMovement>().DenyInput();
        }
        else
        {
            _playerPawn.transform.position = _playerSpawn.GetSpawnLocation();
            _playerPawn.transform.rotation = _playerSpawn.GetSpawnRotation();
        }
    }
}
