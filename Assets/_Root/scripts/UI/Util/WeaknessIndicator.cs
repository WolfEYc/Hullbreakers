using Michsky.UI.MTP;
using UnityEngine;

namespace Hullbreakers
{
    public class WeaknessIndicator : MonoBehaviour
    {
        public static WeaknessIndicator Inst { get; private set; }
    
        [SerializeField] StyleManager indicator;
        int _indicators;
    
        void Awake()
        {
            if (Inst == this) return;
        
            if (Inst != null)
            {
                Destroy(this);
                return;
            }

            Inst = this;
        }

        public void Toggle(bool on)
        {
            indicator.gameObject.SetActive(on);
        }
    }
}
