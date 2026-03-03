using UnityEngine;

public class PlayerFunc : MonoBehaviour
{
    [SerializeField]
    GameObject _prefHoldPoint = default;
    Transform _holdPoint = null;
    public GameObject SensedObj { get; set; }
    HoldableItem _holdableItem = null;
    bool _isHeld = false;

    [SerializeField]
    GameObject _prefSensor = default;
    GameObject _objSensor = default;

    public bool IsSenseIngredients { get; set; }

    InputSystem.PlayerActions _playerInput;

    void Awake()
    {
        _holdPoint = Instantiate(_prefHoldPoint).transform;
        _holdPoint.SetParent(transform, false);

        _objSensor = Instantiate(_prefSensor);
        _objSensor.transform.SetParent(transform, false);
    }
    private void OnEnable()
    {
        _playerInput = InputManager.Instance.Player;

        _playerInput.PickUp.performed += OnPickUp;
        _playerInput.PickUp.canceled += OnPickUp;

        _playerInput.Throw.performed += OnThrow;
    }
    
    public void OnPickUp(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        bool isPickUp = ctx.ReadValueAsButton();
        if(isPickUp && IsSenseIngredients && !_isHeld)
        {
            _holdableItem = SensedObj.GetComponent<HoldableItem>();
            _holdableItem.OnPickUp(_holdPoint);

            _isHeld = true;
            IsSenseIngredients = false;
        }
    }
    public void OnThrow(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        bool isKeyDown = ctx.ReadValueAsButton();
        if(isKeyDown && _isHeld)
        {
            _holdableItem.OnThrow();
            _holdableItem = null;

            _isHeld = false;
        }
    }
}
