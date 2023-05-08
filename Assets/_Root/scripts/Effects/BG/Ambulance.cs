using UnityEngine;


namespace Hullbreakers
{
    [RequireComponent(typeof(Animator))]
    public class Ambulance : Singleton<Ambulance>
    {
        [SerializeField] float threshold;
        bool _active;

        void Start()
        {
            gameObject.SetActive(false);
            GameMaster.Inst.gameEnd.AddListener(TurnOff);
        }

        public void HandleHpUpdate(float newHp)
        {
            SetActive(newHp < threshold);
        }

        void TurnOff()
        {
            SetActive(false);
        }

        void SetActive(bool active)
        {
            if(_active == active) return;
            _active = active;
            
            gameObject.SetActive(_active);

        }
    }
}
