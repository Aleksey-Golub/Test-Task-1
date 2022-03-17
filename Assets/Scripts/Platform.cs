using UnityEngine;

public class Platform : Selectable
{
    [SerializeField] private Connectable _connectable;

    public Connectable Connectable => _connectable;

    public override void Accept(ISelectableVisitor visitor)
    {
        visitor.Visit(this);
    }
}
