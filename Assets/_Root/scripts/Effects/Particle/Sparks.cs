using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Hullbreakers
{
    public class Sparks : MonoBehaviour
    {
        VisualEffect _vfx;
        
        static readonly int ColorID = Shader.PropertyToID("Color");
        static readonly int DurationID = Shader.PropertyToID("Duration");
        WaitForSeconds _waitDuration;
        Transform _transform;
        
        
        void Awake()
        {
            _transform = transform;
            _vfx = GetComponent<VisualEffect>();
            _waitDuration = new WaitForSeconds(GetDuration());
        }
        
        void SetColor(Color color)
        {
            _vfx.SetVector4(ColorID, color);
        }

        float GetDuration()
        {
            return _vfx.GetFloat(DurationID);
        }

        IEnumerator ReleaseRoutine()
        {
            yield return _waitDuration;
            LightningPool.HitVFX.Release(this);
        }

        public void Spark(Vector2 pos, Color color)
        {
            _transform.position = pos;
            SetColor(color);
            _vfx.Play();
            StartCoroutine(ReleaseRoutine());
        }
    }
}
