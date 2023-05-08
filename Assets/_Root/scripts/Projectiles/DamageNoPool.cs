using UnityEngine;
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class DamageNoPool : MonoBehaviour
    {
        public ObjectPool<TTLNo> Pool { get; private set; }
    
        public TTLNo toInstantiate;

        public Transform parent;

        public float ttl;

        WaitForSeconds _ttl;

        [SerializeField] int defaultCapacity;
        
        void Awake()
        {
            Pool = new ObjectPool<TTLNo>(
                PoolInstantiate, 
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                defaultCapacity: defaultCapacity,
                maxSize: defaultCapacity
            );
            _ttl = new WaitForSeconds(ttl);
        }

        TTLNo PoolInstantiate()
        {
            TTLNo dmger = Instantiate(toInstantiate, parent);
            dmger.pool = Pool;
            dmger.ttl = _ttl;
            
            return dmger;
        }
    
        void OnReturnedToPool(TTLNo dmger)
        {
            dmger.rb.velocity = Vector2.zero;
            dmger.gObject.SetActive(false);
        }
    
        void OnTakeFromPool(TTLNo dmger)
        {
            dmger.gObject.SetActive(true);
            dmger.StartTimer();
        }
    
        void OnDestroyPoolObject(TTLNo dmger)
        {
            Destroy(dmger.gObject);
        }
    }
}
