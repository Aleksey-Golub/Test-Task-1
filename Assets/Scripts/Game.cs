using UnityEngine;

public class Game : MonoBehaviour, ISelectableVisitor, IUpdateable
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _pointerObject;
    [SerializeField] private LineController _lineController;

    private ConnectableController _connectableController;
    private PlayerInput _input;
    private Line _currentLine;
    private Connectable _currentConnectable;
    private Connector _selectedConnector;
    private Connector _hoveredConnector;
    private Plane _plane;

    public void CustomUpdate(float deltaTime)
    {
        RaycastHit hit;
        Ray ray;
        ProduceRaycast(out hit, out ray);

        if (_input.PointerDown)
            HandlePointerDown(ray, hit);

        if (_input.PointerHold)
            HandlePointerHold(ray, hit);

        if (_input.PointerUp)
            HandlePointerUp(ray, hit);

        _lineController.CustomUpdate(deltaTime);
    }
    
    public void Init(PlayerInput input, ConnectableController connectableController)
    {
        _input = input;
        _connectableController = connectableController;
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    public void Visit(Platform platform)
    {
        _currentConnectable = platform.Connectable;

        if (_selectedConnector)
            ResetConnector();
    }

    public void Visit(Connector connector)
    {
        if (_selectedConnector == connector)
        {
            ResetConnector();
            return;
        }

        _selectedConnector = connector;
        if (_currentLine)
        {
            _currentLine.SetEndPoint(_selectedConnector.transform);
            ResetConnector(false);
        }
        else
        {
            _currentLine = _lineController.GetLine(_selectedConnector.transform);
            _connectableController.UnselectAll();
            connector.Select();
        }
    }

    private void HandlePointerDown(Ray ray, RaycastHit hit)
    {
        if (hit.collider == null)
        {
            ResetConnector();
            return;
        }

        Selectable selected = hit.collider.GetComponent<Selectable>();
        if (selected)
            selected.Accept(this);
    }

    private void HandlePointerHold(Ray ray, RaycastHit hit)
    {
        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 pointerPosition = ray.GetPoint(distance);

        // handle platform
        if (_currentConnectable)
            _currentConnectable.Move(pointerPosition);

        // handle line
        if (_currentLine)
        {
            Connector connector;
            if (hit.collider == null || hit.collider.TryGetComponent(out connector) == false)
            {
                _pointerObject.position = pointerPosition;
                _currentLine.SetEndPoint(_pointerObject);

                if (_hoveredConnector != _selectedConnector)
                    _hoveredConnector?.Unselect();
                
                _hoveredConnector = null;
                return;
            }

            if (_hoveredConnector != _selectedConnector)
                _hoveredConnector?.Unselect();

            _hoveredConnector = connector;
            _hoveredConnector.Select();

            _pointerObject.position = connector.transform.position;
            _currentLine.SetEndPoint(_pointerObject);
        }
    }

    private void HandlePointerUp(Ray ray, RaycastHit hit)
    {
        // handle platform
        _currentConnectable = null;

        // handle line
        if (_currentLine)
        {
            Connector connector;
            if (hit.collider == null || hit.collider.TryGetComponent(out connector) == false)
            {
                ResetConnector();
            }
            else if (connector != _selectedConnector)
            {
                _currentLine.SetEndPoint(connector.transform);
                ResetConnector(false);
            }
        }
    }

    private void ResetConnector(bool removeLine = true)
    {
        _connectableController.DeselectAll();

        _selectedConnector = null;

        if (removeLine && _currentLine)
            _lineController.RemoveLine(_currentLine);

        _currentLine = null;
    }

    private void ProduceRaycast(out RaycastHit hit, out Ray ray)
    {
        ray = _camera.ScreenPointToRay(_input.PointerScreenPosition);
        Physics.Raycast(ray, out hit);
    }
}
