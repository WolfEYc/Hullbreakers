using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class Weakness : MonoBehaviour
    {
        [SerializeField] float wait2Show;
        [SerializeField] float hangtime;
        [SerializeField] SpriteRenderer outline;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(wait2Show);
            outline.enabled = true;
        
            WeaknessIndicator.Inst.Toggle(true);
            yield return new WaitForSeconds(hangtime);
            Destroy(gameObject);
        }
    }
}
