using System.Collections;
using UnityEngine;
using UnityEngine.VFX;


namespace Hullbreakers
{
    [RequireComponent(typeof(VisualEffect), typeof(AudioSource))]
    public class Lightning : MonoBehaviour
    {
        [SerializeField] LayerMask player, enemies;
        
        VisualEffect _lightningEffect;
        static readonly int LengthID = Shader.PropertyToID("Length");
        static readonly int ColorID = Shader.PropertyToID("Color");
        
        static readonly int BoltLifeTimeID = Shader.PropertyToID("BoltLifeTime");
        static readonly int ImpactDelayID = Shader.PropertyToID("ImpactDelay");
        static readonly int GlowLifeTimeID = Shader.PropertyToID("GlowLifeTime");

        WaitForSeconds _waitForDuration;

        AudioSource _audio;

        Transform _transform;

        ContactFilter2D _contactFilter2D;

        RaycastHit2D[] _results;

        const int Pierce = 5;

        Color _color;
        Vector2 _target;
        float _dmg;
        bool _playerStrike;

        void Awake()
        {
            _transform = transform;
            _lightningEffect = GetComponent<VisualEffect>();
            _audio = GetComponent<AudioSource>();
            _waitForDuration = new WaitForSeconds(GetDuration());
            _results = new RaycastHit2D[Pierce];
            _contactFilter2D.useLayerMask = true;
        }

        float GetDuration()
        {
            return _lightningEffect.GetFloat(BoltLifeTimeID) * _lightningEffect.GetFloat(ImpactDelayID) +
                   _lightningEffect.GetFloat(GlowLifeTimeID);
        }
        
        void SetLength(float len)
        {
            _lightningEffect.SetFloat(LengthID, len);
        }

        void SetColor(Color color)
        {
            _color = color;
            _lightningEffect.SetVector4(ColorID, _color);
        }
        
        void SetImpactPos(Vector2 pos)
        {
            var position = _transform.position;
            _transform.rotation = Rotation.LookAtQuaternion(position, pos);
            SetLength(Vector2.Distance(position, pos));
        }
        
        void HandleStrike(Vector2 origin, Vector2 target, Color color, float dmg, bool asPlayer, float vol)
        {
            _transform.position = origin;
            _target = target;
            SetColor(color);
            _dmg = dmg;
            _playerStrike = asPlayer;
            _audio.volume = vol;
            
            PhysicalFX();
            StrikeFX();
        }

        void StrikeFX()
        {
            SetImpactPos(_target);
            _lightningEffect.Play();
            _audio.Play();
            StartCoroutine(Release());
        }

        void PhysicalFX()
        {
            _contactFilter2D.layerMask = _playerStrike ? enemies : player;
            var position = (Vector2)_transform.position;
            Vector2 dir = _target - position;

            int resultsSize = Physics2D.Raycast(position, dir.normalized, _contactFilter2D, _results, dir.magnitude);

            for (int i = 0; i < resultsSize; i++)
            {
                float resultDmg = _results[i].collider.GetComponent<IDamageable>().Damage(_dmg, Vector2.zero);
                
                //Debug.Log(_results[i].collider.gameObject.name);

                if (_playerStrike)
                {
                    GameMaster.Inst.DamageAtLocation(resultDmg, _results[i].point);
                }
                
                SpawnSparks(_results[i].point);
            }
        }

        void SpawnSparks(Vector2 pos)
        {
            LightningPool.HitVFX.Get().Spark(pos, _color);
        }

        IEnumerator Release()
        {
            yield return _waitForDuration;
            LightningPool.Pool.Release(this);
        }

        public static void Strike(Vector2 origin, Vector2 target, Color color, float dmg = 0f, bool asPlayer = false, float vol = 0.5f)
        {
            LightningPool.Pool.Get().HandleStrike(origin, target, color, dmg, asPlayer, vol);
        }

        public static Vector2 ClampImpact(Vector2 origin, Vector2 target, float radius)
        {
            return origin + Vector2.ClampMagnitude(target - origin, radius);
        }
    }
}
