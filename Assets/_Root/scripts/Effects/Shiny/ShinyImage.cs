using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    public class ShinyImage : MonoBehaviour
    {
        public Image image;
        Color _oldColor;

        void Awake()
        {
            _oldColor = image.color;
        }

        void FixedUpdate()
        {
            image.color = CPURainbow.Color;
        }

        void OnDisable()
        {
            image.color = _oldColor;
        }
    }
}
