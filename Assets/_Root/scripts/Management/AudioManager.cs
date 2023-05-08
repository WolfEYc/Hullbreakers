using UnityEngine;
using UnityEngine.Audio;

namespace Hullbreakers
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioMixerSnapshot inGameOn;
        public AudioMixerSnapshot inGameOff;
        [SerializeField] AudioMixer mixer;
    
    
        public void ToggleInGameOn()
        {
            inGameOn.TransitionTo(0.01f);
        }
    
        public void ToggleInGameOff()
        {
            inGameOff.TransitionTo(0.01f);
        }

        public void SetMasterVol(float vol)
        {
            mixer.SetFloat("MasterVol", vol);
        }
    }
}
