using System;
using UnityEngine;

namespace Hullbreakers
{
    public class NoobMenu : Singleton<NoobMenu>
    {
        [SerializeField] GameObject controls;
        [SerializeField] SetAllCanvasGroupsVisible inGameAllCanvasGroups;

        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
        }

        public void ShowNoob()
        {
            gameObject.SetActive(true);
            controls.SetActive(true);
        }
        
        public void SpawnTutorialEncounter()
        {
            GameMaster.Inst.EnemySpawnerInstance.InstantiateEncounter(1);
        }

        public void Cleanup()
        {
            inGameAllCanvasGroups.SetCanvasGroups(1f);
            gameObject.SetActive(false);
        }
    }
}
