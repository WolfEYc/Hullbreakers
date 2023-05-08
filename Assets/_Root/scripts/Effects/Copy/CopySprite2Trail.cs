using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(TrailRenderer))]
    public class CopySprite2Trail : MonoBehaviour, IShiny
    {
        TrailRenderer _trailRenderer;
        Gradient _gradient;
        [SerializeField] Material shinyMat;
        [SerializeField] SpriteRenderer spriteRenderer;

        Material _default;
        
        void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            _default = _trailRenderer.material;
            _gradient = new Gradient();

            var colorGradient = _trailRenderer.colorGradient;
            _gradient.SetKeys(colorGradient.colorKeys, colorGradient.alphaKeys);
            SetColor(spriteRenderer.color);
        }

        public void SetColor(Color color)
        {
            GradientColorKey[] colorKeys = _gradient.colorKeys;

            colorKeys[0].color = color;
            colorKeys[^1].color = color;

            _gradient.colorKeys = colorKeys;
            _trailRenderer.colorGradient = _gradient;
        }

        public void ToggleShinyOn()
        {
            SetColor(Color.white);
            _trailRenderer.material = shinyMat;
        }

        public void ToggleShinyOff()
        {
            SetColor(spriteRenderer.color);
            _trailRenderer.material = _default;
        }
    }
}
