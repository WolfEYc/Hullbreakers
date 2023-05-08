using UnityEngine;

namespace Hullbreakers
{
    public class Rotation : MonoBehaviour
    {
        public Vector2 target;
        [SerializeField] float rotationSpeed;
        const float DefaultSpriteAngle = 90f;
    
        Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public static float LookAtRot(Vector2 pos, Vector2 point)
        {
            Vector2 diff = point - pos;
            diff.Normalize();
            return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - DefaultSpriteAngle;
        }
        
        public static float LookAtRot(Vector2 pos)
        {
            pos.Normalize();
            return Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - DefaultSpriteAngle;
        }

        public static Quaternion LookAtQuaternion(Vector2 origin, Vector2 target)
        {
            return Quaternion.AngleAxis(LookAtRot(origin, target), Vector3.forward);
        }

        void FixedUpdate()
        {
            _rb.MoveRotation(Mathf.MoveTowardsAngle(
                _rb.rotation,
                LookAtRot(_rb.position, target),
                rotationSpeed * Time.fixedDeltaTime
            ));
        }

        public void SetRotationSpeed(float newspeed)
        {
            rotationSpeed = newspeed;
        }
    }
}
