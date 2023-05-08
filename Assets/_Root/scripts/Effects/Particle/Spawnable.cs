using UnityEngine;

namespace Hullbreakers
{
    public class Spawnable : MonoBehaviour, IShiny
    {
        public SpriteRenderer spriteRenderer;
        Transform _transform;
        
        
        public uint amt;
        public float speed;
        public float vol;

        bool _rainbow;

        void Awake()
        {
            _transform = transform;
        }

        public void Spawn()
        {
            OnDie.StaticSpawn(_transform, _rainbow ? CPURainbow.Color : spriteRenderer.color , amt, speed, vol);
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
