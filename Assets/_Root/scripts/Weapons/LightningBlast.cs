using UnityEngine;

namespace Hullbreakers
{
    public class LightningBlast : WeaponBase
    {
        [SerializeField] float range;
        [SerializeField] float vol;

        bool _rainbow;

        public override void Shoot()
        {
            var position = rb.position;

            Vector2 impactPos = position + (Vector2)shotPointTransform.up * range;
            
            Lightning.Strike(shotPointTransform.position, impactPos, _rainbow ? CPURainbow.Color : spriteRenderer.color, dmgMultiplier, amPlayer, vol);
        }

        public override void ToggleShinyOn()
        {
            _rainbow = true;
        }

        public override void ToggleShinyOff()
        {
            _rainbow = false;
        }
    }
}
