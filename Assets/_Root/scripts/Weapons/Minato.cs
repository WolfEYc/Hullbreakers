using System;
using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class Minato : WeaponBase
    {
        [SerializeField] Rotation rotation;
        [SerializeField] LightningBall shurikenPrefab;
        [SerializeField] float range;
        [SerializeField] float portDelay;

        int _idx;

        bool _rainbow;

        WaitForSeconds _waitForPort;
        LightningBall[] _shuriken;

        protected override void Awake()
        {
            base.Awake();
            _shuriken = new LightningBall[pierce];
            _waitForPort = new WaitForSeconds(portDelay);
            
            
            for(int i = 0; i < _shuriken.Length; i++)
            {
                _shuriken[i] = Instantiate(shurikenPrefab);
            }
        }

        Color GetColor()
        {
            return _rainbow ? CPURainbow.Color : spriteRenderer.color;
        }

        public override void Shoot()
        {
            var position = rb.position;
            var color = GetColor();
            
            Vector2 impactPos = position + (Vector2)shotPointTransform.up * Mathf.Clamp(Vector2.Distance(rotation.target, position), 0f, range);
            
            _shuriken[_idx % _shuriken.Length].Shoot(impactPos, color);
            
            Lightning.Strike(shotPointTransform.position, impactPos, color, dmgMultiplier, amPlayer);

            _idx++;

            StartCoroutine(ChainRoutine());
        }

        public override void ToggleShinyOn()
        {
            _rainbow = true;
        }

        public override void ToggleShinyOff()
        {
            _rainbow = true;
        }
        

        IEnumerator ChainRoutine()
        {
            int max = Math.Min(_idx - 1, _shuriken.Length - 1);
            int idxCpy = _idx;
            
            for(int i = 0; i < max ; i++)
            {
                idxCpy--;
                
                Lightning.Strike(_shuriken[idxCpy% _shuriken.Length].Pos(), _shuriken[(idxCpy - 1) % _shuriken.Length].Pos(), spriteRenderer.color, dmgMultiplier, amPlayer);
                
                yield return _waitForPort;
            }
            
        }

        void OnDestroy()
        {
            foreach (LightningBall lightningBall in _shuriken)
            {
                if (lightningBall != null)
                {
                    Destroy(lightningBall.gameObject);
                }
            }
        }
    }
}
