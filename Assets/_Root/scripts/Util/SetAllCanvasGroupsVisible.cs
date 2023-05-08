using UnityEngine;

namespace Hullbreakers
{
    public class SetAllCanvasGroupsVisible : MonoBehaviour
    {
        CanvasGroup[] _canvasGroups;

        void Awake()
        {
            _canvasGroups = GetComponentsInChildren<CanvasGroup>();
        }

        public void SetCanvasGroups(float alpha)
        {
            foreach (CanvasGroup canvasGroup in _canvasGroups)
            {
                canvasGroup.alpha = alpha;
            }
        }
    }
}
