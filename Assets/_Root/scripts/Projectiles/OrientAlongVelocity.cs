using UnityEngine;

namespace Hullbreakers
{
    public class OrientAlongVelocity : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rb;
        
        void FixedUpdate()
        {
            rb.rotation = Rotation.LookAtRot(rb.velocity);
        }
    }
}
