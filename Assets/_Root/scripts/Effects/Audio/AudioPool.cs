using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class AudioPool : MonoBehaviour
    {
        Transform _transform;
        ObjectPool<AudioSource> _audioPool;
        [SerializeField] AudioSource prefab;
        WaitForSeconds _waitforClip;

        void Awake()
        {
            _transform = transform;
            _audioPool = new ObjectPool<AudioSource>(CreateFunc);
            _waitforClip = new WaitForSeconds(prefab.clip.length);
        }
        
        AudioSource CreateFunc()
        {
            return Instantiate(prefab, _transform);
        }

        public void PlayAtPos(Vector2 pos, float vol)
        {
            AudioSource source = _audioPool.Get();
            source.volume = vol;
            source.transform.position = pos;
            source.Play();
            StartCoroutine(ReleaseRoutine(source));
        }

        IEnumerator ReleaseRoutine(AudioSource source)
        {
            yield return _waitforClip;
            _audioPool.Release(source);
        }
    }
}
