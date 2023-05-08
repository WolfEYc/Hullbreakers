using TMPro;
using UnityEngine;

namespace Hullbreakers
{
    public class HighScoreTitleText : MonoBehaviour
    {
        public TMP_Text text;
    
        void OnEnable()
        {
            text.SetText($"HighScores ({GameMaster.Inst.difficulty.ToString()})");
        }
    }
}
