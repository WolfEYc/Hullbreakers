using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Hullbreakers
{
    public class ScreenPort : MonoBehaviour
    {
        [SerializeField] UnityEvent becameInRange;
        [SerializeField] UnityEvent prePort;
        [SerializeField] float force2Mid;
    
        Rigidbody2D _rb;
    
        bool OutLeft => _rb.position.x < GameMaster.Inst.Origin.x;
        bool OutRight => _rb.position.x > GameMaster.Inst.Screen2World.x;
        bool OutBottom => _rb.position.y < GameMaster.Inst.Origin.y;
        bool OutTop => _rb.position.y > GameMaster.Inst.Screen2World.y;

        bool InRange => !OutLeft && !OutRight && !OutBottom && !OutTop;

        public bool portInRange;
        [SerializeField] bool shouldForceToMid = true;

        [SerializeField] bool canTeleport = true;

        bool _ported;

        WaitForFixedUpdate _waitForFixedUpdate;
    
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _waitForFixedUpdate = new WaitForFixedUpdate();
        }

        void OnEnable()
        {
            if (portInRange && canTeleport)
            {
                StartCoroutine(PortInRange());
                return;
            }
            
            StartCoroutine(ForceToMidRoutine());
        }
    
        void ForceToMid()
        {
            if (shouldForceToMid)
            {
                _rb.AddForce(-_rb.position * force2Mid);
            }
        }

        IEnumerator ForceToMidRoutine()
        {
            while (!InRange)
            {
                ForceToMid();
                yield return _waitForFixedUpdate;
            }
        
            becameInRange.Invoke();

            if (canTeleport)
            {
                StartCoroutine(PortInRange());
            }
        }

        IEnumerator PortInRange()
        {
            while (true)
            {
                yield return _waitForFixedUpdate;
                if (_ported)
                {
                    prePort.Invoke();
                }
                _ported = false;
        
                if (OutLeft)
                {
                    _rb.position = new Vector2(GameMaster.Inst.Screen2World.x, _rb.position.y);
                    _ported = true;
                    continue;
                }
        
                if (OutRight)
                {
                    _rb.position = new Vector2(GameMaster.Inst.Origin.x, _rb.position.y);
                    _ported = true;
                    continue;
                }
        
                if (OutBottom)
                {
                    _rb.position = new Vector2(_rb.position.x, GameMaster.Inst.Screen2World.y);
                    _ported = true;
                    continue;
                }
        
                if (OutTop)
                {
                    _rb.position = new Vector2(_rb.position.x, GameMaster.Inst.Origin.y);
                    _ported = true;
                    continue;
                }
            }
        }
    }
}
