using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class TargetMoveCycler : AI
    {
        [SerializeField] TargetPlayer targetPlayer;
        [SerializeField] float targetTime;
        [SerializeField] float thrustTime;

        WaitForSeconds _targeting, _thrusting;

        protected override void Awake()
        {
            base.Awake();
            _targeting = new WaitForSeconds(targetTime);
            _thrusting = new WaitForSeconds(thrustTime);
        }

        IEnumerator Start()
        {
            while (enabled)
            {
                movement.ToggleThrust(true);
                targetPlayer.enabled = false;
                
                yield return _thrusting;
                
                if(!enabled) yield break;

                movement.ToggleThrust(false);
                targetPlayer.enabled = true;

                yield return _targeting;
            }
        }
        
        
    }
}
