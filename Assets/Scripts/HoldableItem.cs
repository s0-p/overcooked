using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HoldableItem : MonoBehaviour
{
    //[SerializeField]
    float _throwForce = 10f;

    Rigidbody _rigidbody = default;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void OnPlaced(Transform holdPoint)
    {
        transform.SetParent(holdPoint);
        transform.position = holdPoint.position;
        transform.rotation = holdPoint.rotation;

        _rigidbody.isKinematic = true;
        //_rigidbody.interpolation = ;
    }
    public void OnReleased()
    {
        _rigidbody.isKinematic = false;
        //_rigidbody.interpolation = ;

        transform.parent = null;
    }
    public void OnThrow()
    {
        transform.parent = null;

        _rigidbody.isKinematic = false;
        //_rigidbody.interpolation = ;

        Vector3 throwDir = transform.forward * _throwForce;
        _rigidbody.AddForce(throwDir, ForceMode.Impulse);
    }
}
