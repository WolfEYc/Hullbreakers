using TMPro;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(TMP_Text))]
    public class TierText : MonoBehaviour
    {
        TMP_Text _text;
        public Tier CurrentTier { get; private set; }

        [SerializeField] bool displayOnLowest;

        public enum Tier
        {
            F,
            D,
            C,
            B,
            A,
            S
        }

        void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void SetTier(Tier tier)
        {
            CurrentTier = tier;
            
            if (!displayOnLowest && CurrentTier == Tier.F)
            {
                _text.SetText("");
                return;
            }
            
            
            _text.SetText(CurrentTier.ToString());
        }
    }
}
