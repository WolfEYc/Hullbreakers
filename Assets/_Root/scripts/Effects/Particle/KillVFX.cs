using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace Hullbreakers
{
    public class KillVFX : MonoBehaviour
    {
        ObjectPool<KillVFX> _myPool;
        protected VisualEffect vfx;
        protected AudioSource audioSource;
        Transform _transform;
        public GameObject GameObject { get; private set; }
        
        WaitForSeconds _waitForDuration;
        
        static readonly int DurationID = Shader.PropertyToID("Duration");
        public static readonly int ColorID = Shader.PropertyToID("Color");

        public void SetPool(ObjectPool<KillVFX> pool)
        {
            _myPool = pool;
        }

        void SetColor(Color color)
        {
            vfx.SetVector4(ColorID, color);
        }
        
        void Awake()
        {
            _transform = transform;
            GameObject = gameObject;
            vfx = GetComponent<VisualEffect>();
            audioSource = GetComponent<AudioSource>();
            _waitForDuration = new WaitForSeconds(GetDuration());
        }

        void OnEnable()
        {
            StartCoroutine(KillMe());
        }

        float GetDuration()
        {
            return vfx.GetFloat(DurationID);
        }
        
        IEnumerator KillMe()
        {
            yield return _waitForDuration;
            _myPool.Release(this);
        }

        public void Spawn(Transform t, Color c)
        {
            _transform.SetPositionAndRotation(t.position, t.rotation);
            SetColor(c);
        }

        
    }
}
