using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SnakePart : MonoBehaviour, IDamageable
    {
        public Rigidbody2D next;
        public SnakePart prev;
        public Hull head;
        public Rigidbody2D rb;
        public GameObject root;
        float _fixedDist;
        public float destructionTimer;

        [SerializeField] UnityEvent destroyed;
    
        [SerializeField] float snakePartDmgMult = 0.3f;
  
        bool _isBottom;
    
        void Awake()
        {
            _isBottom = prev == null;
        }

        void Start()
        {
            _fixedDist = (next.position - rb.position).magnitude;
        }

        void FixedUpdate()
        {
            if(!enabled) return;
            var nextPosition = next.position;
            var rbPos = rb.position;
            Vector2 vectorToNext = Vector2.ClampMagnitude(
                nextPosition - rbPos,
                _fixedDist);
        
            rb.position = nextPosition - vectorToNext;
        
            rb.rotation = Rotation.LookAtRot(rbPos, nextPosition);
        }

        public float Damage(float dmg, Vector2 velocity)
        {
            if (!enabled) return 0f;
        
            dmg *= snakePartDmgMult;
            return head.Damage(dmg, velocity);
        }

        IEnumerator Die()
        {
            yield return new WaitForSeconds(destructionTimer);
            Kill();
            Destroy(gameObject);
        }

        public void Disable()
        {
            enabled = false;
        
            if (_isBottom)
            {
                return;
            }
        
            prev.Disable();
        }
    
        public void Kill()
        {
            destroyed.Invoke();

            if (_isBottom)
            {
                Destroy(root);
                return;
            }

            prev.StartCoroutine(prev.Die());
        }
    }
}
