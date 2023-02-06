using System;
using MelonLoader;
using UnityEngine;
using System.Collections;
using System.Diagnostics.SymbolStore;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


namespace debabbdi
{
    public class Debabbdi : MelonMod
    {

        public static Debabbdi Instance;

        private static KeyCode _freezeToggleKey;
        private static KeyCode _menuToggleKey;
        private static KeyCode _dumpDebugInfo;

        private float coordinateX;
        private float coordinateY;
        private float coordinateZ;
        private Vector3 velocity;
        private double acceleration;
        
        private static bool _frozen;
        private static bool _menu;
        private static float _baseTimeScale;
        public override void OnEarlyInitializeMelon()
        {
            Instance = this;
            _freezeToggleKey = KeyCode.Q;
            _menuToggleKey = KeyCode.M;
            _dumpDebugInfo = KeyCode.P;
        }
        
        // Formatting is absolutely horrendous
        private void DrawMenu()
        {
            GUI.Box(new Rect(0, 0, 300, 200), "Info \n \n Position X: " + coordinateX + "\t \n Position Y: " + coordinateY +
                                              " \t \n Position Z: " + coordinateZ + "\t \n Speed: " + velocity + "\t \n Acceleration: " + acceleration + "\t");
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
            else if (Input.GetKeyDown(_dumpDebugInfo))
            {
                DumpDebug();
            }
            
            // Gets player info
            foreach (var camera in Camera.allCameras)
            {
                if (camera.name == "Main Camera")
                {
                    velocity = camera.velocity;
                    coordinateX = camera.transform.position.x;
                    coordinateY = camera.transform.position.y;
                    coordinateZ = camera.transform.position.z;
                    acceleration = Math.Sqrt(Math.Pow(velocity.x, 2) + Math.Pow(velocity.y, 2) +
                                             Math.Pow(velocity.z, 2));

                }
            }
        }
        private static void DumpDebug()
        {
            // Gets current scene name
            var sceneName = SceneManager.GetActiveScene().name;
            Instance.LoggerInstance.Msg("Loaded scene -> " + sceneName + "\n");

            /* Gets all cameras in loaded scene
            foreach (var camera in Camera.allCameras)
            {
                Instance.LoggerInstance.Msg("Found Camera: " + camera.name + " " + camera.transform.position.x);
            }
            */
            
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
        private static void ToggleFreeze()
        {
            _frozen = !_frozen;

            if (_frozen)
            {
                Instance.LoggerInstance.Msg("Freezing");
                
                _baseTimeScale = Time.timeScale; // Save the original time scale before freezing
                Time.timeScale = 0.6f;
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