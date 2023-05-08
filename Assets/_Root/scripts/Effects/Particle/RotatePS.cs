using UnityEngine;

namespace Hullbreakers
{
    public class RotatePS : MonoBehaviour
    {
        public ParticleSystem ps;
        public Rigidbody2D rb;

        ParticleSystem.MainModule _main;

        void Awake()
        {
            _main = ps.main;
        }

        void Update()
        {
            _main.startRotation = -rb.rotation * Mathf.Deg2Rad;
        }
    }
}
