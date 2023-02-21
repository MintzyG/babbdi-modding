using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Speedrun
{
    public class Speedrun : MelonMod
    {
        private static bool _isGrounded;
        private GameObject _player;
        private GroundedCheck _groundedCheck = new GroundedCheck();
        
        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Speedrun Mod loaded!");
        }
        
        public override void OnUpdate()
        {
            if (SceneManager.GetActiveScene().name == "Scene_AAA")
            {
                _isGrounded = _groundedCheck.CheckGrounded(_player);
            }
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (SceneManager.GetActiveScene().name == "Scene_AAA")
            {
                _player = GameObject.Find("Player");
                MelonEvents.OnGUI.Subscribe(DrawMenu, 100);
                MelonEvents.OnGUI.Subscribe(DrawTimerBox, 1);
            }
        }
        private void DrawMenu()
        {
            GUI.Box(new Rect(0, 0, 113, 20), "On Ground: " + _isGrounded);
        }
        private void DrawTimerBox()
        {
            GUI.Box(new Rect(1810, 0, 110, 50), "");
        }
    }
}