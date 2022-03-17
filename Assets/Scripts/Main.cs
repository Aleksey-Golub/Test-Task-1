using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private Game _game;
    [SerializeField] private ConnectableController _connectableController;

    [Header("Settings")]
    [SerializeField] private float _radius = 10;
    [SerializeField] private Connectable _prefab;
    [SerializeField] private int _objectCount = 10;

    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = new PlayerInput();
        _connectableController.GenerateConnectables(_prefab, _objectCount, transform.position, _radius);

        _cameraMover.Init(_playerInput);
        _game.Init(_playerInput, _connectableController);
    }

    private void Update()
    {
        _game.CustomUpdate(Time.deltaTime);
    }
}
