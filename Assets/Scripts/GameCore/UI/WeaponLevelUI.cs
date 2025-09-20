using System;
using Player.Weapon;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using Zenject;

namespace GameCore.UI
{
    public class WeaponLevelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private BaseWeapon _currentWeapon;

        // [Inject]
        // public void Construct(BaseWeapon currentWeapon)
        // {
        //     _currentWeapon = currentWeapon;
        // }

        private void Start()
        {
            UpdateLevelText();
        }

        private void Update()
        {
            UpdateLevelText();
        }

        private void UpdateLevelText()
        {
            if (_currentWeapon != null && _levelText != null)
            {
                _levelText.text =
                    $"{_currentWeapon.WeaponName} lvl {_currentWeapon.CurrentLevel}";
            }
        }
    }
}