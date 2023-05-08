using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Animator))]
    public class ToggleAnimation : MonoBehaviour
    {

        Animator _animator;
        static readonly int Enabled = Animator.StringToHash("toggle");
        
        
        void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void Toggle()
        {
            _animator.SetTrigger(Enabled);
        }
    }
}
