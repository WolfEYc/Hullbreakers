using UnityEngine;

namespace Hullbreakers
{
    public class DroneSpawner : WeaponBase
    {
        [SerializeField] Rigidbody2D dronePrefab;

        [SerializeField] int dronesPerSpawn;

        [SerializeField] float spread;

        [SerializeField] float speed;


        Vector3 _rot;
        Transform _spawnerParent;

        protected override void Start()
        {
            base.Start();
            _spawnerParent = GameMaster.Inst.enemyDroneTransform;
        }

        public override void Shoot()
        {
            float half = (dronesPerSpawn - 1) / 2f;

            _rot = shotPointTransform.eulerAngles;

            for (int i = 0; i < dronesPerSpawn; i++)
            {
                float diff = i - half;
                float prevz = _rot.z;
                
                _rot.z += diff * spread;

                Rigidbody2D drone = Instantiate(dronePrefab, _spawnerParent);

                var transform1 = drone.transform;
                transform1.eulerAngles = _rot;
                transform1.position = rb.position;

                _rot.z = prevz;
                
                drone.velocity = rb.velocity + (Vector2)transform1.up * speed;
            }

            if (!music || !playOnShot.enabled) return;

            playOnShot.Stop();
            playOnShot.Play();
        }

        public override void ToggleShinyOn()
        {
            throw new System.NotImplementedException();
        }

        public override void ToggleShinyOff()
        {
            throw new System.NotImplementedException();
        }
    }
}
