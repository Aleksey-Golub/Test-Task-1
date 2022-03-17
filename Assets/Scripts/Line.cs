using UnityEngine;

public class Line : MonoBehaviour, IUpdateable
{
    [SerializeField] private LineRenderer _lineRenderer;

    private Transform _startPoint;
    private Transform _endPoint;

    public void CustomUpdate(float deltaTime)
    {
        _lineRenderer.SetPosition(0, _startPoint.position);
        _lineRenderer.SetPosition(1, _endPoint.position);
    }

    public void Init(Transform startPoint)
    {
        _startPoint = startPoint;
        _endPoint = startPoint;
    }

    public void SetEndPoint(Transform point)
    {
        _endPoint = point;
    }
}
