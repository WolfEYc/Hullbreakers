using UnityEngine;

namespace Hullbreakers
{
    public interface IDamageable
    {
        public float Damage(float dmg, Vector2 velocity);
    }
}
