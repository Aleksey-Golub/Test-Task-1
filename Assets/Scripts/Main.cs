using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private ObjectSelector _selector;

    [Header("Settings")]
    [SerializeField] private float _radius = 10;
    [SerializeField] private Connectable _prefab;
    [SerializeField] private int _objectCount = 10;

    private PlayerInput _playerInput;
    private ConnectableController _connectableController;

    private void Start()
    {
        _playerInput = new PlayerInput();
        _connectableController = new ConnectableController();
        GenerateConnectables();

        _cameraMover.Init(_playerInput);
        _selector.Init(_playerInput, _connectableController);
    }

    private void GenerateConnectables()
    {
        for (int i = 0; i < _objectCount; i++)
        {
            Vector3 newObjectPosition = Random.insideUnitSphere * _radius;
            newObjectPosition.y = 0;
            Connectable newObject = Instantiate(_prefab, newObjectPosition, Quaternion.identity);
            _connectableController.AddConnectable(newObject);
        }
    }
}
