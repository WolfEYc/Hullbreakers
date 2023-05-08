using UnityEngine;
using UnityEngine.Events;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        public UnityEvent startedThrusting;
        public UnityEvent stoppedThrusting;
    
        public float accel;
        public float maxVeloclity;
        public float friction;

        Transform _transform;
        Rigidbody2D _rb;
        Vector2 _force;

        bool _thrusting;
        [HideInInspector] public bool dashing;
        
        void Awake()
        {
            _transform = transform;
            _rb = GetComponent<Rigidbody2D>();
        }

        public void ToggleThrust(bool on)
        {
            if(_thrusting == on) return;
            _thrusting = on;
        
        
            if (_thrusting)
            {
                startedThrusting.Invoke();
            }
            else
            {
                stoppedThrusting.Invoke();
            }
        }
    
        void FixedUpdate()
        {
            if (_thrusting && !dashing)
            {
                _force = (Vector2)_transform.up * accel;
                var velocity = _rb.velocity;
            
                velocity += _force * Time.fixedDeltaTime;

                _rb.velocity = Vector2.ClampMagnitude(velocity, maxVeloclity);
            
            }
            else
            {
                _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, friction * Time.fixedDeltaTime);
            }
        }

        public void SetAccel(float newaccel)
        {
            accel = newaccel;
        }

        public void SetMaxVel(float maxVel)
        {
            maxVeloclity = maxVel;
        }
    }
}
