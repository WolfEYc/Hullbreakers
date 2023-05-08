using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    public class DifficultyOption : MonoBehaviour
    {
        [SerializeField] GameMaster.Difficulty difficulty;
        [SerializeField] Button button;
        [SerializeField] GameObject toDisplayIfLocked;
        
        static int _difficultyUnlockLevel;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LoadSerializedData()
        {
            _difficultyUnlockLevel = PlayerPrefs.GetInt("difficultyUnlockLevel", 1);
        }

        void OnEnable()
        {
            bool playable = (int)difficulty <= _difficultyUnlockLevel;

            button.interactable = playable;
            if (toDisplayIfLocked != null)
            {
                toDisplayIfLocked.SetActive(!playable);
            }
        }
        
        
        
        public static void IncreaseDifficulty()
        {
            if ((int)GameMaster.Inst.difficulty < _difficultyUnlockLevel) return;

            PlayerPrefs.SetInt("difficultyUnlockLevel", ++_difficultyUnlockLevel);
        }
        
    }
}
