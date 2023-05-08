using UnityEngine;
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class XpDonor : MonoBehaviour
    {
        public int xp;
        public Rigidbody2D me;
    
        ObjectPool<Orb> _xpPool;

        void Awake()
        {
            _xpPool = GameMaster.Inst.XpPool;
        }

        public void SpawnOrb()
        {
            while (xp > 0)
            {
                XpOrb orb = (XpOrb)_xpPool.Get();
                orb.ttlOrb.SelfTransform.position = me.position;
                xp -= 10;
            }
        }
    }
}