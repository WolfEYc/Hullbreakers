using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    public class DifficultyDefault : MonoBehaviour
    {
        public Image[] spriteRenderers;

        int _prev;
        Color[] _colors;

        void Awake()
        {
            _colors = new Color[spriteRenderers.Length];
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                _colors[i] = spriteRenderers[i].color;
                spriteRenderers[i].color = Color.white;
            }
        }

        void Start()
        {
            SelectDifficultyBtn();
        }

        public void SelectDifficultyBtn()
        {
            spriteRenderers[_prev].color = Color.white;
            _prev = (int)GameMaster.Inst.difficulty;
            spriteRenderers[_prev].color = _colors[_prev];
        }
    }
}
