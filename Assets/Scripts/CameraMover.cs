using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _rotationSpeed = 150f;

    private PlayerInput _input;

    private void LateUpdate()
    {
        Move();

        if (_input.CanRotateCamera)
            Rotate();
    }

    public void Init(PlayerInput input)
    {
        _input = input;
    }

    private void Move()
    {
        Vector3 offset = _moveSpeed * Time.deltaTime * new Vector3(_input.XMovement, _input.YMovement, _input.ZMovement);

        transform.Translate(offset);
    }

    private void Rotate()
    {
        Vector3 newRotation = _rotationSpeed * Time.deltaTime * new Vector3(_input.YMouse * -1, _input.XMouse, 0);

        transform.eulerAngles +=  newRotation;
    }
}
