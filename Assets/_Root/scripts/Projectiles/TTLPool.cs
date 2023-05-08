
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class TTLPool : TTL
    {
        public ObjectPool<Damager> pool;
        Damager _dmger;
        
        

        protected override void Awake()
        {
            base.Awake();
            _dmger = GetComponent<Damager>();

        }

        
        public override void HandleDeath()
        {
            if(!gObject.activeSelf) return;

            pool.Release(_dmger);
        }
        
        
        
    }
}
