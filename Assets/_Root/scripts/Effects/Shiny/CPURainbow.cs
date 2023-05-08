using UnityEngine;

namespace Hullbreakers
{
    public class CPURainbow : MonoBehaviour
    {
        [SerializeField] Material textMat;

        static readonly int TextColor = Shader.PropertyToID("_FaceColor");
        
        static CPURainbow _inst;
        const float Speed = 7f;
        const float TextIntensity = 2f;
        const float TwoThirdsPi = 2f * Mathf.PI / 3f;
        const float FourThirdsPi = 2f * TwoThirdsPi;

        void Awake()
        {
            if(_inst == this) return;

            if (_inst != null)
            {
                Destroy(this);
                return;
            }

            _inst = this;
        }

        Color _color = Color.white;

        void FixedUpdate()
        {
            float scaledTime = Time.time * Speed;
            
            _color.r = Mathf.Sin(scaledTime).Remap(-1, 1, 0, 1);
            _color.g = Mathf.Sin(scaledTime + TwoThirdsPi).Remap(-1, 1, 0, 1);
            _color.b = Mathf.Sin(scaledTime + FourThirdsPi).Remap(-1, 1, 0, 1);

            UpdateMats();
        }

        void UpdateMats()
        {
            textMat.SetColor(TextColor, _color * TextIntensity);
        }
        
        public static Color Color => _inst._color;
    }
    
    public static class ExtensionMethods {
 
        public static float Remap (this float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}
