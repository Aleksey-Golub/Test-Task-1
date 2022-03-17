using UnityEngine;

public class ObjectSelector : MonoBehaviour, ISelectableVisitor
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _pointerObject;
    [SerializeField] private LineController _lineController;

    private ConnectableController _connectableController;
    private Line _currentLine;
    private Connectable _currentConnectable;
    private Connector _selectedConnector;
    private Connector _hoveredConnector;
    private PlayerInput _input;
    private Plane _plane;

    private void Update()
    {
        if (_input.PointerDown)
            SelectObject();

        if (_input.PointerHold)
            MoveObject();

        if (_input.PointerUp)
            DeselectObject();

        _lineController.CustomUpdate();
    }

    public void Init(PlayerInput input, ConnectableController connectableController)
    {
        _input = input;
        _connectableController = connectableController;
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void MoveObject()
    {
        Ray ray = _camera.ScreenPointToRay(_input.PointerScreenPosition);

        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 pointerPosition = ray.GetPoint(distance);

        if (_currentConnectable)
            _currentConnectable.Move(pointerPosition);

        if (_currentLine)
        {

            RaycastHit hit;
            Connector connector;
            if (Physics.Raycast(ray, out hit) == false || hit.collider.TryGetComponent(out connector) == false)
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

    private void DeselectObject()
    {
        _currentConnectable = null;

        if (_currentLine)
        {
            Ray ray = _camera.ScreenPointToRay(_input.PointerScreenPosition);

            RaycastHit hit;
            Connector connector;
            if (Physics.Raycast(ray, out hit) == false || hit.collider.TryGetComponent(out connector) == false)
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

    private void SelectObject()
    {
        Ray ray = _camera.ScreenPointToRay(_input.PointerScreenPosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) == false)
        {
            ResetConnector();
            return;
        }

        Selectable selected = hit.collider.GetComponent<Selectable>();
        if (selected)
            selected.Accept(this);
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

    private void ResetConnector(bool removeLine = true)
    {
        _connectableController.DeselectAll();

        _selectedConnector = null;

        if (removeLine && _currentLine)
            _lineController.RemoveLine(_currentLine);

        _currentLine = null;
    }
}
