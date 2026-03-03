using UnityEngine;

public class PlayerFunc : MonoBehaviour
{
    [SerializeField]
    GameObject _prefHoldPoint = default;
    GameObject _objHoldingPoint = default;
    public GameObject ObjHolding { get; set; }

    [SerializeField]
    GameObject _prefSensor = default;
    GameObject _objSensor = default;

    public bool IsSenseIngredients { get; set; }

    InputSystem.PlayerActions _playerInput;

    void Awake()
    {
        _objHoldingPoint = Instantiate(_prefHoldPoint);
        _objHoldingPoint.transform.SetParent(transform, false);

        _objSensor = Instantiate(_prefSensor);
        _objSensor.transform.SetParent(transform, false);
    }
    private void OnEnable()
    {
        _playerInput = InputManager.Instance.Player;

        _playerInput.PickUp.performed += OnPickUp;
        _playerInput.PickUp.canceled += OnPickUp;
    }
    
    public void OnPickUp(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        bool isPickUp = ctx.ReadValueAsButton();
        if(isPickUp && IsSenseIngredients)
        {
            ObjHolding.transform.SetParent(_objHoldingPoint.transform);
            ObjHolding.GetComponent<Rigidbody>().isKinematic = true;
            ObjHolding.transform.position = _objHoldingPoint.transform.position;

            _isHolding = true;
            IsSenseIngredients = false;
        }
    }
}
