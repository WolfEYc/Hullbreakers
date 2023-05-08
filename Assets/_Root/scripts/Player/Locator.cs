using System;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Animator))]
    public class Locator : MonoBehaviour
    {
        Animator _animator;
        static readonly int Locate1 = Animator.StringToHash("locate");

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Start()
        {
            Locate();
        }

        public void Locate()
        {
            _animator.SetTrigger(Locate1);
        }
    }
}
