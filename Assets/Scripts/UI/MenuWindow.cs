using System;
using Level;
using UnityEditor;
using UnityEditor.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuWindow : AnimatedWindow
    {
       
        private Action _onClose;

        public void StartGameButton()
        {
            _onClose += () =>
            {
                var levelLoader = FindAnyObjectByType<LevelLoader>();
                levelLoader.LoadWithLoader("Level_1");
            };
            CloseButton();
        }

        public void OptionMenuButton()
        {
            LoadMenu.Load("UI/OptionMenu");
        }

        public void LocaleMenuButton()
        {
            LoadMenu.Load("UI/LocaleMenu");
        }

        public void ExitGameButton()
        {
            _onClose += () =>
            {
                Application.Quit();
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#endif
            };
            CloseButton();
        }

        protected override void OnClosedAnimation()
        {
            _onClose?.Invoke();
            base.OnClosedAnimation();
        }
    }
}