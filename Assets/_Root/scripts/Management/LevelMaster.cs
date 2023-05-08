using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    public class LevelMaster : MonoBehaviour
    {
        [SerializeField] FillMeter xpFillMeter;

        [SerializeField] TMP_Text scoreText;
        [SerializeField] Image shipAvailable;
        [SerializeField] AmtCounter prestigeCounter;

        public event Action onXpObtained;

        public int Score { get; private set; }

        int _prestigeAvailable;
        int _xpPerLevel;
        int _idxAvailable;
        int _xpMissingForThisLevel;
        int _overAvailable;
        
        public void Cleanup()
        {
            Score = 0;
            _overAvailable = 0;
            _idxAvailable = 1;
            _prestigeAvailable = 0;
            UpdateXpPerLevel(GameMaster.Inst.requiredXpBase);

            _xpMissingForThisLevel = _xpPerLevel;
            xpFillMeter.SetValueImmediate(0f);
            scoreText.SetText(Score.ToString());
            UpdateOverCounter();
            UpdateShipAvailable();
        }
        public void IncreaseScore(int amt)
        {
            IncreaseLevelMeter(amt);
            Score += amt;
            scoreText.SetText(Score.ToString());
            onXpObtained?.Invoke();
        }

        public bool CanLevelUp()
        {
            return _prestigeAvailable > GameMaster.Inst.Prestige || _idxAvailable > PlayerShipyard.Instance.Idx + 1;
        }

        public void LevelUp()
        {
            if(!CanLevelUp()) return;
            _overAvailable--;
            GameMaster.Inst.LevelUpPlayer();
            UpdateOverCounter();
        }

        void IncreaseLevelMeter(int amt)
        {
            if(GameMaster.Inst.CurrentState != GameMaster.GameState.InGame) return;
            
            _xpMissingForThisLevel -= amt;

            if (_xpMissingForThisLevel > 0)
            {
                xpFillMeter.SetValue(_xpPerLevel - _xpMissingForThisLevel);
                return;
            }

            IncreaseIdxAvailable();
            UpdateShipAvailable();
            

            _xpMissingForThisLevel = _xpPerLevel;
            xpFillMeter.SetValue(0);
        }

        void IncreaseIdxAvailable()
        {
            _idxAvailable++;
            _overAvailable++;
            UpdateOverCounter();

            if (_idxAvailable < PlayerShipyard.Instance.ShipCount) return;
            
            _idxAvailable = 0;
            _prestigeAvailable++;
            
            
            UpdateXpPerLevel(GameMaster.Inst.XpPerPrestige(_prestigeAvailable));
        }

        void UpdateShipAvailable()
        {
            shipAvailable.sprite = PlayerShipyard.Instance.Ship(_idxAvailable).shipSprite;
            shipAvailable.color = PlayerShipyard.Instance.Ship(_idxAvailable).shipColor;
            xpFillMeter.SetFillColor(shipAvailable.color);
        }

        void UpdateOverCounter()
        {
            prestigeCounter.SetAmt(_overAvailable);
        }

        void UpdateXpPerLevel(int xp)
        {
            _xpPerLevel = xp;
            xpFillMeter.SetMaxValue(_xpPerLevel);
        }
        
    }
}
