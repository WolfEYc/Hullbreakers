using UnityEngine;
using Random = UnityEngine.Random;

namespace Hullbreakers
{
    public class SimpleAI : AI
    {
        public float minThresh;
        public float maxThresh;

        float _threshold;

        bool WithinThreshold => (rb.position - PlayerRb.position).sqrMagnitude < _threshold;
    
        protected override void Awake()
        {
            base.Awake();
        
            _threshold = Mathf.Pow(Random.Range(minThresh, maxThresh), 2);
        }

        protected override void HandleMovement()
        {
            movement.ToggleThrust(!WithinThreshold);
        }
    }
}
