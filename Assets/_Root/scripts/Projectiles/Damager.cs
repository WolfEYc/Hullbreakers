using UnityEngine;

namespace Hullbreakers
{
    public class Damager : MonoBehaviour
    {
        public float dmg;
        public float multiplier;

        public float TotalDmg { get; private set; }
    
        public Rigidbody2D rb;
        public TTLPool ttlPool;
        public SpriteRenderer spriteRenderer;
        public AimbotShot aimbotShot;

        public bool dmgNumbersOn;

        TrailRenderer _trailRenderer;

        public GameObject Self { get; private set; }
        public Transform SelfTransform { get; private set; }

        public int pierce;
        Color _color;
        
        
        void Awake()
        {
            TotalDmg = dmg * multiplier;
            Self = gameObject;
            SelfTransform = transform;
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        public void ToggleTrail(bool on)
        {
            _trailRenderer.enabled = on;
            _color = spriteRenderer.color;
            _color.a = 0.5f;
            _trailRenderer.startColor = _color;
            _color.a = 0f;
            _trailRenderer.endColor = _color;
            _trailRenderer.widthMultiplier = SelfTransform.localScale.x;
            _trailRenderer.Clear();
        }

        public void SetMultiplier(float mp)
        {
            multiplier = mp;
            TotalDmg = dmg * multiplier;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            IDamageable damageShip = col.GetComponent<IDamageable>();
        
            float resultDmg = damageShip.Damage(TotalDmg, rb.velocity * SelfTransform.localScale.sqrMagnitude);

            if (dmgNumbersOn)
            {
                GameMaster.Inst.DamageAtLocation(resultDmg, rb.position);
            
                SpawnOnHitEffect();
            }

            if (pierce == 0)
            {
                ttlPool.StopCoroutine(ttlPool.DieRoutine);
                ttlPool.HandleDeath();
                return;
            }

            pierce--;
        }
    
        void SpawnOnHitEffect()
        {
            GameMaster.Inst.OnHitPool.Get().Spawn(SelfTransform, spriteRenderer.color);
        }
    }
}
