using UnityEngine;

namespace Hullbreakers
{
    public class Spin : AI
    {
        [SerializeField] float rotSpeed;

        void Start()
        {
            rb.angularVelocity = rotSpeed;
        }
    }
}
