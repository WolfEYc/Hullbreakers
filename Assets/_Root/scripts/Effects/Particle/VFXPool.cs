using UnityEngine;
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class VFXPool : MonoBehaviour
    {
        public ObjectPool<KillVFX> Pool { get; private set; }
        Transform _transform;
        [SerializeField] KillVFX prefab;
        [SerializeField] int defaultCapacity;

        void Awake()
        {
            _transform = transform;
            Pool = new ObjectPool<KillVFX>(CreateFunc, GetFunc, ReturnFunc, defaultCapacity: defaultCapacity, maxSize: defaultCapacity);
        }

        KillVFX CreateFunc()
        {
            KillVFX killVFX = Instantiate(prefab, _transform);
            killVFX.SetPool(Pool);
            return killVFX;
        }

        void ReturnFunc(KillVFX obj)
        {
            obj.GameObject.SetActive(false);
        }

        void GetFunc(KillVFX obj)
        {
            obj.GameObject.SetActive(true);
        }
    }
}
