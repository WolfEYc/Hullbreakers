using UnityEngine;

namespace Hullbreakers
{
    public class OnDie : KillVFX
    {
        static readonly int ParticlesID = Shader.PropertyToID("Particles");
        static readonly int SpeedID = Shader.PropertyToID("Speed");
        
        
        void SetParticles(uint particles)
        {
            vfx.SetUInt(ParticlesID, particles);
        }

        void SetSpeed(float speed)
        {
            vfx.SetFloat(SpeedID, speed);
        }

        public void Spawn(Transform t, Color c, uint particles, float speed, float vol)
        {
            Spawn(t, c);
            SetParticles(particles);
            SetSpeed(speed);
            if (!(vol > 0f)) return;
            
            audioSource.volume = vol;
            audioSource.Play();
        }

        public static void StaticSpawn(Transform t, Color color, uint particles, float speed, float vol)
        {
            ((OnDie)GameMaster.Inst.OnDiePool.Get()).Spawn(t, color, particles, speed, vol);
        }
    }
}
