using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class PopUp : MonoBehaviour
    {
        enum PopUpState
        {
            Hidden,
            Hiding,
            Displayed,
            Displaying
        }
    
        public AudioSource playOnOpen;

        public float hangTime = -1f;
        public CanvasGroup cg;
        public Vector2 displayPos;
        public RectTransform modalTransform;
        public float timeToLerp;

        Vector2 _originalPos;

        IEnumerator _goTo, _hide;

        WaitForSeconds _hideAwaySeconds;
        [SerializeField] GameObject child;
        
        
        PopUpState _state;
    
        void Awake()
        {
            _hideAwaySeconds = new WaitForSeconds(hangTime);
            _originalPos = modalTransform.anchoredPosition;
            _state = PopUpState.Hidden;
        }

        public void Display()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
        
            switch (_state)
            {
                case PopUpState.Displaying or PopUpState.Displayed:
                    return;
                case PopUpState.Hiding:
                    StopCoroutine(_hide);
                    break;
            }
            
            child.SetActive(true);

            if (playOnOpen != null)
            {
                playOnOpen.Play();
            }
        
            _goTo = GoToDisplay();
            StartCoroutine(_goTo);
            _state = PopUpState.Displaying;
        }

        public void Hide()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
        
            switch (_state)
            {
                case PopUpState.Hidden or PopUpState.Hiding:
                    return;
                case PopUpState.Displaying:
                    StopCoroutine(_goTo);
                    break;
            }
        
            _hide = HideAway();
            StartCoroutine(_hide);
            _state = PopUpState.Hiding;
        }

        IEnumerator GoToDisplay()
        {
            float time = 0;
            float frac;
            while (time < timeToLerp)
            {
                frac = time / timeToLerp;
                modalTransform.anchoredPosition = Vector2.Lerp(_originalPos, displayPos, frac);
                cg.alpha = frac;
                time += Time.deltaTime;
                yield return null;
            }
            cg.alpha = 1;
            modalTransform.anchoredPosition = displayPos;
            _state = PopUpState.Displayed;
        
            if(hangTime < 0f) yield break;

            yield return _hideAwaySeconds;
        
            Hide();
        }
    
        IEnumerator HideAway()
        {
            float time = 0;
            float frac;
            while (time < timeToLerp)
            {
                frac = time / timeToLerp;
                modalTransform.anchoredPosition = Vector2.Lerp(displayPos, _originalPos, frac);
                cg.alpha = 1 - frac;
                time += Time.deltaTime;
                yield return null;
            }
            modalTransform.anchoredPosition = _originalPos;
            cg.alpha = 0;
            _state = PopUpState.Hidden;
            
            child.SetActive(false);
        }

        public void HideImmediately()
        {
            modalTransform.anchoredPosition = _originalPos;
            cg.alpha = 0;
            _state = PopUpState.Hidden;
            child.SetActive(false);
        }
    }
}
