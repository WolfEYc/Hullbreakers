using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    [RequireComponent(typeof(RawImage))]
    public class ShinyRawImage : MonoBehaviour, IShiny
    {
        [SerializeField] Material shinyMat;
        RawImage _self;
        Color _default;
        
        void Awake()
        {
            _self = GetComponent<RawImage>();
            _default = _self.color;
        }

        public void ToggleShinyOn()
        {
            _self.color = Color.white;
            _self.material = shinyMat;
        }

        public void ToggleShinyOff()
        {
            _self.color = _default;
            _self.material = null;
        }
    }
}
