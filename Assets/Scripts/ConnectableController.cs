using System.Collections.Generic;

public class ConnectableController
{
    private List<Connectable> _connectables = new List<Connectable>();

    public IReadOnlyList<Connectable> Connectables => _connectables;

    public void AddConnectable(Connectable connectable)
    {
        _connectables.Add(connectable);
    }

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
}
