using UnityEngine;

namespace Hullbreakers
{
    public class SecondHalfAudio : MonoBehaviour
    {
        [SerializeField] AudioSource firstHalfAudio, secondHalfAudio;

        public void Play()
        {
            if(secondHalfAudio.isPlaying) return;
            firstHalfAudio.Stop();
            secondHalfAudio.Play();
        }
    }
}
