using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;


namespace Hullbreakers
{
    [RequireComponent(typeof(VisualEffect))]
    public class PlanetSwitcher : MonoBehaviour
    {
        [SerializeField] float timeTillNewPlanet;
        [SerializeField] Texture2D[] planets;

        VisualEffect _planetVfx;
        WaitForSeconds _waitTillNewPlanet;

        static readonly int PlanetTextureID = Shader.PropertyToID("PlanetTexture");


        void Awake()
        {
            _waitTillNewPlanet = new WaitForSeconds(timeTillNewPlanet);
            _planetVfx = GetComponent<VisualEffect>();
        }

        IEnumerator Start()
        {
            while (true)
            {
                yield return _waitTillNewPlanet;
                _planetVfx.SetTexture(PlanetTextureID, RandomPlanet());
                _planetVfx.Play();
            }
        }

        Texture2D RandomPlanet()
        {
            return planets[Random.Range(0, planets.Length)];
        }
    }
}
