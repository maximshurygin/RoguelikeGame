using UnityEngine;

namespace GameCore.UpgradeSystem
{
    [CreateAssetMenu(fileName = "UpgradeCard", menuName = "ScriptableObjects/Upgrade")]
    public class UpgradeCard : ScriptableObject
    {
        [Header("Values")]
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _id;


        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public int ID => _id;
    }
}