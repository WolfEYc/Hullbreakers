using System;
using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public abstract class Orb : MonoBehaviour
    {
        public static event Action OrbSpawned;
        
        
        public Rigidbody2D me;
        public TTLOrb ttlOrb;
        public GameObject Self { get; private set; }

        bool _triggered;
        public float threshold;
        public float accel;
        
        public float friction;
        bool _playerDead;
        
        float _thresholdSqrd;

        Vector2 Diff => GameMaster.Inst.PlayerRb.position - me.position;
        bool InThreshold => Diff.sqrMagnitude < _thresholdSqrd;

        Collider2D _collider;

        const float Waitseconds = 0.5f;
        static readonly WaitForSeconds WaittoPickup = new(Waitseconds);

        static readonly WaitForFixedUpdate WaitForFixedUpdate = new();

        const float AdditionalVelocity = 2f;
        
        float _maxVelocity;

        void Awake()
        {
            _collider = GetComponent<Collider2D>();
            
            Self = gameObject;
            _thresholdSqrd = threshold * threshold;
        }

        IEnumerator TriggeredRoutine()
        {
            while (enabled)
            {
                me.velocity = Vector2.ClampMagnitude(me.velocity + Diff.normalized * (accel * Time.fixedDeltaTime), _maxVelocity);
                
                yield return WaitForFixedUpdate;
            }
        }

        IEnumerator FrictionRoutine()
        {
            while (!_triggered)
            {
                me.velocity = Vector2.Lerp(me.velocity, Vector2.zero, friction * Time.fixedDeltaTime);
                _triggered = InThreshold;
                yield return WaitForFixedUpdate;
            }
            
            StartCoroutine(TriggeredRoutine());
        }

        IEnumerator PickupRoutine()
        {
            yield return WaittoPickup;
            _collider.enabled = true;
            
            
            StartCoroutine(FrictionRoutine());
        }

        void OnEnable()
        {
            if (GameMaster.Inst.CurrentState != GameMaster.GameState.InGame)
            {
                _playerDead = true;
                return;
            }
            
            GameMaster.Inst.gameEnd.AddListener(PlayerDead);

            _maxVelocity = GameMaster.Inst.PlayerInstance.GetComponent<Movement>().maxVeloclity + AdditionalVelocity;
            
            OrbSpawned?.Invoke();

            StartCoroutine(PickupRoutine());
        }

        void OnDisable()
        {
            _triggered = false;
            _collider.enabled = false;
            
            if(_playerDead) return;
            GameMaster.Inst.gameEnd.RemoveListener(PlayerDead);
        }
    
        void PlayerDead()
        {
            ttlOrb.HandleDeath();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            OnPickup(col);
            ttlOrb.HandleDeath();
        }

        protected abstract void OnPickup(Collider2D col);
        
    }
}
