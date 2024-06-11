using System;
using UnityEngine;


namespace LemApperson_UIPortfolio
{

    [CreateAssetMenu(fileName = "MenuSettings", menuName = "ScriptableObjects/MenuSettings", order = 1)]

    public class MenuSettings : ScriptableObject
    {
        
        public event Action<bool> OnDarkModeChanged;
        
        [SerializeField] private bool darkMode;

        public void ToggleDarkMode()
        {
            darkMode = !darkMode;
            OnDarkModeChanged?.Invoke(darkMode);
        }

        public bool GetDarkMode(){
            return darkMode;
        }
    }
}
