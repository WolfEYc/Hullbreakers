using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace Hullbreakers
{
    public class GameMaster : MonoBehaviour
    {
        public enum Difficulty
        {
            Tutorial,
            Easy,
            Normal,
            Hard
        }

        public enum GameState
        {
            Ready,
            InGame,
            Dead
        }
    
        public static GameMaster Inst { get; private set; }

        public static bool Testing { get; private set; }
        
        [SerializeField] bool testing;
        
        public Transform shurikenPool;
        public Transform playerSpawn;
        public Highscore highscoreMaster;
        public LevelMaster lvlMaster;

        public Spawner tutorialSpawner, normalSpawner, easySpawner, hardSpawner;
        
        public Transform enemyDroneTransform;
        public float prestigeScalar;
        public int requiredXpBase;
        public Difficulty difficulty;
    
        [SerializeField] ProjectilePool enemyPool, playerPool;
        [SerializeField] OrbPool xpOrbPool;
        [SerializeField] DamageNoPool damageNoPool;
        [SerializeField] VFXPool onHitPool, onDiePool;


        ObjectPool<TTLNo> _dmgNoPool;
        public ObjectPool<Orb> XpPool { get; private set; }
        public ObjectPool<Damager> EnemyPPool { get; private set; }
        public ObjectPool<Damager> PlayerPPool { get; private set; }
        public ObjectPool<KillVFX> OnHitPool { get; private set; }
        
        public ObjectPool<KillVFX> OnDiePool { get; private set; }

        public UnityEvent gameStart;
        public UnityEvent gameEnd;
        public UnityEvent<string> nextWave;

        public event Action OnNewShip;
        
        public LevelUp PlayerLevelUp { get; private set; }
        public GameObject PlayerInstance { get; private set; }
        public Spawner EnemySpawnerInstance { get; private set; }
        public Rigidbody2D PlayerRb { get; private set; }
        public Hull PlayerHull { get; private set; }

        public WeaponBase[] PlayerWeapons { get; private set; }
        
        
        public int Wave { get; private set; }
        public int Prestige { get; private set; }

        public float PrestigeMult { get; private set; }
        
        public Vector2 Origin { get; private set; }
        public Vector2 Screen2World { get; private set; }
        public GameState CurrentState { get; private set; }
    
        float _force2MidPlayer = 10f;
        Camera _main;
        
        void SetPrestigeMult()
        {
            PrestigeMult = 1f + Prestige * prestigeScalar;
        }

        public int XpPerPrestige(int prestige)
        {
            return (int)(requiredXpBase * (1f + prestige * prestigeScalar));
        }
        
        void Awake()
        {
            if (Inst != null)
            {
                Destroy(this);
                return;
            }

            Inst = this;
            DontDestroyOnLoad(Inst);
            Testing = testing;

            CurrentState = GameState.Ready;
            _main = Camera.main;


            UpdateScreen2WorldDim();
        }
    
        void Start()
        {
            AssociatePools();
        }

        void AssociatePools()
        {
            _dmgNoPool = damageNoPool.Pool;
            XpPool = xpOrbPool.Pool;
            EnemyPPool = enemyPool.Pool;
            PlayerPPool = playerPool.Pool;
            OnHitPool = onHitPool.Pool;
            OnDiePool = onDiePool.Pool;
        }

        public void UpdateScreen2WorldDim()
        {
            Origin = _main.ScreenToWorldPoint(Vector2.zero);
            Screen2World = _main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        }
    
        public void SpawnPlayer()
        {
            Prestige = 0;
            SetPrestigeMult();

            PlayerInstance = Instantiate(PlayerShipyard.Instance.NextShip(), playerSpawn.position, playerSpawn.rotation);
            AssociatePlayer();
            
            PlayerInstance.GetComponent<ScreenPort>().portInRange = false;
            PlayerRb.AddForce(-PlayerRb.position * _force2MidPlayer, ForceMode2D.Impulse);
        }
        void AssociatePlayer()
        {
            PlayerHull = PlayerInstance.GetComponent<Hull>();
            PlayerHull.destroyed.AddListener(gameEnd.Invoke); 
            PlayerRb = PlayerInstance.GetComponent<Rigidbody2D>();
            PlayerLevelUp = PlayerInstance.GetComponent<LevelUp>();
            PlayerWeapons = PlayerInstance.GetComponentsInChildren<WeaponBase>();
                        

            if (Testing)
            {
                PlayerLevelUp.AddXp(150);
            }
            
            OnNewShip?.Invoke();
        }

        public void DisAssociatePlayer()
        {
            CurrentState = GameState.Dead;
        }
    
        public void LevelUpPlayer()
        {
            Vector2 vel = PlayerRb.velocity;
            Vector2 pos = PlayerRb.position;
            float rot = PlayerRb.rotation;
            
            

            if (PlayerShipyard.Instance.LastShip)
            {
                Prestige++;
                SetPrestigeMult();
            }

            Destroy(PlayerInstance);
            
            PlayerInstance = Instantiate(PlayerShipyard.Instance.NextShip(), pos, Quaternion.Euler(0f, 0f, rot));
            
            AssociatePlayer();
            
            PlayerRb.velocity = vel;
        }
        public void StartGame()
        {
            CurrentState = GameState.InGame;
            EnemySpawnerInstance = difficulty switch
            {
                Difficulty.Tutorial => Instantiate(tutorialSpawner),
                Difficulty.Easy => Instantiate(easySpawner),
                Difficulty.Normal => Instantiate(normalSpawner),
                Difficulty.Hard => Instantiate(hardSpawner),
                _ => EnemySpawnerInstance
            };
            gameStart.Invoke();
        }

        public void FirstWave()
        {
            if(difficulty == Difficulty.Tutorial) return;
            NextWave();
        }
        
        public void NextWave()
        {
            if (CurrentState == GameState.Dead) return;
            Wave++;
            EnemySpawnerInstance.StartNextWave();
        }

        public void DisplayWave()
        {
            nextWave.Invoke(Wave.ToString());
        }

        public void KillPlayer()
        {
            PlayerHull.Kill();
        }
        
        public void CleanUp()
        {
            Wave = 0;

            Destroy(EnemySpawnerInstance.gameObject);
            
            foreach (Transform tr in enemyDroneTransform)
            {
                Destroy(tr.gameObject);
            }
            
            PlayerShipyard.Instance.CleanUp();

            CurrentState = GameState.Ready;
        }
        public void DamageAtLocation(float dmg, Vector2 location)
        {
            TTLNo ttlNo = _dmgNoPool.Get();
            ttlNo.SelfTransform.position = location;
            ttlNo.text.SetText(((int)dmg).ToString());
        }

        public void SetDifficulty(int newDiffculty)
        {
            difficulty = (Difficulty)newDiffculty;
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
