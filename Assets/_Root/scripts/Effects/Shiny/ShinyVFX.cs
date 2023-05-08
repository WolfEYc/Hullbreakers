using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Hullbreakers
{
    [RequireComponent(typeof(VisualEffect))]
    public class ShinyVFX : MonoBehaviour, IShiny
    {
        static readonly ExposedProperty RainbowMode = "RainbowMode";
        
        VFXEventAttribute _eventAttribute;

        VisualEffect _vfx;

        void Awake()
        {
            _vfx = GetComponent<VisualEffect>();
        }

        bool _rainbow;
        bool _playing;
        
        public void ToggleShinyOn()
        {
            _rainbow = true;
            if (_playing)
            {
                _vfx.Stop();
                _vfx.SendEvent(RainbowMode, _eventAttribute);
            }
        }

        public void SetColor(Color color)
        {
            _vfx.SetVector4(KillVFX.ColorID, color);
        }

        public void ToggleShinyOff()
        {
            _rainbow = false;
            if (_playing)
            {
                _vfx.Stop();
                _vfx.Play();
            }
        }

        public void Play()
        {
            _playing = true;
            _vfx.enabled = true;
            if (_rainbow)
            {
                _vfx.SendEvent(RainbowMode, _eventAttribute);
            }
            else
            {
                _vfx.Play();
            }
        }

        public void Stop()
        {
            _playing = false;
            _vfx.enabled = false;
            _vfx.Stop();
        }
        
        
    }
}
