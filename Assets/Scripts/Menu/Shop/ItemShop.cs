using UnityEngine;

namespace Menu.Shop
{
    [CreateAssetMenu(fileName = "ItemShop", menuName = "ScriptableObjects/ItemShop")]
    public class ItemShop : ScriptableObject
    {
        [SerializeField] private float _value;
        [SerializeField] private int _cost;

        public float Value => _value;
        public int Cost => _cost;
    }
}