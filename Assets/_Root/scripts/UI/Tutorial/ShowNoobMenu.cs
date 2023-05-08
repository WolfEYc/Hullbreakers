using UnityEngine;

namespace Hullbreakers
{
    public class ShowNoobMenu : MonoBehaviour
    {
        void Start()
        {
            NoobMenu.Instance.ShowNoob();
        }
    }
}
