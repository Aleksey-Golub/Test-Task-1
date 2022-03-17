using UnityEngine;

public class Connectable : MonoBehaviour
{
    [SerializeField] private Connector _connector;

    public Connector Connector => _connector;

    public void Move(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
