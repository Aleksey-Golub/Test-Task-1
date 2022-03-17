using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public abstract void Accept(ISelectableVisitor visitor);
}
