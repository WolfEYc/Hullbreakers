using System;
using UnityEngine;

namespace Hullbreakers
{
    public class AimbotShot : MonoBehaviour
    {
        [Serializable]
        public struct AimbotData
        {
            public float accel;
            public float maxVelocity;
            public Rigidbody2D rb;
        }
        
        [SerializeField] GameObject gobj;

        protected Vector2 diff;
        bool _targetPlayer;
        
        public void SetAimbot(AimbotData newdata)
        {
            
            if (GameMaster.Inst.CurrentState != GameMaster.GameState.InGame)
            {
                return;
            }
            
            gobj.SetActive(true);
            data = newdata;
            _targetPlayer = data.rb == null;
            _cachedpos = rb.position;
        }

        Vector2 _cachedpos;

        [SerializeField] AimbotData data;
        [SerializeField] Rigidbody2D rb;

        void Awake()
        {
            gobj.layer = rb.gameObject.layer;
        }

        void OnEnable()
        {
            GameMaster.Inst.gameEnd.AddListener(DisableThis);
        }

        void OnDisable()
        {
            GameMaster.Inst.gameEnd.RemoveListener(DisableThis);
            gobj.SetActive(false);
        }

        void DisableThis()
        {
            gobj.SetActive(false);
        }

        void FixedUpdate()
        {
            if (GameMaster.Inst.CurrentState != GameMaster.GameState.InGame)
            {
                DisableThis();
                return;
            }
            
            diff = (_targetPlayer ? GameMaster.Inst.PlayerRb.position : _cachedpos) - rb.position;
            diff.Normalize();
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + diff * (data.accel * Time.fixedDeltaTime), data.maxVelocity);
        }
    }
}
