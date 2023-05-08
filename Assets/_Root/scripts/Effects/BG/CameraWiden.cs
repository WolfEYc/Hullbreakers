using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class CameraWiden : MonoBehaviour
    {
        bool _expanded;
        public float expandedSize;
        public float speed;
        Camera _camera;
        float _regular;

        void Awake()
        {
            _camera = GetComponent<Camera>();
            _regular = _camera.orthographicSize;
        }

        public void Expand()
        {
            if(_expanded) return;
            _expanded = true;
            StartCoroutine(ExpandRoutine());
        }

        IEnumerator ExpandRoutine()
        {
            while (_camera.orthographicSize < expandedSize)
            {
                _camera.orthographicSize += speed * Time.deltaTime;
                GameMaster.Inst.UpdateScreen2WorldDim();
                yield return null;
            }

            _camera.orthographicSize = expandedSize;
            GameMaster.Inst.UpdateScreen2WorldDim();
        }

        public void ReturnToRegular()
        {
            _camera.orthographicSize = _regular;
            GameMaster.Inst.UpdateScreen2WorldDim();
            _expanded = false;
        }
    }
}
