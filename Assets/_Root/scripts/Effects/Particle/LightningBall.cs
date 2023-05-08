using UnityEngine;
using UnityEngine.VFX;

namespace Hullbreakers
{
    [RequireComponent(typeof(VisualEffect))]
    public class LightningBall : MonoBehaviour
    {
        Transform _transform;
        GameObject _gameObject;
        VisualEffect _vfx;
        Color _color;

        static readonly int ColorID = Shader.PropertyToID("Color");

        void Awake()
        {
            _transform = transform;
            _gameObject = gameObject;
            _vfx = GetComponent<VisualEffect>();
            _transform.parent = GameMaster.Inst.shurikenPool;
            _gameObject.SetActive(false);
        }

        public void SetColor(Color color)
        {
            _color = color;
            _vfx.SetVector4(ColorID, _color);
        }

        public Color GetColor()
        {
            return _color;
        }
        
        public void Shoot(Vector2 pos, Color color)
        {
            _gameObject.SetActive(true);
            SetColor(color);
            _transform.position =pos;
            
        }

        public Vector2 Pos()
        {
            return transform.position;
        }
    }
}
