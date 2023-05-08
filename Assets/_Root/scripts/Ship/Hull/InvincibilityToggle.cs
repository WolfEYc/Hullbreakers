using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class InvincibilityToggle : MonoBehaviour
    {
        public float starTime;
        public Hull hull;
    
        void Start()
        {
            if(GameMaster.Testing) return;
            StartCoroutine(TurnOffInvincible());
        }

        IEnumerator TurnOffInvincible()
        {
            yield return new WaitForSeconds(starTime);
            hull.invincible = false;
        }
    }
}
