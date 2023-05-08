using UnityEngine;

namespace Hullbreakers
{
    public class ColorSetter : MonoBehaviour
    {
        public ParticleSystem ps;
        public SpriteRenderer spriteRenderer;

        ParticleSystem.MainModule _main;
    
        public void SetColor(Color color)
        {
            _main = ps.main;
            _main.startColor = color;
            spriteRenderer.color = color;
        }
    }
}
