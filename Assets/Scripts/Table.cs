using UnityEngine;

public class Table : MonoBehaviour
{
    Transform _holdPoint = null;
    HoldableItem _holdableItem = null;
    private void Awake()
    {
        _holdPoint = transform.Find(Constants.HOLD_POINT_NAME);
    }
    public bool OnPlace(HoldableItem item)
    {
        if (_holdableItem != null)
            return false;

        _holdableItem = item;
        _holdableItem.OnPlaced(_holdPoint);
        return true;
    }
}
