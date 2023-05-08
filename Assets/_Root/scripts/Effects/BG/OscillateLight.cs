using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Hullbreakers
{
    public class OscillateLight : MonoBehaviour
    {
        public Light2D light2D;
        public float speed;
        public float min;
        public float max;
    
        float _scalar;
        int _dir = 1;

        void Awake()
        {
            _scalar = min;
        }
        void Flip()
        {
            _dir *= -1;
            _scalar = Mathf.Clamp(_scalar, min, max);
        }
        
        
        void Update()
        {
            _scalar += Time.deltaTime * speed * _dir;

            if (_scalar > max || _scalar < min)
            {
                Flip();
            }

            light2D.intensity = _scalar;
        }
    }
}
