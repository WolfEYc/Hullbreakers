using UnityEngine;
using UnityEngine.Events;

namespace Hullbreakers
{
    public class Highscore : MonoBehaviour
    {
        public RectTransform highScoreTextParent;
        public HighScoreText scoreTextPrefab;
        public int nScoresSaved = 3;
    
        int[] _scores;
        HighScoreText[] _scoreTextInstances;

        public UnityEvent getInitials;
        public UnityEvent dontGetInitials;

        const int Score2Increase = 10000;

        string GetInitials(int i)
        {
            return PlayerPrefs.GetString($"{GameMaster.Inst.difficulty.ToString()}initials[{i}]", "AAA");
        }
    
        int GetScore(int i)
        {
            return PlayerPrefs.GetInt($"{GameMaster.Inst.difficulty.ToString()}score[{i}]", 0);
        }

        void SetInitials(int i, string initials)
        {
            PlayerPrefs.SetString($"{GameMaster.Inst.difficulty.ToString()}initials[{i}]", initials);
        }

        void SetScore(int i, int score)
        {
            PlayerPrefs.SetInt($"{GameMaster.Inst.difficulty.ToString()}score[{i}]", score);
        }
    
        void Awake()
        {
            _scores = new int[nScoresSaved];
            _scoreTextInstances = new HighScoreText[nScoresSaved];
            for(int i = 0; i < nScoresSaved; i++)
            {
                _scoreTextInstances[i] = Instantiate(scoreTextPrefab, highScoreTextParent);
            }
        }

    

        void LoadFromDisk()
        {
            for (int i = 0; i < nScoresSaved; i++)
            {
                _scores[i] = GetScore(i);
            
                _scoreTextInstances[i].scoreText.SetText(_scores[i].ToString());
                _scoreTextInstances[i].initialsText.SetText(GetInitials(i));
            }
        }

        bool CanSaveScore()
        {
            return GameMaster.Inst.lvlMaster.Score > _scores[nScoresSaved - 1];
        }
    
        public void SaveScore(int score, string initials, int i = 0)
        {
            for (; i < nScoresSaved; i++)
            {
                if (score <= _scores[i]) continue;
            
                SetScore(i, score);
                SetInitials(i, initials);

                SaveScore(_scores[i], _scoreTextInstances[i].initialsText.text, i + 1);
            
                _scores[i] = score;
                _scoreTextInstances[i].scoreText.SetText(score.ToString());
                _scoreTextInstances[i].initialsText.SetText(initials);
            
                break;
            }
        }

        public void HandleGameEnd()
        {
            if (GameMaster.Inst.lvlMaster.Score > Score2Increase)
            {
                DifficultyOption.IncreaseDifficulty();
            }
            
            LoadFromDisk();
            if (CanSaveScore())
            {
                getInitials.Invoke();
            }
            else
            {
                dontGetInitials.Invoke();
            }
        }
    }
}
