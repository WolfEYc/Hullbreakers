using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hullbreakers
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu, settings, inGame;
        [SerializeField] InputAction pause;
        
        
        void OnEnable()
        {
            pause.performed += OnPause;
            pause.Enable();
        }

        void OnDisable()
        {
            pause.Disable();
            pause.performed -= OnPause;
        }

        public void Pause()
        {
            switch (GameMaster.Inst.CurrentState)
            {
                case GameMaster.GameState.Ready:
                    _on = true;
                    OpenSettings();
                    return;
                case GameMaster.GameState.Dead:
                    return;
            }
            
            AudioManager.Instance.ToggleInGameOff();
            Time.timeScale = 0f;
            _on = true;
            MenuManagement.Instance.OverrideMenu(pauseMenu);
            
        }

        public void Play()
        {
            switch (GameMaster.Inst.CurrentState)
            {
                case GameMaster.GameState.Ready:
                    _on = false;
                    CloseSettings();
                    return;
                case GameMaster.GameState.Dead:
                    return;
            }

            AudioManager.Instance.ToggleInGameOn();
            Time.timeScale = 1f;
            _on = false;
            MenuManagement.Instance.OverrideMenu(inGame);
            
        }

        public void OpenSettings()
        {
            MenuManagement.Instance.OpenMenu(settings);
        }

        public void CloseSettings()
        {
            MenuManagement.Instance.Back();
        }

        bool _on;


        public void OnPause(InputAction.CallbackContext context)
        {
            if (!context.action.IsPressed()) return;
            
            if (_on)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }
    }
}
