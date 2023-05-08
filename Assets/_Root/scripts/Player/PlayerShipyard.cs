using UnityEngine;

namespace Hullbreakers
{
    public class PlayerShipyard : Singleton<PlayerShipyard>
    {
        [SerializeField] ShipScriptable[] playerShips;

        int _idx = -1;

        public GameObject NextShip()
        {
            _idx++;
            if (_idx >= playerShips.Length)
            {
                _idx = 0;
            }

            return playerShips[_idx].shipPrefab;
        }
        
        public int Idx => _idx;

        public int ShipCount => playerShips.Length;

        public bool LastShip => playerShips.Length == _idx + 1;

        public ShipScriptable Ship(int index)
        {
            return playerShips[index];
        }

        public void CleanUp()
        {
            _idx = -1;
        }
        
    }
}
