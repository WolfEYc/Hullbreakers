using TMPro;
using UnityEngine;

namespace Hullbreakers
{
    public class AmtCounter : MonoBehaviour
    {
        [SerializeField] int min, max;
        [SerializeField] bool displayOnMin;
        [SerializeField] TMP_Text text;


        public int Max => max;
        
        public void SetAmt(int amt)
        {
            gameObject.SetActive(true);
            
            if (amt > max)
            {
                text.SetText($"{max}+");
                return;
            }

            if (amt <= min && displayOnMin == false)
            {
                gameObject.SetActive(false);
                return;
            }
            
            text.SetText($"{amt}");
        }
    }
}
