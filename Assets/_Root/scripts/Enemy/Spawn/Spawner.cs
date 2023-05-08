using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hullbreakers
{
    public class Spawner : MonoBehaviour
    {
        public float spawnDist = 5f;
    
        [Header("Sort in ascending difficulty plz")]
        [SerializeField] Encounter[] encounterPrefabs;

        [SerializeField] Boss[] bosses;

        int _encountersLeft;
        int _rr;
        int _bossidx;

        public int deferFirstBossBy;
        public int diffEncounter = 3;
        public float waitBtwSpawn;
        public float waitBtwWave;
        public int bossEveryNWave;
        public int bossIncreasedEncounters;
        public float additionalSpawnDelayOnBoss;

        int SpawnDir => Random.Range(0, 4);
        float XInRange => Random.Range(GameMaster.Inst.Origin.x, GameMaster.Inst.Screen2World.x);
        float YInRange => Random.Range(GameMaster.Inst.Origin.y, GameMaster.Inst.Screen2World.y);
    
        int _enemiesSpawned;
        bool _moveAlongWave;
        bool _moveAlongSpawn;
        
        Transform _transform;
        IEnumerator _nextWaveTimer;

        WaitForSeconds _spawnInterval;
        WaitForSeconds _waveInterval;
    
        WaitUntil _nextWaveReady;
        WaitUntil _nextSpawnReady;
        CameraWiden _cameraWiden;

        bool AllDead => _transform.childCount == 0;
        bool BossWave => GameMaster.Inst.Wave % (bossEveryNWave + deferFirstBossBy) == 0;

        Vector2 SpawnLocation(int spawnDir)
        {
            Vector2 location = default;
            switch (spawnDir)
            {
                case 0:
                    location.x = XInRange;
                    location.y = GameMaster.Inst.Origin.y - spawnDist;
                    break;
                case 1:
                    location.x = GameMaster.Inst.Screen2World.x + spawnDist;
                    location.y = YInRange;
                    break;
                case 2:
                    location.x = XInRange;
                    location.y = GameMaster.Inst.Screen2World.y + spawnDist;
                    break;
                case 3:
                    location.x = GameMaster.Inst.Origin.x - spawnDist;
                    location.y = YInRange;
                    break;
            }

            return location;
        }

        Quaternion SpawnRot(int spawnDir)
        {
            return Quaternion.Euler(Vector3.forward * (spawnDir * 90));
        }

        void Awake()
        {
            _transform = transform;
            _cameraWiden = Camera.main!.GetComponent<CameraWiden>();
        
            _nextWaveReady = new WaitUntil(() => AllDead || _moveAlongWave);
            _nextSpawnReady = new WaitUntil(() => AllDead || _moveAlongSpawn);
            
            _waveInterval = new WaitForSeconds(waitBtwWave);
        }

        public void StartNextWave()
        {
            StartCoroutine(SpawnWave());
        }
        
        IEnumerator SpawnWave()
        {
            GameMaster.Inst.DisplayWave();
            
            if (BossWave)
            {
                SpawnBoss();
            }
        
            _encountersLeft = Math.Clamp(GameMaster.Inst.Wave, 3, 10) + (BossWave ? bossIncreasedEncounters : 0);
        
            for (int baseIdx = _rr;  _encountersLeft > 0; _rr++)
            {
                if (_rr == baseIdx + diffEncounter)
                {
                    _rr = baseIdx;
                }
                
                _encountersLeft--;

                SpawnEncounter();

                _moveAlongSpawn = false;
                
                StartCoroutine(NextSpawnTimer());
            
                yield return _nextSpawnReady;
            
                StopCoroutine(_nextSpawnReady);
            }
        
            _rr %= encounterPrefabs.Length;

            StartCoroutine(_nextWaveTimer = NextWaveTimer());
            
            yield return _nextWaveReady;
        
            StopCoroutine(_nextWaveTimer);
            _moveAlongWave = false;
            
            GameMaster.Inst.NextWave();
        }

        IEnumerator NextWaveTimer()
        {
            yield return _waveInterval;
            _moveAlongWave = true;
        }
    
        IEnumerator NextSpawnTimer()
        {
            yield return _spawnInterval;
            _moveAlongSpawn = true;
        }
    
        void SpawnBoss()
        {
            deferFirstBossBy = 0;
            int spawnDir = SpawnDir;
            Instantiate(bosses[_bossidx],
                SpawnLocation(spawnDir),
                SpawnRot(spawnDir), _transform);
            
            _bossidx++;
            _bossidx %= bosses.Length;
            _cameraWiden.Expand();
        }

        void SpawnEncounter()
        {
            int difficulty = _rr % encounterPrefabs.Length;
            
            float waitmult = InstantiateEncounter(difficulty);
            
            _spawnInterval = new WaitForSeconds(waitBtwSpawn * waitmult + (BossWave ? additionalSpawnDelayOnBoss : 0f));
        }

        public float InstantiateEncounter(int index)
        {
            int spawnDir = SpawnDir;

            Encounter spawnedEncounter = Instantiate(
                encounterPrefabs[index],
                encounterPrefabs[index].randomSpawn
                    ? SpawnLocation(spawnDir)
                    : encounterPrefabs[index].transform.position,
                SpawnRot(spawnDir));
            
            int len = spawnedEncounter.transform.childCount;

            for (int i=0; i < len; i++)
            {
                spawnedEncounter.transform.GetChild(0).parent = _transform;
            }

            float waitMult = spawnedEncounter.waitMult;
            
            Destroy(spawnedEncounter.gameObject);

            return waitMult;
        }

    }
}
