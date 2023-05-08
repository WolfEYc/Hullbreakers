using UnityEngine;

namespace Hullbreakers
{
    public class XpOrb : Orb
    {
        public int xpAmt;
        
        protected override void OnPickup(Collider2D col)
        {
            GameMaster.Inst.PlayerLevelUp.AddXp(xpAmt);
            GameMaster.Inst.PlayerHull.Damage(-xpAmt, Vector2.zero);
        }
    }
}
