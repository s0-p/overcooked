using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    enum Tag
    {
        None,
        Ingredients,
        Last
    }
    PlayerFunc _playerFunc = default;

    private void Start()
    {
        _playerFunc = GetComponentInParent<PlayerFunc>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tag.Ingredients.ToString()))
        {
            _playerFunc.IsSenseIngredients = true;
            _playerFunc.SensedObj = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Ingredients.ToString()))
        {
            _playerFunc.IsSenseIngredients = false;
            _playerFunc.SensedObj = default;
        }
    }
}
