using UnityEngine;

public class PlayerInput
{
    public bool PointerUp => Input.GetMouseButtonUp(0);
    public bool PointerHold => Input.GetMouseButton(0);
    public bool PointerDown => Input.GetMouseButtonDown(0);
    public Vector3 PointerScreenPosition => Input.mousePosition;
    public bool CanRotateCamera => Input.GetMouseButton(1);
    public float XMovement => Input.GetAxis("Horizontal");
    public float YMovement => Input.GetAxis("QE");
    public float ZMovement => Input.GetAxis("Vertical");
    public float XMouse => Input.GetAxis("Mouse X");
    public float YMouse => Input.GetAxis("Mouse Y");
}
