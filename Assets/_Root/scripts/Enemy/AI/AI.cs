using UnityEngine;

namespace Hullbreakers
{
    public class AI : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Movement movement;
        protected Rotation rotation;
        WeaponBase[] _weapons;

        bool _playerAlive = true;

        protected Rigidbody2D PlayerRb => GameMaster.Inst.PlayerRb;

        protected virtual void Awake()
        {
            movement = GetComponent<Movement>();
            rotation = GetComponent<Rotation>();
            _weapons = GetComponentsInChildren<WeaponBase>();
            rb = GetComponent<Rigidbody2D>();

            if (GameMaster.Inst.CurrentState != GameMaster.GameState.InGame)
            {
                _playerAlive = false;
            }
            else
            {
                GameMaster.Inst.gameEnd.AddListener(SetPlayerDead);
            }
        }

        void FixedUpdate()
        {
            if (!_playerAlive) return;
            HandleMovement();
            HandleRotation();
        }

        protected virtual void HandleMovement()
        {
        
        }
        

        protected virtual void HandleRotation()
        {
        
        }

        void SetPlayerDead()
        {
            _playerAlive = false;
            GameMaster.Inst.gameEnd.RemoveListener(SetPlayerDead);
        }

        void OnDisable()
        {
            if (_playerAlive)
            {
                GameMaster.Inst.gameEnd.RemoveListener(SetPlayerDead);
            }
        }
    
        protected void ToggleShooting(bool on)
        {
            foreach (WeaponBase weapon in _weapons)
            {
                weapon.ToggleShooting(on);
            }
        }
    }
}
