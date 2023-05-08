using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Hullbreakers
{
    public abstract class Ability : MonoBehaviour
    {
        public float coolDown = 2f;
        public bool onCd = true;
    
        WaitForSeconds _waitToReset;
        IEnumerator _timeLeftUpdateEnumerator;

        public UnityEvent invoked;
        public UnityEvent offCd;

        float _invokedAt;

        public FillMeter cdIndicator;

        bool _indicate;

        protected virtual void Awake()
        {
            _waitToReset = new WaitForSeconds(coolDown);


            _indicate = cdIndicator != null;

            if (!_indicate) return;
            
            _timeLeftUpdateEnumerator = TimeLeftUpdate();
            cdIndicator.SetMaxValue(coolDown);
        }

        void Start()
        {
            if (!onCd)
            {
                if (_indicate)
                {
                    cdIndicator.SetValueImmediate(coolDown);
                }

                return;
            }

            if (_indicate)
            {
                cdIndicator.SetValueImmediate(0f);
                StartCoroutine(_timeLeftUpdateEnumerator);
            }

            StartCoroutine(ResetRoutine());
        }

        public void Use()
        {
            if(onCd) return;
            onCd = true;
            _invokedAt = Time.time;
            HandleUse();
            invoked.Invoke();
            StartCoroutine(ResetRoutine());

            if (!_indicate) return;
            cdIndicator.SetValueImmediate(0f);
            StartCoroutine(_timeLeftUpdateEnumerator);
        }

        protected abstract void HandleUse();


        IEnumerator TimeLeftUpdate()
        {
            while (true)
            {
                yield return null;
                cdIndicator.SetValue(Time.time - _invokedAt);
            }
        }

        IEnumerator ResetRoutine()
        {
            yield return _waitToReset;
            onCd = false;
            offCd.Invoke();

            if (!_indicate) yield break;
            
            StopCoroutine(_timeLeftUpdateEnumerator);
            cdIndicator.SetValueImmediate(coolDown);
        }
    }
}
