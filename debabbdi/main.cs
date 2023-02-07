using System;
using MelonLoader;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace debabbdi
{
    public class Debabbdi : MelonMod
    {
        private static Debabbdi _instance;
        
        private static KeyCode _freezeToggleKey;
        private static KeyCode _menuToggleKey;
        private static KeyCode _dumpDebugInfo;

        private float _coordinateX;
        private float _coordinateY;
        private float _coordinateZ;
        private Vector3 _velocity;
        private double _acceleration;

        private static bool _cheatDetected;
        private static bool _freezeMenu;
        private static bool _menu;

        private static float _sliderHome = 1.0f;
        
        public override void OnEarlyInitializeMelon()
        {
            _instance = this;
            _freezeToggleKey = KeyCode.O;
            _menuToggleKey = KeyCode.P;
            _dumpDebugInfo = KeyCode.L;
        }

        public override void OnLateUpdate()
        {
            if (Input.GetKeyDown(_freezeToggleKey))
            {
                FreezeMenu();
            } 
            else if (Input.GetKeyDown(_menuToggleKey))
            {
                ToggleMenu();
            }
            else if (Input.GetKeyDown(_dumpDebugInfo))
            {
                DumpDebug();
            }
            
            // Gets player pos info off of the camera
            foreach (var camera in Camera.allCameras)
            {
                if (camera.name == "Main Camera")
                {
                    _velocity = camera.velocity;
                    _coordinateX = camera.transform.position.x;
                    _coordinateY = camera.transform.position.y;
                    _coordinateZ = camera.transform.position.z;
                    _acceleration = Math.Sqrt(Math.Pow(_velocity.x, 2) + Math.Pow(_velocity.y, 2) +
                                             Math.Pow(_velocity.z, 2));
                }
            }
        }
        private static void DumpDebug()
        {
            // Gets current scene name
            var sceneName = SceneManager.GetActiveScene().name;
            _instance.LoggerInstance.Msg("Loaded scene -> " + sceneName + "\n");

            // Gets all cameras in loaded scene
            foreach (var camera in Camera.allCameras)
            {
                _instance.LoggerInstance.Msg("Found Camera: " + camera.name);
            }

            /* Gets all objects in the loaded scene
            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            Instance.LoggerInstance.Msg("Object List: \n");
            foreach (var i in rootGameObjects)
            {
                Instance.LoggerInstance.Msg("Found Object: " + i.name);
            }
            */
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
        private void FreezeMenu()
        {
            _freezeMenu = !_freezeMenu;

            if (_freezeMenu)
            {
                MelonEvents.OnGUI.Subscribe(DrawFreezeMenu, 100);
            }
            else
            {
                MelonEvents.OnGUI.Unsubscribe(DrawFreezeMenu);
            }
        }
        private static void ToggleFreeze()
        {
            _cheatDetected = true;

            if (_cheatDetected)
            {
                MelonEvents.OnGUI.Subscribe(cheatOn);
            }
            
            _instance.LoggerInstance.Msg("Game speed set to: " + _sliderHome);
            Time.timeScale = _sliderHome;
        }
        private void DrawFreezeMenu()
        { 
            _sliderHome = GUI.HorizontalSlider(new Rect(1000, 1000, 200, 50), _sliderHome, 0.1f, 2.0f);
            
            if (GUI.Button(new Rect(900, 900, 70, 30), "Confirm"))
            {
                ToggleFreeze();
                FreezeMenu();
            } 
            else if (GUI.Button(new Rect(1000, 900, 70, 30), "Default"))
            {
                _sliderHome = 1.0f;
                ToggleFreeze();
                FreezeMenu();
            }
        }
        private void DrawMenu()
        {
            GUI.Box(new Rect(0, 0, 300, 200), "Info \n \n Position X: " + _coordinateX + "\t \n Position Y: " + _coordinateY +
                                              " \t \n Position Z: " + _coordinateZ + "\t \n Speed: " + _velocity + "\t \n Acceleration: " + _acceleration + "\t");
        }
        private static void cheatOn()
        {
            GUI.Box(new Rect(1817, 35, 100, 25),"<b><color=red>Cheated</color></b>", "richText");
        }
        public override void OnDeinitializeMelon()
        {
            _sliderHome = 1.0f;
            ToggleFreeze(); // Unfreeze the game in case the melon gets unregistered
        }
    }
}