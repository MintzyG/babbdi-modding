using MelonLoader;
using UnityEngine;
using System.Collections;

namespace debabbdi
{
    public class Debabbdi : MelonMod
    {

        public static Debabbdi Instance;

        private static KeyCode _freezeToggleKey;
        private static KeyCode _menuToggleKey;
        
        private static bool _frozen;
        private static bool _menu;
        private static float _baseTimeScale;
        public override void OnEarlyInitializeMelon()
        {
            Instance = this;
            _freezeToggleKey = KeyCode.Q;
            _menuToggleKey = KeyCode.M;
        }
        
        // Formatting is absolutely horrendous
        private void DrawMenu()
        {
            GUI.Box(new Rect(0, 0, 300, 200), "Info \n \n Position X: \t \n Position Y:" +
                                              " \t \n Position Z: \t \n Speed: \t \n Acceleration: \t");
        }
        public override void OnLateUpdate()
        {
            if (Input.GetKeyDown(_freezeToggleKey))
            {
                ToggleFreeze();
            } 
            else if (Input.GetKeyDown(_menuToggleKey))
            {
                ToggleMenu();
            }
        }

        private void ToggleMenu()
        {
            _menu = !_menu;

            if (_menu)
            {
                MelonEvents.OnGUI.Subscribe(DrawMenu, 100);
            }
            else
            {
                MelonEvents.OnGUI.Unsubscribe(DrawMenu);
            }
        }
        private static void ToggleFreeze()
        {
            _frozen = !_frozen;

            if (_frozen)
            {
                Instance.LoggerInstance.Msg("Freezing");
                
                _baseTimeScale = Time.timeScale; // Save the original time scale before freezing
                Time.timeScale = 0;
            }
            else
            {
                Instance.LoggerInstance.Msg("Unfreezing");
                
                Time.timeScale = _baseTimeScale; // Reset the time scale to what it was before we froze the time
            }
        }

        public override void OnDeinitializeMelon()
        {
            if (_frozen)
            {
                ToggleFreeze(); // Unfreeze the game in case the melon gets unregistered
            }
        }
    }
}