using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Hullbreakers
{
    public class DashEffect : MonoBehaviour, IShiny
    {
        [SerializeField] SpriteRenderer sp;
        [SerializeField] VisualEffect vfx;

        VFXEventAttribute _eventAttribute;
        static readonly ExposedProperty MainTex = "MainTex";
        static readonly ExposedProperty RainbowMode = "PlayRainbow";
        
        void Awake()
        {
            vfx.SetTexture(MainTex, sp.sprite.texture);
            vfx.SetVector4(KillVFX.ColorID, sp.color);
        }

        bool _rainbow;
        
        public void Dash()
        {
            if (_rainbow)
            {
                vfx.SendEvent(RainbowMode, _eventAttribute);
            }
            else
            {
                vfx.Play();
            }
        }

        public void ToggleShinyOn()
        {
            _rainbow = true;
        }

        public void ToggleShinyOff()
        {
            _rainbow = false;
        }
    }
}

