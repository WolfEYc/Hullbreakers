using UnityEngine;

namespace Hullbreakers
{
    public class DisableOnOut : StateMachineBehaviour
    {

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.gameObject.SetActive(false);
        }

        
    }
}
