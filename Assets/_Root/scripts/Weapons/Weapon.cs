using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Hullbreakers
{
    public class Weapon : WeaponBase, IShiny
    {
        public int projectiles;
    
        public float speed;
    
        public float spread;
    
        public float timeToLive;

        public Vector3 scale = new(0.05f, 0.1f, 1f);

        [SerializeField] bool aimbotEnabled;

        public AimbotShot.AimbotData aimbotData;
        
        ObjectPool<Damager> _projectilePool;
        Vector3 _rot;
        
        
        [SerializeField] VisualEffect muzzleFlash;
        [SerializeField] Sprite projectileSprite;

        [SerializeField] bool trail;

        [SerializeField] bool useSpriteRenderer = true;

        VFXEventAttribute _eventAttribute;
        static readonly ExposedProperty RainbowMode = "PlayRainbow";
        static readonly ExposedProperty Velocity = "Velocity";

        WaitForSeconds _ttl;
        
        bool _muzzleFlash;
        
        bool _musicContinuous;

        bool _rainbow;

        Color _color;

        protected override void Awake()
        {
            base.Awake();
        
            _projectilePool = amPlayer ?
                GameMaster.Inst.PlayerPPool
                : GameMaster.Inst.EnemyPPool;

            _muzzleFlash = muzzleFlash != null;

            if (music)
            {
                _musicContinuous = playOnShot.loop;
            }

            _color = useSpriteRenderer ? spriteRenderer.color : Color.white;
            
            if (_muzzleFlash)
            {
                muzzleFlash.SetVector4(KillVFX.ColorID, spriteRenderer.color);
            }

            _ttl = new WaitForSeconds(timeToLive);
        }

        public override void ToggleShooting(bool on)
        {
            base.ToggleShooting(on);
            if(!_musicContinuous) return;
            if (on)
            {
                playOnShot.Play();
            }
            else
            {
                playOnShot.Stop();
            }
        }

        Color GetColor()
        {
            return _rainbow ? CPURainbow.Color : _color;
        }

        public void ToggleRainbow(bool rainbow)
        {
            _rainbow = rainbow;
        }

        public override void Shoot()
        {
            float half = (projectiles - 1) / 2f;
            _rot = shotPointTransform.eulerAngles;

            var color = GetColor();
            
            for (int i = 0; i < projectiles; i++)
            {
                float diff = i - half;
                float prevz = _rot.z;
                _rot.z += diff * spread;

                Damager dmger = _projectilePool.Get();
            
                dmger.SelfTransform.eulerAngles = _rot;
                _rot.z = prevz;
            
                dmger.SelfTransform.position = shotPointTransform.position;
            
                dmger.SetMultiplier(dmgMultiplier);
                dmger.rb.velocity = rb.velocity + (Vector2)dmger.SelfTransform.up * speed;
                
                dmger.spriteRenderer.color = color;
                dmger.spriteRenderer.sprite = projectileSprite;


                dmger.SelfTransform.localScale = scale;
                dmger.dmgNumbersOn = amPlayer;
                dmger.pierce = pierce;
                
                dmger.ToggleTrail(trail);

                if (aimbotEnabled)
                {
                    dmger.aimbotShot.SetAimbot(aimbotData);
                }


                dmger.ttlPool.ttl = _ttl;
                dmger.ttlPool.StartTimer();
            }

            if (_muzzleFlash)
            {
                muzzleFlash.SetVector2(Velocity, rb.velocity);
                if (_rainbow)
                {
                    muzzleFlash.SendEvent(RainbowMode, _eventAttribute);
                }
                else
                {
                    muzzleFlash.Play();
                }
            }

            if (!music || _musicContinuous) return;
            
            playOnShot.Play();
        }
        

        public override void ToggleShinyOn()
        {
            ToggleRainbow(true);
        }

        public override void ToggleShinyOff()
        {
            ToggleRainbow(false);
        }
    }
}
