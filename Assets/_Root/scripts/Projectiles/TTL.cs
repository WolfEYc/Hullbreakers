using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class TTL : MonoBehaviour
    {
        public WaitForSeconds ttl;
        
        public IEnumerator DieRoutine { get; private set; }
        [HideInInspector] public GameObject gObject;
        public Transform SelfTransform { get; private set; }
        
        protected virtual void Awake()
        {
            gObject = gameObject;
            SelfTransform = transform;
        }

        public void StartTimer()
        {
            StartCoroutine(DieRoutine = Die());
        }

        IEnumerator Die()
        {
            yield return ttl;
            HandleDeath();
        }

        public virtual void HandleDeath()
        {
            Destroy(gObject);
        }
    }
}
