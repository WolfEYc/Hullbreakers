using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(FillMeter))]
    public class FireRateMeter : MonoBehaviour
    {
        [SerializeField] TierText tierText;
        [SerializeField] TierColor tierColor;
        FillMeter _fillMeter;

        [SerializeField] float fillPercentPerPickUp;
        [SerializeField] float fireRatePerTier;
        [SerializeField] float depletionRate;
        
        void Awake()
        {
            _fillMeter = GetComponent<FillMeter>();
            
            GameMaster.Inst.gameStart.AddListener(CleanUp);
            GameMaster.Inst.OnNewShip += UpdateWeaponFireRate;
            GameMaster.Inst.lvlMaster.onXpObtained += LvlMasterOnonXpObtained;
        }

        void Start()
        {
            CleanUp();
        }

        void CleanUp()
        {
            _fillMeter.SetValue(0f);
            SetRank(TierText.Tier.F);
        }
        
        void LvlMasterOnonXpObtained()
        {
            _fillMeter.SetValueImmediate(_fillMeter.Value + fillPercentPerPickUp);

            if (_fillMeter.Value > _fillMeter.slider.maxValue)
            {
                UpRank();
            }

        }

        void UpRank()
        {
            if(tierText.CurrentTier == TierText.Tier.S)
            {
                _fillMeter.SetValueImmediate(_fillMeter.slider.maxValue);
                return;
            }
            
            _fillMeter.SetValueImmediate(_fillMeter.Value - _fillMeter.slider.maxValue);
            SetRank(tierText.CurrentTier + 1);
        }

        void DropRank()
        {
            if(tierText.CurrentTier == TierText.Tier.F)
            {
                _fillMeter.SetValueImmediate(0f);
                return;
            }
            
            _fillMeter.SetValueImmediate(_fillMeter.Value + _fillMeter.slider.maxValue);
            SetRank(tierText.CurrentTier - 1);
        }
        
        void SetRank(TierText.Tier tier)
        {
            tierText.SetTier(tier);
            tierColor.SetTier(tier);
            UpdateWeaponFireRate();
        }

        void UpdateWeaponFireRate()
        {
            foreach (WeaponBase weaponBase in GameMaster.Inst.PlayerWeapons)
            {
                weaponBase.SetFireRateMultiplier(1f + fireRatePerTier * (int)tierText.CurrentTier);
            }
        }

        void Update()
        {
            
            _fillMeter.SetValueImmediate(_fillMeter.Value - depletionRate * Time.deltaTime);
            DebugGraph.Log(_fillMeter.Value);

            if (_fillMeter.Value < _fillMeter.slider.minValue)
            {
                DropRank();
            }
        }
        
        
    }
}
