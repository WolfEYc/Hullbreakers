using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    public class UISignal : MonoBehaviour
    {
        public Image image;
        public Color color;
        public float interval;

        WaitForSeconds _waitToToggle;

        Color _originalColor;
        bool _toggle;

        void Awake()
        {
            _waitToToggle = new WaitForSeconds(interval);
            _originalColor = image.color;
        }

        void OnEnable()
        {
            StartCoroutine(ToggleColor());
        }
    
        IEnumerator ToggleColor()
        {
            while (true)
            {
                yield return _waitToToggle;
                image.color = _toggle ? _originalColor : color;
                _toggle = !_toggle;
            }
        }
    }
}
