using UnityEngine;

namespace Hullbreakers
{
    public class Teleport : Ability, IShiny
    {
        [SerializeField] Rotation rotation;
        [SerializeField] float radius;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] SpriteRenderer visuals;
        [SerializeField] bool amPlayer;
        [SerializeField] float dmg;

        float _totalDmg;

        bool _rainbow;
        
        protected override void Awake()
        {
            base.Awake();
            _totalDmg = amPlayer ? dmg * GameMaster.Inst.PrestigeMult : dmg;
        }

        protected override void HandleUse()
        {
            Port(Lightning.ClampImpact(rb.position, rotation.target, radius));
        }

        public void Port(Vector2 location)
        {
            Lightning.Strike(rb.position, location, _rainbow ? CPURainbow.Color : visuals.color, _totalDmg, amPlayer);
            rb.position = location;
        }

        public void ToggleShinyOn()
        {
            _rainbow = true;
        }

        public void ToggleShinyOff()
        {
            _rainbow = false;
        }
    }
}
