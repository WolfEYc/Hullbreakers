using UnityEngine;

namespace Hullbreakers
{
    public class ShinySprite : MonoBehaviour, IShiny
    {
        SpriteRenderer _sp;
        Material _default;
        Color _defaultColor;
        [SerializeField] Material rainbowMaterial;
        [SerializeField] SpriteRenderer toCopyFrom;

        void Awake()
        {
            _sp = GetComponent<SpriteRenderer>();
            _default = _sp.material;
            _sp.color = toCopyFrom.color;
            _defaultColor = _sp.color;
        }

        public void ToggleShinyOn()
        {
            _sp.material = rainbowMaterial;
            _sp.color = Color.white;
        }

        public void ToggleShinyOff()
        {
            _sp.material = _default;
            _sp.color = _defaultColor;
        }
    }
}
