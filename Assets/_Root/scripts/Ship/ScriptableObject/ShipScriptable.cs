using UnityEngine;

namespace Hullbreakers
{
    [CreateAssetMenu(fileName = "New Ship Data", menuName = "Ship Data", order = 1)]
    public class ShipScriptable : ScriptableObject
    {
        public GameObject shipPrefab;
        public Sprite shipSprite;
        public Color shipColor;
    }
}
