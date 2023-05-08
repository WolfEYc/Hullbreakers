using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class TTLNo : TTL
    {
        public ObjectPool<TTLNo> pool;
        public Rigidbody2D rb;
        public TMP_Text text;
    
    
        public override void HandleDeath()
        {
            pool.Release(this);
        }
    }
}
