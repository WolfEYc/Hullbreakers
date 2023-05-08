using UnityEngine;
using UnityEngine.Events;

namespace Hullbreakers
{
    public class LevelUp : MonoBehaviour
    {
        public UnityEvent leveledUp;
        
        InputMaster _inputMaster;
    
        LevelMaster _levelMaster;
        
        bool _lock;

        [SerializeField] TTLShiny ttlShiny;

        const float Shinytime = 0.5f;
        
        void Awake()
        {
            _levelMaster = GameMaster.Inst.lvlMaster;
        }


        public void PerformLevelUp()
        {
            if (_lock || !_levelMaster.CanLevelUp()) return;
            _lock = true;
            
            leveledUp.Invoke();

            _levelMaster.LevelUp();   
        }
    
        public void AddXp(int xp)
        {
            _levelMaster.IncreaseScore(xp);
            
            ttlShiny.StartTimer(Shinytime);
        }
    }
}
