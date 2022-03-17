using System.Collections.Generic;
using UnityEngine;

public class ConnectableController : MonoBehaviour
{
    private List<Connectable> _connectables = new List<Connectable>();

    public IReadOnlyList<Connectable> Connectables => _connectables;

    public void DeselectAll()
    {
        foreach (var c in _connectables)
            c.Connector.Deselect();
    }

    public void UnselectAll()
    {
        foreach (var c in _connectables)
            c.Connector.Unselect();
    }

    public void GenerateConnectables(Connectable prefab, int objectCount, Vector3 center, float radius)
    {
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 newObjectPosition = Random.insideUnitSphere * radius + center;
            newObjectPosition.y = 0;
            Connectable newObject = Instantiate(prefab, newObjectPosition, Quaternion.identity, transform);
            _connectables.Add(newObject);
        }
    }
}
