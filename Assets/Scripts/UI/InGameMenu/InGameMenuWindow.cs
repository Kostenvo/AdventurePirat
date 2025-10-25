using System;
using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.InGameMenu
{
    public class InGameMenuWindow : AnimatedWindow
    {
        private ReloadLevelComponent _reloadLevel;
        private Canvas _canvas;
        private Action _onClose;
        private float _timeScale;

        protected override void Start()
        {
            _reloadLevel = GetComponent<ReloadLevelComponent>();
            _timeScale = Time.timeScale;
            Time.timeScale = 0;
            base.Start();
        }

        public void RestartGameButton()
        {
            _onClose = () => _reloadLevel.Reload();
            CloseButton();
        }
        public void OptionMenuButton()
        {
            LoadMenu.Load("UI/OptionMenu");
        }

        public void ExitGameButton()
        {
            _onClose = () => SceneManager.LoadScene("MainMenu");
            CloseButton();
        }

        protected override void OnClosedAnimation()
        {
            Time.timeScale = _timeScale;
            _onClose?.Invoke();
            base.OnClosedAnimation();
        }
    }
}