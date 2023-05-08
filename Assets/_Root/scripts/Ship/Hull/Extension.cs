using UnityEngine;

namespace Hullbreakers
{
    public class Extension : MonoBehaviour, IDamageable
    {
        [SerializeField] float dmgMult;
        [SerializeField] Hull hull;


        public float Damage(float dmg, Vector2 velocity)
        {
            return hull.Damage(dmg * dmgMult, velocity);
        }
    }
}
