using UnityEngine;
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class ProjectilePool : MonoBehaviour
    {
        public ObjectPool<Damager> Pool { get; private set; }
    
        public Damager toInstantiate;

        public int layer;

        public Transform shotsParent;

        [SerializeField] int defaultBufferSize;
        
        
        void Awake()
        {
            Pool = new ObjectPool<Damager>(
                PoolInstantiate, 
                OnTakeFromPool,
                OnReturnedToPool,
                defaultCapacity: defaultBufferSize,
                maxSize: defaultBufferSize
            );
            
            
        }

        Damager PoolInstantiate()
        {
            Damager dmger = Instantiate(toInstantiate, shotsParent);
            dmger.Self.layer = layer;
            dmger.ttlPool.pool = Pool;

            return dmger;
        }
    
        void OnReturnedToPool(Damager dmger)
        {
            dmger.rb.velocity = Vector2.zero;
            dmger.Self.SetActive(false);
        }
    
        void OnTakeFromPool(Damager dmger)
        {
            dmger.Self.SetActive(true);
        }
    }
}
