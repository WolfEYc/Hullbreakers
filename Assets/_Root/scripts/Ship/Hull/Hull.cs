using System;
using UnityEngine;
using UnityEngine.Events;

namespace Hullbreakers
{
    public class Hull : MonoBehaviour, IDamageable
    {
        public float maxHp;

        public bool invincible;
        public float knockbackScalar;
        public bool isPlayer;
        bool _destroyed;
        
        public UnityEvent destroyed;
        
        public event Action<float> hpUpdate;
        public event Action<float> maxHpUpdate;

        Rigidbody2D _rb;

        public float Hp { get; protected set; }

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            if (!isPlayer) return;
            
            maxHpUpdate += HpMeter.Instance.playerHpMeter.SetMaxValue;
            hpUpdate += HpMeter.Instance.playerHpMeter.SetValue;
            hpUpdate += Ambulance.Instance.HandleHpUpdate;
        }

        public void SetMaxHp(float newMaxHp)
        {
            maxHp = newMaxHp;
            Hp = maxHp;
            maxHpUpdate?.Invoke(maxHp);
            hpUpdate?.Invoke(Hp);
        }
    
        void Start()
        {
            Hp = maxHp;
            maxHpUpdate?.Invoke(maxHp);
            hpUpdate?.Invoke(Hp);
        }
    
        public float Damage(float dmg, Vector2 velocity)
        {
            if(invincible) return 0f;
            
            Hp = Mathf.Min(Hp - dmg, maxHp);
        
            hpUpdate?.Invoke(Hp);
            
            _rb.AddForce(velocity * knockbackScalar, ForceMode2D.Impulse);
            
            if (Hp > 0f) return dmg;
        
            Kill();

            return dmg;
        }

        public void Kill()
        {
            if(_destroyed) return;
            _destroyed = true;
            invincible = true;
            Hp = 0f;
            hpUpdate?.Invoke(Hp);
            destroyed.Invoke();
            Destroy(gameObject);
        }
    }
}
