using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rotation))]
    public class SimpleDrone : MonoBehaviour
    {
        [SerializeField] Rotation parentTarget;
        Rotation _myRotation;

        void Awake()
        {
            _myRotation = GetComponent<Rotation>();
        }

        void FixedUpdate()
        {
            _myRotation.target = parentTarget.target;
        }
    }
}
