using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController _charCtrl;

    //[SerializeField]
    float _moveSpeed = 6f;
    Vector3 _moveDir = default;

    //[SerializeField]
    float _dashSpeed = 7f;
    //[SerializeField]
    float _dashDuration = 0.2f;
    float _dashTimer = 0f;

    //[SerializeField]
    float _rotateSpeed = 30f;

    InputSystem.PlayerActions _playerInput;

    private void OnEnable()
    {
        _charCtrl = GetComponent<CharacterController>();

        _playerInput = InputManager.Instance.Player;
        
        _playerInput.Move.performed += OnMove;
        _playerInput.Move.canceled += OnMove;

        _playerInput.Dash.performed += OnDash;
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        Vector3 dir = Vector3.zero;
        float velocity = 0f;

        dir = _moveDir;
        velocity = Time.deltaTime * Mathf.Min(dir.magnitude, 1f) * _moveSpeed;

        if (_dashTimer > 0f)
        {
            _dashTimer -= Time.deltaTime;
            dir = transform.forward;
            velocity = Time.deltaTime * _dashSpeed;
        }

        transform.forward = Vector3.Slerp(transform.forward, dir, Time.deltaTime * _rotateSpeed);
        _charCtrl.Move(velocity * dir.normalized);
    }
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Vector2 inputVal = ctx.ReadValue<Vector2>();
        _moveDir.x = inputVal.x;
        _moveDir.z = inputVal.y;
    }
    public void OnDash(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton() && _dashTimer <= 0f)
            _dashTimer = _dashDuration;
    }
}
