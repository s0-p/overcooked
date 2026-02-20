using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController _charCtrl;

    [SerializeField]
    float _moveSpeed = 6f;
    Vector3 _moveDir = default;
    [SerializeField]
    float _rotateSpeed = 30f;

    InputSystem.PlayerActions _playerInput;

    private void OnEnable()
    {
        _charCtrl = GetComponent<CharacterController>();

        _playerInput = InputManager.Instance.Player;
        _playerInput.Move.performed += OnMove;
        _playerInput.Move.canceled += OnMove;
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        _charCtrl.Move(Time.deltaTime * Mathf.Min(_moveDir.magnitude, 1f) * _moveSpeed * _moveDir.normalized);
    }
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Vector2 inputVal = ctx.ReadValue<Vector2>();
        _moveDir.x = inputVal.x;
        _moveDir.z = inputVal.y;
    }
}
