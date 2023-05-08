using UnityEngine;
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class OrbPool : MonoBehaviour
    {
        public ObjectPool<Orb> Pool { get; private set; }
    
        public Orb toInstantiate;
    
        public Transform parent;

        [SerializeField] float ttl;

        WaitForSeconds _tll;

        [SerializeField] int defaultCapacity;
        
        void Awake()
        {
            Pool = new ObjectPool<Orb>(
                PoolInstantiate,
                OnTakeFromPool,
                OnReturnedToPool,
                defaultCapacity: defaultCapacity,
                maxSize: defaultCapacity
            );
            _tll = new WaitForSeconds(ttl);
        }

        Orb PoolInstantiate()
        {
            Orb dmger = Instantiate(toInstantiate, parent);
            dmger.ttlOrb.pool = Pool;
            dmger.ttlOrb.ttl = _tll;
            
            return dmger;
        }
    
        void OnReturnedToPool(Orb dmger)
        {
            dmger.me.velocity = Vector2.zero;
            dmger.Self.SetActive(false);
        }
    
        void OnTakeFromPool(Orb dmger)
        {
            dmger.Self.SetActive(true);
            dmger.ttlOrb.StartTimer();
        }
    }
}
