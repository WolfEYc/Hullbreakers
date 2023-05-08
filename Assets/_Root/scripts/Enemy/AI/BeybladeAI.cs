using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class BeybladeAI : AI
    {
        public float moveTime;
        public float shootTime;
    
        public float rotSpeed;

        WaitForSeconds _moveSeconds, _shootSeconds;

        float _degs;
    
        protected override void Awake()
        {
            base.Awake();
            _moveSeconds = new WaitForSeconds(moveTime);
            _shootSeconds = new WaitForSeconds(shootTime);
        }

        void Start()
        {
            StartCoroutine(MovementCycle());
        }


        IEnumerator MovementCycle()
        {
            while (true)
            {
                movement.ToggleThrust(true);
                if (GameMaster.Inst.CurrentState == GameMaster.GameState.InGame)
                {
                    rotation.target = PlayerRb.position;
                }

                yield return _moveSeconds;
            
                movement.ToggleThrust(false);
                rotation.enabled = false;
                rb.angularVelocity = rotSpeed;
                ToggleShooting(true);
                yield return _shootSeconds;
                // ReSharper disable once Unity.InefficientPropertyAccess
                rb.angularVelocity = 0f;
                ToggleShooting(false);
            }
        }
    
    
    }
}
