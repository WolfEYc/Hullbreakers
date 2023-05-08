using UnityEngine;
using UnityEngine.InputSystem;

namespace Hullbreakers
{
    public class PlayerControls : MonoBehaviour, InputMaster.IPlayerActions
    {
        Movement _movement;
        Rotation _rotation;
        [SerializeField] Ability ability;
        WeaponBase[] _weapons;
        Camera _camera;
        LevelUp _levelUp;
        
        void Awake()
        {
            _camera = Camera.main;
            _movement = GetComponent<Movement>();
            _rotation = GetComponent<Rotation>();
            _weapons = GetComponentsInChildren<WeaponBase>();
            _levelUp = GetComponent<LevelUp>();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _movement.ToggleThrust(context.action.IsPressed());
        }

        public void OnRotation(InputAction.CallbackContext context)
        {
            _rotation.target =_camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            bool pressing = context.action.IsPressed();
            foreach (WeaponBase weapon in _weapons)
            {
                weapon.ToggleShooting(pressing);
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.action.IsPressed())
            {
                ability.Use();
            }
        }

        public void OnUpgrade(InputAction.CallbackContext context)
        {
            if (context.action.IsPressed())
            {
                _levelUp.PerformLevelUp();
            }
        }
    }
}
