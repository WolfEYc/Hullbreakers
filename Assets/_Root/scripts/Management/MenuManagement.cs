using System.Collections.Generic;
using UnityEngine;

namespace Hullbreakers
{
    public class MenuManagement : Singleton<MenuManagement>
    {
        [SerializeField] GameObject firstMenu;
        
        Stack<GameObject> _menustack = new();

        void Start()
        {
            OpenMenu(firstMenu);
        }

        public void OpenMenu(GameObject menu)
        {
            if (_menustack.Count > 0)
            {
                _menustack.Peek().SetActive(false);
            }
            _menustack.Push(menu);
            menu.SetActive(true);
        }

        public void OverrideMenu(GameObject menu)
        {
            if (_menustack.Count > 0)
            {
                _menustack.Peek().SetActive(false);
            }
            _menustack.Clear();
            OpenMenu(menu);
        }

        public void Back()
        {
            if(_menustack.Count < 2) return;
            _menustack.Pop().SetActive(false);
            _menustack.Peek().SetActive(true);
        }
    }
}
