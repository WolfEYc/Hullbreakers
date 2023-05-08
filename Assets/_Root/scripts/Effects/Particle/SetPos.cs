using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Hullbreakers
{
    public class SetPos : MonoBehaviour, IShiny
    {
        VisualEffect _vfx;
        VFXEventAttribute _eventAttribute;
        [SerializeField] SpriteRenderer visuals;

        static readonly ExposedProperty RainbowMode = "RainbowMode";

        bool _rainbow;
        bool _on;
        
        void Awake()
        {
            _vfx = GetComponent<VisualEffect>();
            SetColor(visuals.color);
        }
        
        public void SetColor(Color color)
        {
            _vfx.SetVector4(KillVFX.ColorID, color);
        }

        public void Toggle(bool on)
        {
            if(_on == on) return;
            _on = on;
            
            UpdateVFX();
        }

        public void ToggleRainbow(bool on)
        {
            if(_rainbow == on) return;
            _rainbow = on;

            if (_on)
            {
                UpdateVFX();
            }
        }

        void UpdateVFX()
        {
            if (_on)
            {
                if (_rainbow)
                {
                    _vfx.SendEvent(RainbowMode, _eventAttribute);
                }
                else
                {
                    _vfx.Play();
                }
            }
            else
            {
                _vfx.Stop();
            }
        }

        public void ToggleShinyOn()
        {
            ToggleRainbow(true);
        }

        public void ToggleShinyOff()
        {
            ToggleRainbow(false);
        }
    }
}
