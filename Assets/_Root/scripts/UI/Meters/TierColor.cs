using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    [RequireComponent(typeof(Image), typeof(ShinyImage))]
    public class TierColor : MonoBehaviour
    {
        [SerializeField] TMP_Text tierText;
        
        
        [SerializeField] Color[] colors;
        Image _image;
        ShinyImage _shinyImage;
        bool _rainbow;

        [SerializeField] TMP_FontAsset rainbowFont;
        TMP_FontAsset _defaultFont;
        
        void Awake()
        {
            _image = GetComponent<Image>();
            _shinyImage = GetComponent<ShinyImage>();
            _defaultFont = tierText.font;
        }

        public void SetTier(TierText.Tier tier)
        {
            if (tier == TierText.Tier.S)
            {
                SetRainbow();
                return;
            }

            if (_rainbow)
            {
                RemoveRainbow();
            }
            
            _image.color = colors[(int)tier];
            tierText.color = colors[(int)tier];
        }

        void SetRainbow()
        {
            _rainbow = true;
            _shinyImage.enabled = true;
            tierText.color = Color.white;
            tierText.font = rainbowFont;
        }

        void RemoveRainbow()
        {
            _rainbow = false;
            _shinyImage.enabled = false;
            tierText.font = _defaultFont;
        }
    }
}
