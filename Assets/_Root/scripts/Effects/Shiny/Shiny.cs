using UnityEngine;

namespace Hullbreakers
{
    public class Shiny : MonoBehaviour
    {
        IShiny[] _shinies;

        void Awake()
        {
            _shinies = GetComponentsInChildren<IShiny>();
        }

        public void ToggleShinyOn()
        {
            foreach (IShiny shiny in _shinies)
            {
                shiny.ToggleShinyOn();
            }
        }

        public void ToggleShinyOff()
        {
            foreach (IShiny shiny in _shinies)
            {
                shiny.ToggleShinyOff();
            }
        }
    }
}
