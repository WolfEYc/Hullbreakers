using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Hull))]
    public class SnekAI : AI
    {
        Transform _transform;


        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
        }
    
        protected override void HandleRotation()
        {
            rotation.target = PlayerRb.position;
        }

        protected override void HandleMovement()
        {
            rb.velocity = (Vector2)_transform.up * movement.accel;
        }
    }
}
