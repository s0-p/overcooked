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
    public void OnPickUp(Transform holdPoint)
    {
        transform.SetParent(holdPoint);
        transform.position = holdPoint.position;

        _rigidbody.isKinematic = true;
        //_rigidbody.interpolation = ;
    }
    public void OnThrow()
    {
        Vector3 throwDir = transform.root.forward * _throwForce;
        _rigidbody.isKinematic = false;
        //_rigidbody.interpolation = ;

        transform.parent = null;
        _rigidbody.AddForce(throwDir, ForceMode.Impulse);
    }
}
