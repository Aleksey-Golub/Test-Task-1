using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Connector : Selectable
{
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private Material _unselectedMaterial;
    [SerializeField] private MeshRenderer _meshRenderer;

    private Material _defaultMaterial;

    private void Start()
    {
        _defaultMaterial = _meshRenderer.material;
    }

    private void SetMaterial(Material newMaterial)
    {
        _meshRenderer.material = newMaterial;
    }

    public void Select()
    {
        SetMaterial(_selectedMaterial);
    }

    public void Deselect()
    {
        SetMaterial(_defaultMaterial);
    }

    public void Unselect()
    {
        SetMaterial(_unselectedMaterial);
    }

    public override void Accept(ISelectableVisitor visitor)
    {
        visitor.Visit(this);
    }
}
