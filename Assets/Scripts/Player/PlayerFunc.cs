using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerFunc : MonoBehaviour
{
    //[SerializeField]
    float _rayDistance = 1f;
    Transform _rayOrigin = null;

    Transform _holdPoint = null;
    HoldableItem _holdableItem = null;

    InputSystem.PlayerActions _playerInput;

    void Awake()
    {
        _rayOrigin = transform.Find(Constants.RAY_ORIGIN_NAME);
        _holdPoint = transform.Find(Constants.HOLD_POINT_NAME);
    }
    private void OnEnable()
    {
        _playerInput = InputManager.Instance.Player;

        _playerInput.PickUpPutDown.performed += OnPickUpPutDown;
        _playerInput.PickUpPutDown.canceled += OnPickUpPutDown;

        _playerInput.Throw.performed += OnThrow;
    }
    private void Update()
    {
        Debug.DrawRay(_rayOrigin.position, transform.forward * _rayDistance, Color.red);
    }

    public void OnPickUpPutDown(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;

        bool isCast = Physics.Raycast(_rayOrigin.position, transform.forward, out RaycastHit hitInfo, _rayDistance);
        if (_holdableItem != null)
            PutDown(isCast, hitInfo);
        else if(isCast)
            PickUp(hitInfo);
    }
    void PutDown(bool isCast, RaycastHit hitInfo)
    {
        bool isPlaced = false;
        if (isCast && hitInfo.collider.TryGetComponent<Table>(out Table table))
            isPlaced = table.OnPlace(_holdableItem);
        
        if(!isPlaced)
            _holdableItem.OnReleased();

        _holdableItem = null;
    }
    void PickUp(RaycastHit hitInfo)
    {
        if (hitInfo.collider.TryGetComponent<HoldableItem>(out HoldableItem item))
        {
            _holdableItem = item;
            _holdableItem.OnPlaced(_holdPoint);
        }
        else if (hitInfo.collider.TryGetComponent<Table>(out Table table))
        {

        }
    }
    public void OnThrow(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;
        if (_holdableItem == null)
            return;

        _holdableItem.OnThrow();
        _holdableItem = null;
    }
}
