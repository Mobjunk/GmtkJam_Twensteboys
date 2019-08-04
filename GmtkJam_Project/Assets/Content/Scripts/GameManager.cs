using DG.Tweening;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject GetPlayer => _playerPawn;
    public Camera GetCamera => _camera;

    [SerializeField] private GameObject _cameraGameObject;
    [SerializeField] private GameObject _playerPawnObject;
    private GameObject _playerPawn;
    private PlayerMovement _playerMovement;

    [SerializeField]
    public Vector3 CameraOffset;

    [SerializeField] private Vector3 CameraRotation;


    private Camera _camera;
    private FollowPlayer _followPlayer;
    private PlayerSpawn _playerSpawn;
    private Finish _finish;

    private Vector3 _respawnCamPos;
    private void Awake()
    {
        _playerSpawn = PlayerSpawn.Instance();
        _finish = Finish.Instance();
        _camera = Instantiate(_cameraGameObject, _finish.transform.position + CameraOffset, Quaternion.Euler(CameraRotation)).GetComponent<Camera>();
        _followPlayer = _camera.GetComponent<FollowPlayer>();
        KillZone.Instance().BindOnDieEvent(ShowRespawn);


        SpawnPlayer();

        _playerMovement = _playerPawn.GetComponent<PlayerMovement>();
        _respawnCamPos = RespawnCameraLocation.Instance().transform.position;

        _camera.transform.DOMove(_playerSpawn.transform.position + CameraOffset, 4).SetDelay(2f).OnComplete(gameStart).Play();

    }

    void gameStart()
    {
        _followPlayer.StartFollow();
        _playerMovement.AllowInput();
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

    public void ShowRespawn()
    {
        _followPlayer.StopFollowing();
        _playerMovement.DenyInput();
        

        _camera.transform.DOMove(_respawnCamPos, 2).
            SetEase(Ease.InOutQuart).OnComplete(() =>
                {
                    SpawnPlayer();
                    _camera.transform.DOMove(_playerPawn.transform.position + CameraOffset, 3).SetDelay(3f).
                        SetEase(Ease.InOutQuart).
                        OnComplete(gameStart).Play();
                }).Play();

    }
}
