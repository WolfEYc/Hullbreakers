using UnityEngine;

namespace Hullbreakers
{
    public class GoToMid : AI
    {
        protected override void HandleMovement()
        {
            rb.velocity = -rb.position * (movement.accel * Time.fixedDeltaTime);
        }
    }
}
