using UnityEngine;

namespace Hullbreakers
{
    public class TreadMill : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float target;
        public Vector2 start;
        public float speed;

        Vector3 _startPos;

        void Awake()
        {
            rb.velocity = new Vector2(-speed, 0f);
        }

        void FixedUpdate()
        {
            if(rb.position.x > target) return;
            rb.position = start;
        }
    }
}
