using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Hull))]
    public class HpScale : MonoBehaviour
    {
        public float scalar = .1f;
        Hull _hull;

        void Awake()
        {
            _hull = GetComponent<Hull>();
        }

        void OnEnable()
        {
            _hull.SetMaxHp(_hull.maxHp * (1f + scalar * GameMaster.Inst.Wave));
        }
    
    }
}
