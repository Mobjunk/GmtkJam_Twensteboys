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

    private void Awake()
    {
        _playerSpawn = FindObjectOfType<PlayerSpawn>();
        _camera = FindObjectOfType<Camera>();
        if (_camera == null)
        {
            _camera = Instantiate(_cameraGameObject, Vector3.zero, Quaternion.identity).GetComponent<Camera>();
        }

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (GetPlayer == null)
        {
            _playerPawn = Instantiate(_playerPawnObject, _playerSpawn.transform.position,Quaternion.identity);
        }

        _playerPawn.transform.position = _playerPawn.transform.position;

    }
}
