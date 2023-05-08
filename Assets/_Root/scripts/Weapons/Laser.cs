using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : WeaponBase, IShiny
    {
        RaycastHit2D[] _results;
        ShinyVFX[] _hitVFX;
        
        ContactFilter2D _contactFilter2D;
        
        LineRenderer _lr;
        Vector2 _endPt;
        [SerializeField] float defaultLength;
        [SerializeField] ShinyVFX hitVFXPrefab, muzzleFlash;

        const float BaseDmg = 10f;
        float _totalDmg;

        [SerializeField] Material rainbowLaser;
        Material _lazerDefault;
        
        [SerializeField] LayerMask enemy;
        [SerializeField] Transform target;
        
        
        const float LineOffset = 0.1f;
        bool _attachToTarget;

        Color _color;

        protected override void Awake()
        {
            base.Awake();
            _lr = GetComponent<LineRenderer>();
            _results = new RaycastHit2D[pierce];
            _totalDmg = BaseDmg * dmgMultiplier;
            _contactFilter2D.useLayerMask = true;
            _contactFilter2D.layerMask = enemy;
            defaultLength = defaultLength < 1f ? _lr.GetPosition(1).y : defaultLength;
            _color = spriteRenderer.color;
            _lazerDefault = _lr.material;
            _attachToTarget = target != null;
            
            InitHitVFX();
        }

        protected override void Start()
        {
            base.Start();
            _lr.startColor = _color;
            _lr.endColor = _color;
            muzzleFlash.SetColor(_color);
        }
        

        void InitHitVFX()
        {
            _hitVFX = new ShinyVFX[pierce];
            
            for (int i = 0; i < pierce; i++)
            {
                _hitVFX[i] = Instantiate(hitVFXPrefab, transform);
                _hitVFX[i].SetColor(_color);
            }
        }

        public override void ToggleShooting(bool on)
        {
            base.ToggleShooting(on);

            if (on)
            {
                _lr.enabled = true;
                if (playOnShot.enabled)
                {
                    playOnShot.Play();
                }

                muzzleFlash.Play();
                
                foreach (ShinyVFX hitVfx in _hitVFX)
                {
                    hitVfx.Play();
                }
            }
            else
            {
                _lr.enabled = false;
                playOnShot.Stop();

                muzzleFlash.Stop();
                foreach (ShinyVFX hitVfx in _hitVFX)
                {
                    hitVfx.Stop();
                }
            }
        }

        public override void ToggleShinyOn()
        {
            _lr.material = rainbowLaser;
            _lr.startColor = Color.white;
            _lr.endColor = Color.white;
            muzzleFlash.ToggleShinyOn();
            foreach (ShinyVFX shinyVFX in _hitVFX)
            {
                shinyVFX.ToggleShinyOn();
            }
        }

        public override void ToggleShinyOff()
        {
            _lr.material = _lazerDefault;
            _lr.startColor = _color;
            _lr.endColor = _color;
            muzzleFlash.ToggleShinyOff();
            foreach (ShinyVFX shinyVFX in _hitVFX)
            {
                shinyVFX.ToggleShinyOff();
            }
        }

        public override void Shoot()
        {

            int hits = Physics2D.Raycast(
                shotPointTransform.position, 
                shotPointTransform.up,
                _contactFilter2D,
                _results
            );

            foreach (ShinyVFX hitVfx in _hitVFX)
            {
                hitVfx.Stop();
            }
            
            for (int i = 0; i < hits; i++)
            {
                float resultDmg = _results[i].collider.GetComponent<IDamageable>().Damage(_totalDmg, shotPointTransform.up);

                _hitVFX[i].Play();
                _hitVFX[i].transform.SetPositionAndRotation(_results[i].point, shotPointTransform.rotation);

                if (!amPlayer) continue;
                
                GameMaster.Inst.DamageAtLocation(resultDmg, _results[i].point);
            }

            if (_attachToTarget)
            {
                var position = target.position;
                _endPt.y = Vector2.Distance(shotPointTransform.position, position);
                _lr.SetPosition(1, _endPt);
                //_hitVFX[^1].transform.SetPositionAndRotation(position, shotPointTransform.rotation);

                return;
            }
                
            _endPt.y = hits != pierce ? defaultLength : Vector2.Distance(_results[^1].point,shotPointTransform.position) - LineOffset;
            _lr.SetPosition(1, _endPt);
            //_hitVFX[^1].transform.SetPositionAndRotation(_results[^1].point, shotPointTransform.rotation);
            
        }
        
    
    }
}
