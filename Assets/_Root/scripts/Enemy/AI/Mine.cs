using UnityEngine;

namespace Hullbreakers
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] Animator animator;
        static readonly int GoCrazy = Animator.StringToHash("GoCrazy");

        void OnTriggerEnter2D(Collider2D col)
        {
            animator.SetTrigger(GoCrazy);
        }
    }
}
