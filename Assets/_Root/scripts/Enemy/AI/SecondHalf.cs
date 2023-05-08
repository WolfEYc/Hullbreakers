using UnityEngine;
using UnityEngine.Events;

namespace Hullbreakers
{
    public class SecondHalf : MonoBehaviour
    {
        Hull _hull;
        float _maxHp;
        [SerializeField] UnityEvent secondHalf;

        void Awake()
        {
            _hull = GetComponent<Hull>();
            _hull.hpUpdate += HpUpdate;
            _hull.maxHpUpdate += MaxHpUpdate;
        }

        void HpUpdate(float newHp)
        {
            if(newHp * 2 > _maxHp) return;
        
            _hull.hpUpdate -= HpUpdate;
            _hull.maxHpUpdate -= MaxHpUpdate;
        
            secondHalf.Invoke();
        }

        void MaxHpUpdate(float newHp)
        {
            _maxHp = newHp;
        }
    }
}
