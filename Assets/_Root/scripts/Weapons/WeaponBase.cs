using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public abstract class WeaponBase : MonoBehaviour, IShiny
    {
        public AudioSource playOnShot;
    
        public SpriteRenderer spriteRenderer;
    
        public Rigidbody2D rb;
    
        public Transform shotPointTransform;
    
        public int pierce;

        public float fireRate;

        public float dmgMultiplier = 1f;
    
        public bool amPlayer;

        public bool fireOnDefault;
    
        protected bool music;
    
        IEnumerator _shootRoutine;
        WaitForSeconds _waitForShot;
        WaitUntil _isShooting;

        bool _shooting;
        float _fireRateMultiplier = 1f;
    
        public virtual void ToggleShooting(bool on)
        {
            _shooting = on;
        }
    
        void OnEnable()
        {
            StartCoroutine(ShootRoutine());
        }

        protected virtual void Start()
        {
            if (fireOnDefault)
            {
                ToggleShooting(true);
            }
        }

        protected virtual void Awake()
        {
            _waitForShot = new WaitForSeconds(60f / fireRate);
            _isShooting = new WaitUntil(() => _shooting);
        
            music = playOnShot != null;
        
            if (amPlayer)
            {
                dmgMultiplier *= GameMaster.Inst.PrestigeMult;
            }
        }

        public void SetFireRateMultiplier(float newfireRateMult)
        {
            _fireRateMultiplier = newfireRateMult;
            _waitForShot = new WaitForSeconds(60f / (fireRate * _fireRateMultiplier));
        }
        
        public abstract void Shoot();
    
    
        IEnumerator ShootRoutine()
        {
            while (true)
            {
                yield return _waitForShot;
                yield return _isShooting;
                if(!enabled) yield break;
                Shoot();
            }
        }

        public void SetMusic(bool on)
        {
            music = on;
        }

        public abstract void ToggleShinyOn();
        public abstract void ToggleShinyOff();
    }
}
