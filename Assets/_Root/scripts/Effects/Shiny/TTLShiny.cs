using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class TTLShiny : MonoBehaviour
    {
        Shiny _shiny;
        
        public float timeToLive;
        

        float _endtime;

        bool OutOfTime => Time.time > _endtime;

        WaitUntil _waitTime;

        bool _on;
        
        void Awake()
        {
            _shiny = GetComponent<Shiny>();
            _waitTime = new WaitUntil(() => OutOfTime);
        }
        void Start()
        {
            StartTimer();
        }

        void OnDisable()
        {
            _on = false;
        }

        void StartTimer()
        {
            _endtime = Time.time + timeToLive;
            if (_on) return;
            StartCoroutine(Die());
        }

        public void StartTimer(float time)
        {
            _endtime = Time.time + time;
            
            if (_on) return;
            StartCoroutine(Die());
        }

        IEnumerator Die()
        {
            _on = true;
            _shiny.ToggleShinyOn();
            yield return _waitTime;
            _shiny.ToggleShinyOff();
            _on = false;
        }
        

    }
}
