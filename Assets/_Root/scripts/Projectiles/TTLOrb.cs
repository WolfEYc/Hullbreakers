
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class TTLOrb : TTL
    {
        public ObjectPool<Orb> pool;
        Orb _dmger;
        Spawnable _spawnable;

        protected override void Awake()
        {
            base.Awake();
            _dmger = GetComponent<Orb>();
            _spawnable = GetComponent<Spawnable>();
            _spawnable.ToggleShinyOn();
        }
    
        public override void HandleDeath()
        {
            if(!gObject.activeSelf) return;
            pool.Release(_dmger);
            _spawnable.Spawn();
        }
    }
}
