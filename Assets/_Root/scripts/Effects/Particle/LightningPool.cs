using UnityEngine;
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class LightningPool : MonoBehaviour
    {
        public static ObjectPool<Lightning> Pool { get; private set; }
        public static ObjectPool<Sparks> HitVFX { get; private set; }

        [SerializeField] Lightning lightningPrefab;
        [SerializeField] Sparks hitVfXprefab;
        
        Transform _transform;

        const int DefaultCapacity = 32;
        
        void Awake()
        {
            if (Pool != null)
            {
                Destroy(this);
                return;
            }
            
            _transform = transform;
            Pool = new ObjectPool<Lightning>(CreateLightning, defaultCapacity: DefaultCapacity, maxSize: DefaultCapacity);
            HitVFX = new ObjectPool<Sparks>(CreateHitVFX, defaultCapacity: DefaultCapacity, maxSize: DefaultCapacity);
        }

        Lightning CreateLightning()
        {
            return Instantiate(lightningPrefab, _transform);
        }

        Sparks CreateHitVFX()
        {
            return Instantiate(hitVfXprefab, _transform);
        }
    }
}
