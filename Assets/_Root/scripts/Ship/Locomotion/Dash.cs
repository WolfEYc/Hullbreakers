using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Movement))]
    public class Dash : Ability
    {
        public float turnOffThrustForTime;
    
        Movement _movement;
    
        Rigidbody2D _rb;
        Collider2D _collider;
        public float force;
        Transform _rbTransform;
        WaitForSeconds _turnOnWait;

        public bool thru;
        

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
            _movement = GetComponent<Movement>();
            _rbTransform = transform;
            _turnOnWait = new WaitForSeconds(turnOffThrustForTime);
        }

        protected override void HandleUse()
        {
            _movement.dashing = true;
            _rb.AddForce(_rbTransform.up * force, ForceMode2D.Impulse);
            StartCoroutine(TurnOnThrustAfterTime());
        }

        IEnumerator TurnOnThrustAfterTime()
        {
            if (thru)
            {
                _collider.isTrigger = true;
            }
            
            yield return _turnOnWait;

            if (thru)
            {
                _collider.isTrigger = false;
            }

            _movement.dashing = false;
        }
    }
}
