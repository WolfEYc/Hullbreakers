using UnityEngine;

namespace Hullbreakers
{
    public class Crash : MonoBehaviour
    {
        Rigidbody2D _rb;
        public float crashDmgMultiplier;
        GameObject _gameobject;
    
        public bool dmgNumbersOn;

        const float CrashDmgBase = 5f;
        const int PlayerLayer = 9;
    
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _gameobject = gameObject;

            if (dmgNumbersOn)
            {
                crashDmgMultiplier *= GameMaster.Inst.PrestigeMult;
            }
            
        }
    
        void OnCollisionEnter2D(Collision2D col)
        {
            GameObject other = col.gameObject;
        
            if(!other.TryGetComponent(out IDamageable damageShip)) return;

            if (Friendly(other)) return;
        
            DealCrash(damageShip);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.gameObject.TryGetComponent(out IDamageable damageShip)) return;
        
            DealCrash(damageShip);
        }

        void DealCrash(IDamageable damageable)
        {
            var velocity = _rb.velocity;
            float totalDmg = CrashDmgBase + crashDmgMultiplier * velocity.sqrMagnitude;
        
            float resultDmg = damageable.Damage(totalDmg, velocity);
        
            if (dmgNumbersOn)
            {
                GameMaster.Inst.DamageAtLocation(resultDmg, _rb.position);
            }
        }

        bool Friendly(GameObject g)
        {
            return (g.layer == PlayerLayer && _gameobject.layer == PlayerLayer)
                   || (g.layer != PlayerLayer && _gameobject.layer != PlayerLayer);
        }
    }
}
