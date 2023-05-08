using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    public class FillMeter : MonoBehaviour
    {
        [SerializeField] Image fill;
        public Slider slider;
        const float Tolerance = 0.1f;
        public float speed = 100f;
        
        public void SetMaxValue(float value)
        {
            slider.maxValue = value;
        }

        public float Value { get; private set; }
    
        public void SetValue(float value)
        {
            Value = value;
            StartCoroutine(Slide());
        }

        public void SetValueImmediate(float value)
        {
            Value = value;
            slider.value = value;
        }

        IEnumerator Slide()
        {
            while (Math.Abs(slider.value - Value) > Tolerance)
            {
                slider.value = Mathf.MoveTowards(slider.value, Value, Time.deltaTime * speed);
                yield return null;
            }
        }

        public void SetFillColor(Color color)
        {
            fill.color = color;
        }
    }
}
